using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class BaseEnemy : MonoBehaviour
{   
    protected float m_maxHealth;
    protected float m_damagePerHit;

    protected float m_sightRange;
    protected float m_attackRange;


    [SerializeField] protected LayerMask m_groundMask;
    [SerializeField] protected LayerMask m_playerMask;

    [SerializeField] protected AudioClip m_walkSourceClip;
    [SerializeField] protected AudioClip m_runSourceClip;
    [SerializeField] protected AudioClip m_attackSourceClip;
    [SerializeField] protected AudioClip m_dieSourceClip;
    [SerializeField] protected AudioClip m_idleSourceClip;

    protected AudioSource m_walkSource;
    protected AudioSource m_runSource;
    protected AudioSource m_dieSource;
    protected AudioSource m_idleSource;
    protected AudioSource m_attackSource;


    protected Transform m_player;
    protected Animator m_animator;
    protected NavMeshAgent m_agent;
    protected float m_walkPointRange;

    protected HealthBar m_healthBar;
    protected GameObject m_roomIn;
    protected GameObject m_enemySpawner;

    protected Vector3 m_walkPoint;
    protected bool m_walkPointSet;
    protected bool m_playerInSightRange, m_playerInAttackRange;
    protected float m_walkSpeed;
    protected float m_runSpeed;
    protected float m_walkPointWait;
    protected float m_attackWait;

    protected bool m_patrolWaiting;
    protected bool m_attackWaiting;
    protected float m_health;

    protected EnemyState m_state;


    public float GetHealth() {return m_health;}
    public float GetHealthPercent() {return m_health/m_maxHealth;}
    public float GetMaxHealth() {return m_maxHealth;}
    public float GetAttackRange() {return m_attackRange;}

    protected abstract void Initialize();

    protected void Awake()
    {
        m_player = GameObject.Find("Player").transform;
        m_agent = GetComponent<NavMeshAgent>();
        m_roomIn = HaydenHelpers.FindParentWithTag(gameObject, "Room");
        m_enemySpawner = transform.parent.gameObject;
    }

    protected void Start()
    {
        Initialize();
        m_healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        m_healthBar.SetActive(false);
        m_animator = GetComponent<Animator>();
        m_health = m_maxHealth;
        m_agent.autoTraverseOffMeshLink = false;
        m_patrolWaiting = false;
        m_attackWaiting = false;
        m_agent.speed = m_walkSpeed;
        m_state = EnemyState.Idle;

        m_runSource.loop = true;
        m_walkSource.loop = true;
    }

    protected virtual void Update()
    {
        m_playerInSightRange = Physics.CheckSphere(transform.position, m_sightRange, m_playerMask);
        m_playerInAttackRange = Physics.CheckSphere(transform.position, m_attackRange, m_playerMask);

        m_runSource.Pause();
        m_walkSource.Pause();

        if (m_health <= 0)
        {
            m_state = EnemyState.Dying;
            Die();
        }
        else if (m_agent.isOnOffMeshLink)
        {
            if (m_state == EnemyState.Walking || m_state == EnemyState.Running)
            {
                MoveThroughDoor();
            }
        }
        else if (m_playerInAttackRange)
        {
            Attack();
        }
        else if (m_playerInSightRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        DoAnimations();
    }

    protected virtual void Die()
    {
        m_state = EnemyState.Dying;
        m_healthBar.SetActive(false);
        m_agent.speed = 0;

    }

    protected virtual void AfterDeath()
    {
        m_state = EnemyState.Dead;
        SpawnEnemyLoot.SpawnLoot();
        GameObject.Destroy(gameObject, 1.0f);
        m_enemySpawner.GetComponent<EnemySpawner>().DecrementEnemy();
    }
    protected virtual void MoveThroughDoor()
    {
        UnityEngine.AI.OffMeshLinkData data = m_agent.currentOffMeshLinkData;

        //calculate the final point of the link
        Vector3 endPos = data.endPos + Vector3.up * m_agent.baseOffset;

        //Move the agent to the end point
        m_agent.transform.position = Vector3.MoveTowards(m_agent.transform.position, endPos, m_agent.speed * Time.deltaTime);

        //when the agent reach the end point you should tell it, and the agent will "exit" the link and work normally after that
        if (m_agent.transform.position == endPos)
        {
            m_agent.CompleteOffMeshLink();
        }
    }

    protected virtual void Attack()
    {
        m_agent.SetDestination(transform.position);
        Vector3 playerCoords = m_player.transform.position;
        playerCoords.y = transform.position.y;
        transform.LookAt(playerCoords);

        // attack animation is happening or in cooldown after attack
        if (m_state == EnemyState.Attacking)
        {
            return;
        }
        if (m_attackWaiting)
        {
            m_state = EnemyState.Idle;
            return;
        }

        m_state = EnemyState.Attacking;
    }

    protected virtual void ChasePlayer()
    {
        m_state = EnemyState.Running;
        Vector3 playerCoords = m_player.transform.position;
        playerCoords.y = transform.position.y;
        transform.LookAt(playerCoords);

        m_agent.speed = m_runSpeed;
        m_agent.SetDestination(m_player.position);
    }

    protected virtual void Patrol()
    {
        if (m_patrolWaiting)
        {
            return;
        }

        if (!m_walkPointSet)
        {
            CreateWalkPoint();
        }

        m_state = EnemyState.Walking;
        m_agent.speed = m_walkSpeed;
        m_agent.SetDestination(m_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - m_walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            m_walkPointSet = false;
            m_patrolWaiting = true;
            m_agent.speed = 0;
            m_state = EnemyState.Idle;

            // sets patrolwaiting to false after certain amount of time
            HaydenHelpers.StartClock(m_walkPointWait, () => m_patrolWaiting = false);
        }
    }

    protected virtual void DoAnimations()
    {
        switch (m_state)
        {
            case EnemyState.Dying:
                HaydenHelpers.SetAnimation(m_animator, "isDying");
                m_dieSource.Play();
                // Die Animations and sound
                break;
            case EnemyState.Attacking:
                HaydenHelpers.SetAnimation(m_animator, "isAttacking");
                m_attackSource.Play();
                // Attack animations and sound
                break;
            case EnemyState.Running:
                if (!m_runSource.isPlaying)
                {
                    m_runSource.Play();
                }
                HaydenHelpers.SetAnimation(m_animator, "isRunning");
                // running animations and sound
                break;
            case EnemyState.Walking:
                HaydenHelpers.SetAnimation(m_animator, "isWalking");
                m_walkSource.Play();
                // walking animations and sound
                break;
            case EnemyState.Idle:
                HaydenHelpers.SetAnimation(m_animator, "isStanding");
                m_idleSource.Play();
                // idle animations and sound
                break;
        }
    }

    public virtual void AlertObservers(string message)
    {
        if (message.Equals("EnemyAttacked"))
        {
            if (m_playerInAttackRange)
            {
                // player.TakeDamage(m_damagePerHit);
            }
        }

        if (message.Equals("AttackAnimationEnded"))
        {
            m_attackWaiting = true;
            HaydenHelpers.StartClock(m_attackWait, () => m_attackWaiting = false);
        }

        if (message.Equals("DeathAnimationEnded"))
        {
           AfterDeath();
        }
    }

    protected virtual void CreateWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-m_walkPointRange, m_walkPointRange);
        float randomX = Random.Range(-m_walkPointRange, m_walkPointRange);

        m_walkPoint = new Vector3(m_enemySpawner.transform.position.x + randomX, 0, m_enemySpawner.transform.position.z + randomZ);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(m_walkPoint, out hit, 1f, NavMesh.AllAreas))
        {
            m_walkPointSet = true;
        }
    }

    static public BaseEnemy CheckIfEnemy(Collider collider)
    {
        BaseEnemy enemyScript = collider.gameObject.GetComponent<BaseEnemy>();
        if (!enemyScript)
        {
            enemyScript = collider.GetComponentInParent<BaseEnemy>();
        }
        return enemyScript;
    }

    public void TakeHealth(float health)
    {
        m_health += health;

        if (m_health > m_maxHealth)
        {
            m_health = m_maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        m_healthBar.SetActive(true);
        m_health -= damage;
    
        if (m_health <= 0)
        {
            m_health = 0;
            Die();
        }
    }

    public enum WeaponType
    {
        Arrow,
        Grenade,
        Trident,
        Flashlight,
        AOE
    }

    public enum EnemyState
    {
        Idle,
        Walking,
        Running,
        Attacking,
        Dying,
        Dead
    }

    public void HitEnemy(WeaponType weaponType, float damage)
    {
        switch(weaponType)
        {
            case WeaponType.Arrow:
                // Do Work
                break;
            case WeaponType.Grenade:
                // Do Work
                break;
            case WeaponType.Trident:
                // Do Work
                break;
            case WeaponType.Flashlight:
                break;
            case WeaponType.AOE:
                // Do Work
                break;
            default:
                break;
        }
        TakeDamage(damage);
    }
}
