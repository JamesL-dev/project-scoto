/*
 * Filename: Demo.cs
 * Developer: Rodney McCoy
 * Purpose: Control the demo mode
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Main Class
 *
 * Member Variables:
 * m_jump -- pass message to jump to player movement algorithm
 * m_sprint -- pass message to sprint to player movement algorithm
 * m_isSuccessMode -- determines if player should survive or fail
 * m_counter -- determines how long since last player input
 * m_camera -- Object Reference to the game camera
 * m_slackTime -- delta time between last player input and start of demo mode
 */
public class Demo : MonoBehaviour 
{
    [SerializeField] public static int m_counter;
    [SerializeField] public GameObject m_camera;
    [SerializeField] public int m_slackSeconds = 10;

    private static bool m_jump, m_sprint, m_isSuccessMode, m_attack;
    private static int m_slackTime;
    private static float rotation;

    // private UnityEngine.AI.NavMeshAgent m_agent;
    // // protected GameObject m_enemySpawner;
    // Vector3 walkArea = new Vector3(0, 0, 0);

    // protected float m_walkPointRange;
    // protected Vector3 m_walkPoint;
    // protected bool m_walkPointSet;
    // protected float m_walkSpeed;
    // protected float m_runSpeed;
    // protected float m_walkPointWait;
    // protected float m_attackWait;
    // // protected GameObject m_roomIn;

    // protected bool m_patrolWaiting;
    // protected bool m_attackWaiting;        

    void Awake()
    {
        // m_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // m_roomIn = HaydenHelpers.FindParentWithTag(gameObject, "Room");
        // m_enemySpawner = Find();transform.parent.gameObject;
        m_jump = m_sprint = false;
        m_isSuccessMode = true;
        m_attack = true;
        m_counter = 0;
        if(m_slackSeconds < 1) {Debug.LogError("m_slackSeconds is to low. Must be 1 seconds or greater"); m_slackSeconds = 10;}
        m_slackTime = m_slackSeconds * 60;
    }

    void FixedUpdate()
    {
        m_counter++; 
        m_attack = false;
        // if (m_agent.isOnOffMeshLink) {MoveThroughDoor();}
        if(m_counter == m_slackTime) { Debug.Log("Demo Mode Turned On");}

        if(On())
        {
            rotation = m_camera.transform.eulerAngles.x;
            if(true)
            {
                Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 10);
                bool enemyFound = false;
                int target = 0;
                foreach (var hitCollider in hitColliders)
                {
                    if(BaseEnemy.CheckIfEnemy(hitCollider)) 
                    {
                        enemyFound = true;
                        break;
                    }
                    target++;
                }
                if (enemyFound)
                {
                    m_attack = true;
                    Collider theTarget = hitColliders[target];
                    var speed = 1F * Time.deltaTime;
                    Vector3 deltaPos = theTarget.transform.position - gameObject.transform.position;

                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit, 5);
                    if(!hit.collider == theTarget)
                    {
                        transform.forward = Vector3.RotateTowards(transform.forward, deltaPos , speed, speed*4);
                    }

                    // Vector3 deltaVec = theTarget.transform.position - gameObject.transform.position;
                    // float playerAngle = Mathf.Atan2(gameObject.transform.position.y, gameObject.transform.position.x) * Mathf.Rad2Deg;
                    // float x = theTarget.transform.position.x - gameObject.transform.position.x;
                    // float y = theTarget.transform.position.y - gameObject.transform.position.y;
                    // float newAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                    // if(Mathf.Abs(playerAngle - newAngle) > 30)
                    // {
                    //     float val = -(Mathf.Abs(newAngle - playerAngle) - 90) + playerAngle;
                    //     Debug.Log(val + ", " + newAngle + ", " + playerAngle);
                    //     // gameObject.transform.rotation = Quaternion.AngleAxis(val, Vector3.up);
                    //     // m_counter = 0;
                    // }
                }
            }
            // Pathfinding
            if(true)
            {
                // Patrol();
            }
            
        }
    }

    // protected virtual void Patrol()
    // {
    //     if (m_patrolWaiting)
    //     {
    //         return;
    //     }

    //     if (!m_walkPointSet)
    //     {
    //         CreateWalkPoint();
    //     }

    //     m_agent.speed = m_walkSpeed;
    //     m_agent.SetDestination(m_walkPoint);

    //     Vector3 distanceToWalkPoint = transform.position - m_walkPoint;

    //     //Walkpoint reached
    //     if (distanceToWalkPoint.magnitude < 1f)
    //     {
    //         m_walkPointSet = false;
    //         m_patrolWaiting = true;
    //         m_agent.speed = 0;

    //         // sets patrolwaiting to false after certain amount of time
    //         HaydenHelpers.StartClock(m_walkPointWait, () => m_patrolWaiting = false);
    //     }
    // }

    // protected virtual void CreateWalkPoint()
    // {
    //     //Calculate random point in range
    //     float randomZ = Random.Range(-m_walkPointRange, m_walkPointRange);
    //     float randomX = Random.Range(-m_walkPointRange, m_walkPointRange);

    //     m_walkPoint = new Vector3(walkArea.x + randomX, 0, walkArea.z + randomZ);

    //     UnityEngine.AI.NavMeshHit hit;
    //     if (UnityEngine.AI.NavMesh.SamplePosition(m_walkPoint, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
    //     {
    //         m_walkPointSet = true;
    //     }
    // }

    // protected virtual void MoveThroughDoor()
    // {
    //     UnityEngine.AI.OffMeshLinkData data = m_agent.currentOffMeshLinkData;

    //     //calculate the final point of the link
    //     Vector3 endPos = data.endPos + Vector3.up * m_agent.baseOffset;

    //     //Move the agent to the end point
    //     m_agent.transform.position = Vector3.MoveTowards(m_agent.transform.position, endPos, m_agent.speed * Time.deltaTime);

    //     //when the agent reach the end point you should tell it, and the agent will "exit" the link and work normally after that
    //     if (m_agent.transform.position == endPos)
    //     {
    //         m_agent.CompleteOffMeshLink();
    //     }
    // }

    /*
     * Tell whether demo mode is on 
     *
     * Returns:
     * bool -- True if demo mode is on
     */
    public static bool On() { if (Demo.m_counter >= Demo.m_slackTime) return true; return false; }

    /*
     * Tell Player Movement Algorithm to Jump
     *
     * Returns:
     * float -- 1F if player should jump
     */
    public static float Jump() { if(Demo.m_jump) return 1F; return 0F; }

    /*
     * Tell Player Movement Algorithm to Sprint
     *
     * Returns:
     * float -- 1F if player should sprint
     */
    public static float Sprint() { if(Demo.m_sprint) return 1F; return 0F; }

    /*
     *
     */
    public static Vector3 Mouse() 
    { 
        if(180 >= rotation && rotation >= 1) return Vector3.up; 
        if(359 >= rotation && rotation > 180) return -Vector3.up; 
        return Vector3.zero;
    }


    /*
     * Tell WeaponManager to fire weapons
     *
     * Returns:
     * float -- 1F if player should attack
     */
    public static float Attack() { if(Demo.m_attack) return 1F; return 0F; }

    /*
     * Tell Player Movement Algorithm to Move Player
     *
     * Returns:
     * Vector2 showing x and y player movement
     */
    public static Vector2 Move()
    {
        return Vector2.zero;
    }

    /*
     * Tells demo mode that a player input has occured
     */
    public static void ResetTimer() { if(On()) Debug.Log("Demo Mode Turned Off."); Demo.m_counter = 0; }

    /*
     * Tell demo mode to swap from success mode to failure mode and vice versa
     */
    public static void SwapSuccessMode() {Demo.m_isSuccessMode = !Demo.m_isSuccessMode; }

    /*
     * Returns seconds it takes to go into demo mode
     *
     * Returns:
     * seconds tell demo starts with no player input
     */
    public static int MaxSeconds() { return Demo.m_slackTime/60; }
}

