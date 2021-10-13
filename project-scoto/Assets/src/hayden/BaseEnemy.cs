using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float m_health;
    [SerializeField] protected float m_speed;
    [SerializeField] protected float m_maxHealth;

    [SerializeField] protected float m_sightRange;
    [SerializeField] protected float m_attackRange;
    [SerializeField] protected float m_walkPointRange;

    [SerializeField] protected float m_timeBetweenAttacks;

    [SerializeField] protected LayerMask m_groundMask;
    [SerializeField] protected LayerMask m_playerMask;

    [SerializeField] protected NavMeshAgent m_agent;
    [SerializeField] protected Transform m_player;

    protected Vector3 m_walkPoint;
    protected bool m_walkPointSet;
    protected bool m_alreadyAttacked;
    protected bool m_playerInSightRange, m_playerInAttackRange;

    protected int m_numOfGrenadesIn;

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
        m_agent.speed = m_speed;
        m_numOfGrenadesIn = 0;

    }

    private void Update()
    {
        //Check for sight and attack range
        m_playerInSightRange = Physics.CheckSphere(transform.position, m_sightRange, m_playerMask);
        m_playerInAttackRange = Physics.CheckSphere(transform.position, m_attackRange, m_playerMask);

        if (!m_playerInSightRange && !m_playerInAttackRange) Patrol();
        if (m_playerInSightRange && !m_playerInAttackRange) ChasePlayer();
        if (m_playerInAttackRange && m_playerInSightRange) Attack();

        if (m_numOfGrenadesIn > 0)
        {
            float dps = 15;
            dps *= m_numOfGrenadesIn;
            float fps = 1 / Time.deltaTime;
            // should enemy be frozen if in grenade?
            TakeDamage(dps / fps);
        }
    }

    // does not see player, so randomly walks around
    private void Patrol()
    {
        if (!m_walkPointSet) CreateWalkPoint();

        if (m_walkPointSet)
            m_agent.SetDestination(m_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - m_walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            m_walkPointSet = false;
    }

    // sees player, so chases player
    private void ChasePlayer()
    {
        m_agent.SetDestination(m_player.position);
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

            // Destroy enemy in a half a second
            Invoke(nameof(Die), 0.5f);
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
        DestroyImmediate(gameObject, true);
    }

    private void ResetAttack()
    {
        m_alreadyAttacked = false;
    }

    private void Attack()
    {
        //Make sure enemy doesn't move
        m_agent.SetDestination(transform.position);

        // freezes x axis rotation, so weird glitching doesnt happen
        Vector3 playerCoords = m_player.transform.position;
        playerCoords.y = transform.position.y;
        transform.LookAt(playerCoords);

        if (!m_alreadyAttacked)
        {
            ///Attack code here

            m_alreadyAttacked = true;

            // call reset attack after a certain amount of time
            Invoke(nameof(ResetAttack), m_timeBetweenAttacks);
        }
    }

    private void CowerAway()
    {
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
}
