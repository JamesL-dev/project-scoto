/*
 * Filename: BaseEnemy.cs
 * Developer: Hayden Carroll
 * Purpose: This file implements the abstract BaseEnemy class
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/*
 * Abstract class that provides implementation for enemies
 */
public abstract class BaseEnemy : MonoBehaviour
{   
    [SerializeField] protected LayerMask m_groundMask;
    [SerializeField] protected LayerMask m_playerMask;
    [SerializeField] protected AudioClip m_walkSourceClip;
    [SerializeField] protected AudioClip m_attackSourceClip;
    [SerializeField] protected AudioClip m_dieSourceClip;
    [SerializeField] protected AudioClip m_idleSourceClip;
    [SerializeField] protected AudioClip m_hurtSourceClip;

    protected float m_maxHealth;
    protected float m_damagePerHit;
    protected float m_sightRange;
    protected float m_attackRange;
    protected AudioSource m_walkSource;
    protected AudioSource m_dieSource;
    protected AudioSource m_idleSource;
    protected AudioSource m_attackSource;
    protected AudioSource m_hurtSource;
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
    protected bool m_patrolWaiting, m_attackWaiting;
    protected float m_health;
    protected bool m_flashlightHit;
    protected EnemyState m_state;

    /*
    * Gets the current health of the enemy
    *
    * Returns:
    * float -- The current value of m_health (health of enemy):
    */
    public float GetHealth() {return m_health;}

    /*
    * Gets the current health of the enemy as a percent
    *
    * Returns:
    * float - gets the current health percent. Should return value [0, 1]
    */
    public float GetHealthPercent() {return m_health/m_maxHealth;}

    /*
    * Gets the max health the enemy can have
    *
    * Returns:
    * float - returns m_maxHealth (the maximum health the enemy can have)
    */
    public float GetMaxHealth() {return m_maxHealth;}

    /*
    * Gets the attack range of the enemy
    *
    * Returns:
    * float - the attack range of the enemy
    */
    public float GetAttackRange() {return m_attackRange;}

    /*
    * Determines if a collider belongs to an enemy
    *
    * Returns:
    * BaseEnemy - returns reference to a BaseEnemy object.
    *             If collider is not an enemy, reference is null.
    */
    static public BaseEnemy CheckIfEnemy(Collider collider)
    {
        BaseEnemy enemyScript = collider.gameObject.GetComponent<BaseEnemy>();
        if (!enemyScript)
        {
            enemyScript = collider.GetComponentInParent<BaseEnemy>();
        }
        return enemyScript;
    }

    /*
    * Gives the enemy a specified amount of health
    *
    * Parameters:
    * health - amount of health to give
    */
    public void TakeHealth(float health)
    {
        TakeDamage(-health);
    }

    /*
    * Takes damage away from the player
    *
    * Parameters:
    * damage - amount of damage to take
    */
    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            m_healthBar.SetActive(true);
        }
        m_health -= damage;
    
        if (m_health <= 0)
        {
            m_health = 0;
            Die();
        }
        else if (m_health >= m_maxHealth)
        {
            m_health = m_maxHealth;
        }
        m_healthBar.SetHealth(GetHealthPercent());
    }

    /*
    * Allows enemy to be hit by a weapon
    *
    * Parameters:
    * weaponType - type of weapon enemy was attacked with
    * damage - amount of damage to deal to enemy
    */
    public void HitEnemy(WeaponType weaponType, float damage)
    {
        TakeDamage(damage);
        switch(weaponType)
        {
            case WeaponType.Arrow:
                if (m_state != EnemyState.Dying)
                {
                    m_hurtSource.Play();
                }
                break;
            case WeaponType.Grenade:
                // Do Work
                break;
            case WeaponType.Trident:
                if (m_state != EnemyState.Dying)
                {
                    m_hurtSource.Play();
                }
                break;
            case WeaponType.Flashlight:
                m_flashlightHit = true;
                break;
            case WeaponType.AOE:
                // Do Work
                break;
            default:
                break;
        }
    }

    /*
    * public enum listing all the different types of weapons an enemy can
    * be attacked with
    */
    public enum WeaponType
    {
        Arrow,
        Grenade,
        Trident,
        Flashlight,
        AOE
    }


    protected enum EnemyState
    {
        Idle,
        Walking,
        Running,
        Roaring,
        Attacking,
        Dying,
        Dead
    }


    protected abstract void Initialize();

    protected virtual void Start()
    {
        m_enemySpawner = transform.parent.gameObject;
        m_player = GameObject.Find("Player").transform;
        m_agent = GetComponent<NavMeshAgent>();
        m_roomIn = HaydenHelpers.FindParentWithTag(gameObject, "Room");

        Initialize();
        m_walkSource = gameObject.AddComponent<AudioSource>();
        m_idleSource = gameObject.AddComponent<AudioSource>();
        m_attackSource = gameObject.AddComponent<AudioSource>();
        m_dieSource = gameObject.AddComponent<AudioSource>();
        m_hurtSource = gameObject.AddComponent<AudioSource>();

        m_walkSource.clip = m_walkSourceClip;
        m_idleSource.clip = m_idleSourceClip;
        m_attackSource.clip = m_attackSourceClip;
        m_dieSource.clip = m_dieSourceClip;
        m_hurtSource.clip = m_hurtSourceClip;

        m_healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        m_healthBar.SetActive(false);
        m_animator = GetComponent<Animator>();
        m_health = m_maxHealth;
        m_agent.autoTraverseOffMeshLink = false;
        m_patrolWaiting = false;
        m_attackWaiting = false;
        m_agent.speed = m_walkSpeed;
        m_state = EnemyState.Idle;
        Vector3 roomSize = m_roomIn.transform.Find("Floor").GetComponent<Collider>().bounds.size;
        m_walkPointRange = Mathf.Min(roomSize.x, roomSize.z) * 0.5f * 0.75f;

        m_walkSource.volume = 0.5f;
        m_walkSource.spatialBlend = 1.0f;
        m_walkSource.maxDistance = 25.0f;
        m_walkSource.rolloffMode = AudioRolloffMode.Linear;

        m_hurtSource.volume = 0.5f;
        m_hurtSource.spatialBlend = 1.0f;
        m_hurtSource.maxDistance = 25.0f;
        m_hurtSource.rolloffMode = AudioRolloffMode.Linear;

        m_dieSource.volume = 0.5f;
        m_dieSource.spatialBlend = 1.0f;
        m_dieSource.maxDistance = 25.0f;
        m_dieSource.rolloffMode = AudioRolloffMode.Linear;

        m_flashlightHit = false;
    }

    protected virtual void Update()
    {
        m_playerInSightRange = Physics.CheckSphere(transform.position, m_sightRange, m_playerMask);
        m_playerInAttackRange = Physics.CheckSphere(transform.position, m_attackRange, m_playerMask);


        if (m_health <= 0)
        {
            m_state = EnemyState.Dying;
            Die();
        }
        else if (m_flashlightHit)
        {
            OnFlashlightHit();
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

    protected virtual void OnFlashlightHit()
    {
        m_state = EnemyState.Idle;
        m_agent.speed = 0;
        
        Vector3 playerCoords = m_player.transform.position;
        playerCoords.y = transform.position.y;
        transform.LookAt(playerCoords);

        Vector3 difference = transform.position - playerCoords;
        if (difference.magnitude < 10.0f)
        {
            m_state = EnemyState.Running;
            transform.Translate(Vector3.back * m_runSpeed * Time.deltaTime);
        }
        m_flashlightHit = false;

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
            m_agent.SetDestination(transform.position);

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
                // Die Animations and sound
                break;
            case EnemyState.Attacking:
                HaydenHelpers.SetAnimation(m_animator, "isAttacking");
                m_attackSource.Play();
                // Attack animations and sound
                break;
            case EnemyState.Running:
                HaydenHelpers.SetAnimation(m_animator, "isRunning");
                // running animations and sound
                break;
            case EnemyState.Walking:
                HaydenHelpers.SetAnimation(m_animator, "isWalking");
                // walking animations and sound
                break;
            case EnemyState.Idle:
                HaydenHelpers.SetAnimation(m_animator, "isStanding");
                m_idleSource.Play();
                // idle animations and sound
                break;
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

    /*
    * This function is ONLY to be used for animation purposes. On certain
    * animation events (like right after the death animation ends), a call to
    * this function will be made to allow a certain action to occur, 
      (like triggering the AfterDeath() method)
    *
    * Parameters:
    * message - the message the animation sends to cause an action to occur
    */
    protected virtual void AlertObservers(string message)
    {
        if (message.Equals("EnemyAttacked"))
        {
            if (m_playerInAttackRange)
            {
                // player.TakeDamage(m_damagePerHit);
            }
        }

        if (message.Equals("FootTouchedFloor"))
        {
            m_walkSource.Play();
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

        if (message.Equals("DeathAnimationStarted"))
        {
            if (!m_dieSource.isPlaying)
            {
                m_dieSource.Play();
            }
        }
    }
}
