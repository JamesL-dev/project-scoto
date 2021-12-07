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
    // [SerializeField] public static GameObject m_camera;
    [SerializeField] public static NavAgent theNavAgent;
    [SerializeField] public int m_slackSeconds = 10, m_displayState;
    [SerializeField] public float m_displayAngle;
    [SerializeField] public bool m_lookingAtEnemy, m_isSuccessMode;
    public const int radius1 = 24;
    public static ProtoRoom currentRoom;

    private float closestEnemyDist, Angle;
    private static bool m_jump, m_sprint, m_attack, m_followNavAgent;
    private static int m_slackTime, m_counter, m_state;
    private static float rotation;
    private static Vector3 m_mouseValue;

    void Awake()
    {
        theNavAgent = GameObject.Find("DemoPathfinder").GetComponent<NavAgent>();
        m_jump = m_sprint = false;
        m_isSuccessMode = m_attack = m_followNavAgent = true;
        m_counter = m_state = 0;
        if(m_slackSeconds < 1) {Debug.LogError("m_slackSeconds is to low. Must be 1 seconds or greater"); m_slackSeconds = 10;}
        m_slackTime = m_slackSeconds * 60;
    }

    void FixedUpdate()
    {
        Vector3 Target3D = Vector3.zero;
        m_counter++; 
        m_attack = false;
        if(m_counter == m_slackTime) { Debug.Log("Demo Mode Turned On");}
        if (currentRoom.isCleared()) 
        { 
            currentRoom = LevelGeneration.Inst().m_roomsOpened[LevelGeneration.Inst().m_roomsOpened.Count - 1].gameObject.GetComponentInParent<ProtoRoom>();
        }


        if(On())
        {
            // Find enemies
            List<Collider> enemies = new List<Collider>();
            closestEnemyDist = (float)radius1;

            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, radius1);
            Collider closestEnemy = hitColliders[0];
            foreach (var hitCollider in hitColliders)
            {
                if(BaseEnemy.CheckIfEnemy(hitCollider)) 
                {
                    Vector3 enemyPosition = hitCollider.transform.position;
                    float thisDistance = Vector3.Magnitude(enemyPosition - gameObject.transform.position);

                    if(closestEnemyDist > thisDistance) { closestEnemyDist = thisDistance; }
                    closestEnemy = hitCollider;
                    enemies.Add(hitCollider);
                }
            }



            // CONTROL MOVEMENT OF PLAYER
            m_attack = true;
            if(closestEnemyDist < 6)
            {  
                // if(m_state != 1) {NavAgent.Teleport(gameObject.transform.position);}
                Target3D = closestEnemy.transform.position - gameObject.transform.position;
                NavAgent.GoTo(gameObject.transform.position - closestEnemy.transform.position);
                m_state = 1;
            }
            else if(closestEnemyDist < 12) 
            {    
                Target3D = closestEnemy.transform.position - gameObject.transform.position;
                
                m_state = 2;
            } 
            else if(closestEnemyDist < radius1)
            {
                // if(m_state != 3) {NavAgent.Teleport(gameObject.transform.position); }
                Target3D = closestEnemy.transform.position - gameObject.transform.position;
                NavAgent.GoTo(closestEnemy.transform.position);
                m_state = 3;
            } 
            else
            {
                m_attack = false;

                if((currentRoom.roomType() == 1 && Vector3.Magnitude(currentRoom.gameObject.transform.position - gameObject.transform.position) < 1) || m_state == 5)
                {
		            // Behavior if it is the endroom to leave the level
                    // if(m_state != 5) {NavAgent.Teleport(gameObject.transform.position); }

                    Target3D = new Vector3(0, 0, 1);
                    NavAgent.GoTo(gameObject.transform.position + new Vector3(0, 0, 10));
                    m_state = 5;
                }
                else if(currentRoom.roomType() == 0)
                {
                    // if(m_state != 6) {NavAgent.Teleport(gameObject.transform.position); }

                    Target3D = new Vector3(0, 0, 1);
                    NavAgent.GoTo(new Vector3(0, 0, 20));
                    m_state = 6;
                }
                else
                {
                    // if(m_state != 4) {NavAgent.Teleport(gameObject.transform.position); }
                    Target3D = NavAgent.getPosition() - gameObject.transform.position;
                    NavAgent.GoTo(currentRoom.gameObject.transform.position);
                    m_state = 4;
                }                            
            } 

            Vector3 move = NavAgent.getPosition() - gameObject.transform.position;
            move.y = gameObject.transform.position.y;

            gameObject.transform.position = gameObject.transform.position + .25F * move;
            if(Vector3.Magnitude(NavAgent.getPosition() - gameObject.transform.position) > 2F) NavAgent.Teleport(gameObject.transform.position);
            m_displayState = m_state;



            // CONTROL ROTATION OF PLAYER
            m_mouseValue = Vector3.zero;
            m_lookingAtEnemy = false;

            Vector2 Target, Current;
            if(Target3D == Vector3.zero) { Debug.LogError("Target Not Set"); }
            Target.x = Target3D.x; 
            Target.y = Target3D.z; 
            Current.x = gameObject.transform.forward.x;
            Current.y = gameObject.transform.forward.z;
            Angle = Vector2.SignedAngle(Target, Current);

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 20);
            if(hit.collider != null && BaseEnemy.CheckIfEnemy(hit.collider)) m_lookingAtEnemy = true;

            if(180 >= rotation && rotation >= 1) m_mouseValue.y = 1; 
            if(359 >= rotation && rotation > 180) m_mouseValue.y = -1; 
            if(!m_lookingAtEnemy) {m_mouseValue.x = Angle;}
            m_displayAngle = Angle;
        }
    }


    // /*
    //  * Returns minimum float value
    //  */
    // private Vector3 range(Vector3 variable, float range)
    // {
    //     Vector3 returnValue = Vector3.zero;
        
    //     if(variable.x >= 0) returnValue.x = (variable.x > range ? range : variable.x);
    //     else returnValue.x = (variable.x < range ? range : variable.x);

    //     if(variable.y >= 0) returnValue.y = (variable.y > range ? range : variable.y);
    //     else returnValue.y = (variable.y < range ? range : variable.y);

    //     if(variable.z >= 0) returnValue.z = (variable.z > range ? range : variable.z);
    //     else returnValue.z = (variable.z < range ? range : variable.z);

    //     return returnValue;
    // }

    // /*
    //  * Returns minmum float value
    //  */
    // private float range(float variable, float range)
    // {
    //     if(variable >= 0 && variable > range) {return range;}
    //     if(variable < 0 && variable < -1 * range) {return -1*range;}
    //     return variable;
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
        return m_mouseValue;
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
        return Vector3.zero;
    }

    /*
     * Tells demo mode that a player input has occured
     */
    public static void ResetTimer() 
    { 
        if(On())
        {
            Debug.Log("Demo Mode Turned Off.");
        }
        Demo.m_counter = 0; 
    }


    /*
     * Returns seconds it takes to go into demo mode
     *
     * Returns:
     * seconds tell demo starts with no player input
     */
    public static int MaxSeconds() { return Demo.m_slackTime/60; }
}

