using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class BaseEnemy : MonoBehaviour
{   
    [SerializeField] protected float m_maxHealth;
    [SerializeField] protected float m_damagePerHit;

    [SerializeField] protected float m_sightRange;
    [SerializeField] protected float m_attackRange;


    [SerializeField] protected LayerMask m_groundMask;
    [SerializeField] protected LayerMask m_playerMask;

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

    protected bool m_patrolWaiting;
    protected bool m_isDead;
    protected float m_health;


    public float GetHealth() {return m_health;}
    public float GetHealthPercent() {return m_health/m_maxHealth;}
    public float GetMaxHealth() {return m_maxHealth;}
    public float GetAttackRange() {return m_attackRange;}

    protected abstract void Initialize();

    private void Awake()
    {
        m_player = GameObject.Find("Player").transform;
        m_agent = GetComponent<NavMeshAgent>();
        m_roomIn = HaydenHelpers.FindParentWithTag(gameObject, "Room");
        m_enemySpawner = transform.parent.gameObject;
    }

    private void Start()
    {
        Initialize();
        m_healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        m_healthBar.SetActive(false);
        m_animator = GetComponent<Animator>();
        m_health = m_maxHealth;
        m_agent.autoTraverseOffMeshLink = false;
        m_isDead = false;
        m_patrolWaiting = false;
        m_agent.speed = m_walkSpeed;
    }

    

    private void Update()
    {
        if (!m_isDead)
        {
            //do raycast instead, so even if in another room enemy doesnt move if they cant see player
            m_playerInSightRange = Physics.CheckSphere(transform.position, m_sightRange, m_playerMask);
            m_playerInAttackRange = Physics.CheckSphere(transform.position, m_attackRange, m_playerMask);
            m_playerInAttackRange = m_playerInAttackRange && !m_agent.isOnOffMeshLink;

            // cant see player and not in patrol
            if (!m_playerInSightRange)
            {
                Patrol();
            }

            // can see player but cant attack
            if (m_playerInSightRange && !m_playerInAttackRange)
            {
                ChasePlayer();
            }

            // can attack player
            if (m_playerInAttackRange)
            {
                Attack();
            }
        }
    
        if (m_agent.isOnOffMeshLink)
        {
            OffMeshLinkData data = m_agent.currentOffMeshLinkData;

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
    }

    // does not see player, so randomly walks around
    private void Patrol()
    {
        if (m_patrolWaiting)
        {
            return;
        }

        if (!m_walkPointSet)
        {
            CreateWalkPoint();
        }

        m_agent.speed = m_walkSpeed;
        HaydenHelpers.SetAnimation(m_animator, "isWalking");
        m_agent.SetDestination(m_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - m_walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            HaydenHelpers.SetAnimation(m_animator, "isStanding");
            m_walkPointSet = false;
            m_patrolWaiting = true;
            m_agent.speed = 0;

            // sets patrolwaiting to false after certain amount of time
            HaydenHelpers.StartClock(m_walkPointWait, () => m_patrolWaiting = false);
        }
    }


    // sees player, so chases player
    private void ChasePlayer()
    {
        m_agent.speed = m_runSpeed;
        m_agent.SetDestination(m_player.position);
        HaydenHelpers.SetAnimation(m_animator, "isRunning");
    }

    private void CreateWalkPoint()
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

    public void TakeHealth(float health)
    {
        m_health += health;

        if (m_health > m_maxHealth)
        {
            m_health = m_maxHealth;
        }
    }

    private void Die()
    {
        HaydenHelpers.SetAnimation(m_animator, "isDying");
        m_healthBar.SetActive(false);
        m_isDead = true;
        m_agent.speed = 0;
    }

    private void Attack()
    {
        //Make sure enemy doesn't move
        m_agent.SetDestination(transform.position);
        HaydenHelpers.SetAnimation(m_animator, "isAttacking");

        // freezes x axis rotation, so weird glitching doesnt happen
        Vector3 playerCoords = m_player.transform.position;
        playerCoords.y = transform.position.y;
        transform.LookAt(playerCoords);
    }
    public virtual void AlertObservers(string message)
    {
        Debug.Log("alert observers is called!");
        if (message.Equals("AttackAnimationEnded"))
        {
            if (m_playerInAttackRange)
            {
                // player.TakeDamage(m_damagePerHit);
            }
            
        }

        if (message.Equals("DeathAnimationEnded"))
        {
            GameObject.Destroy(gameObject, 1.0f);
            SpawnEnemyLoot.SpawnLoot();
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
