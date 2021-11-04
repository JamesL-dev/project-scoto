/*
 * Filename: Demo.cs
 * Developer: Rodney McCoy
 * Purpose: Controls the demo mode
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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
   
    // protected NavMeshAgent m_agent;

    void Awake()
    {
        // m_agent = GetComponent<NavMeshAgent>();
        // m_agent.autoTraverseOffMeshLink = false;
        // m_agent.speed = 2F;
        // m_agent = GetComponent<NavMeshAgent>();

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
        if(m_counter == m_slackTime) { Debug.Log("Demo Mode Turned On");}

        Vector3 debugVec = Vector3.zero;

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
                        float angleBetween = Vector3.Angle(transform.forward, deltaPos);
                        transform.Rotate(0, angleBetween * .025F, 0);
                        Debug.Log("asdf");
                    }

                    debugVec = theTarget.transform.position;
                }
            }
            // Pathfinding
            if(true)
            {
                // Patrol();
                if(debugVec != Vector3.zero)
                {
                    // if(!m_agent.SetDestination(debugVec))
                    // {
                    //     Debug.Log("A");   
                    // }
                    
                }
            }
            
        }
    }











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

