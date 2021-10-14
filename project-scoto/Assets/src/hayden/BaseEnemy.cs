using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float m_health;
    [SerializeField] protected float m_speed;
    [SerializeField] protected float m_maxHealth;
    [SerializeField] protected float m_damagePerHit;

    [SerializeField] protected float m_sightRange;
    [SerializeField] protected float m_attackRange;
    [SerializeField] protected float m_walkPointRange;


    [SerializeField] protected LayerMask m_groundMask;
    [SerializeField] protected LayerMask m_playerMask;

    protected Transform m_player;
    protected Animator m_animator;
    protected NavMeshAgent m_agent;

    protected Vector3 m_walkPoint;
    protected bool m_walkPointSet;
    protected bool m_playerInSightRange, m_playerInAttackRange;

    protected int m_numOfGrenadesIn;

    protected float m_walkSpeed;
    protected float m_runSpeed;
    protected float m_walkPointWait;

    protected bool m_isInPatrol;
    protected bool m_isDead;


    public float GetHealth() {return m_health;}
    public float GetMaxHealth() {return m_maxHealth;}
    public float GetAttackRange() {return m_attackRange;}

    private void Awake()
    {
        m_player = GameObject.Find("Player").transform;
        m_agent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        m_walkSpeed = 2.0f;
        m_runSpeed = 8.0f;
        m_walkPointWait = 3.0f;
        m_damagePerHit = 10.0f;

        m_agent.speed = m_walkSpeed;
        m_numOfGrenadesIn = 0;
        m_isInPatrol = false;
        m_isDead = false;

        m_animator = GetComponent<Animator>();

    }

    private void Update()
    {


        if (!m_isDead)
        {
                    //Check for sight and attack range
            m_playerInSightRange = Physics.CheckSphere(transform.position, m_sightRange, m_playerMask);
            m_playerInAttackRange = Physics.CheckSphere(transform.position, m_attackRange, m_playerMask);
            if (!m_playerInSightRange && !m_playerInAttackRange && !m_isInPatrol)
            {
                StartCoroutine("Patrol");
            }

            if (m_playerInSightRange && !m_playerInAttackRange)
            {
                ChasePlayer();
            }
            else
            {
                m_animator.SetBool("isRunning", false);
            }
            if (m_playerInAttackRange && m_playerInSightRange)
            {
                Attack();
            }
            else 
            {
                m_animator.SetBool("isAttacking", false);
            }

            if (m_numOfGrenadesIn > 0)
            {
                float dps = 15;
                dps *= m_numOfGrenadesIn;
                float fps = 1 / Time.deltaTime;
                // should enemy be frozen if in grenade?
                TakeDamage(dps / fps);
            }
        }
    }

    // does not see player, so randomly walks around
    private IEnumerator Patrol()
    {
        m_isInPatrol = true;

        m_agent.speed = m_walkSpeed;
        if (!m_walkPointSet) CreateWalkPoint();

        if (m_walkPointSet)
        {
            m_agent.SetDestination(m_walkPoint);
            m_animator.SetBool("isWalking", true);
        }

        Vector3 distanceToWalkPoint = transform.position - m_walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            m_animator.SetBool("isWalking", false);
            m_walkPointSet = false;
            yield return new WaitForSeconds(m_walkPointWait);

        }

        m_isInPatrol = false;
    }



    // sees player, so chases player
    private void ChasePlayer()
    {
        m_agent.speed = m_runSpeed;
        m_agent.SetDestination(m_player.position);
        m_animator.SetBool("isRunning", true);
        m_animator.SetBool("isWalking", false);
    }

    private void CreateWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-m_walkPointRange, m_walkPointRange);
        float randomX = Random.Range(-m_walkPointRange, m_walkPointRange);

        m_walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(m_walkPoint, -transform.up, 2f, m_groundMask))
            m_walkPointSet = true;
    }

    public void TakeDamage(float damage)
    {
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
        // DestroyImmediate(gameObject, true);
        // m_animator.SetBool("isRunning", false);
        // m_animator.SetBool("isWalking", false);
        // m_animator.SetBool("isStanding", false);
        // m_animator.SetBool("isAttacking", false);
        m_animator.SetBool("isDying", true);
        m_isDead = true;
        m_agent.speed = 0;
    }

    private void Attack()
    {
        //Make sure enemy doesn't move
        m_agent.SetDestination(transform.position);
        m_animator.SetBool("isAttacking", true);

        // freezes x axis rotation, so weird glitching doesnt happen
        Vector3 playerCoords = m_player.transform.position;
        playerCoords.y = transform.position.y;
        transform.LookAt(playerCoords);
    }

    public void OnFlashlightHit()
    {
        TakeDamage(100000);
    }

    public void OnArrowHit(GameObject arrow)
    {
        // do sound
        // do animation
        float arrowDamage = arrow.GetComponent<Arrow>().damage;
        TakeDamage(arrowDamage);
    }

    public void OnGrenadeHit(GameObject grenade)
    {
        // do sound
        // do animation
        int initialGrenadeDamage = 50;
        // I NEED THE INITIAL GRENADE DAMAGE
        TakeDamage(initialGrenadeDamage);
    }

    public void OnTridentHit(GameObject trident)
    {
        // do sound
        // do animation
        int tridentDamage = 5;
        // I NEED THE TRIDENT DAMAGE
        TakeDamage(tridentDamage);
    }

    
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "grenadeAOE")
        {
            m_numOfGrenadesIn++;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "grenadeAOE")
        {
            m_numOfGrenadesIn--;
        }

        if (m_numOfGrenadesIn < 0)
        {
            m_numOfGrenadesIn = 0;
        }
    }

    public void DecrementNumGrenadesIn()
    {
        m_numOfGrenadesIn--;
        if (m_numOfGrenadesIn < 0)
        {
            m_numOfGrenadesIn = 0;
        }
    }

    public void AlertObservers(string message)
    {
        if (message.Equals("AttackAnimationEnded"))
        {
            if (m_playerInAttackRange)
            {
                Debug.Log("hit player");
                // player.TakeDamage(m_damagePerHit);
            }
            
        }

        if (message.Equals("DeathAnimationEnded"))
        {
            Debug.Log("Death Animation Ended. Enemy should start to die.");
            // GameObject.Destroy(gameObject);
        }
    }
}
