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
    [SerializeField] public static GameObject m_camera;
    [SerializeField] public int m_slackSeconds = 10, m_displayState;
    [SerializeField] public float m_displayAngle;
    [SerializeField] public bool m_isSuccessMode;
    public static bool m_isOn;
    public const int radius1 = 25;
    public static ProtoRoom currentRoom;
    public static ProtoRoom endRoom;

    private float closestEnemyDist, Angle;
    private static bool m_jump, m_sprint, m_attack, foundEndRoom;
    private static int m_slackTime, m_counter, m_state;
    private static float rotation;
    private static Vector3 m_mouseValue;

    void Awake()
    {
        m_isOn = false;
        theNavAgent = GameObject.Find("DemoPathfinder").GetComponent<NavAgent>();
        m_camera = GameObject.Find("Main Camera");
        m_jump = m_sprint = foundEndRoom = false;
        m_isSuccessMode = m_attack = true;
        m_counter = m_state = 0;
        if(m_slackSeconds < 1) {Debug.LogError("m_slackSeconds is to low. Must be 1 seconds or greater"); m_slackSeconds = 10;}
        m_slackTime = m_slackSeconds * 60;
    }

    void FixedUpdate()
    {
        Vector3 Target3D = Vector3.zero;
        // if(m_isOn) {m_counter++;}
        // if(!m_isOn) {m_counter = 0;}
        m_attack = false;
        // if(m_counter < m_slackTime) {NavAgent.Teleport(gameObject.transform.position); NavAgent.GoTo(gameObject.transform.position);}
        // if(m_counter == m_slackTime) { Debug.Log("Demo Mode Turned On"); }
        if(!m_isOn) {NavAgent.Teleport(gameObject.transform.position); NavAgent.GoTo(gameObject.transform.position);}
        

        if(On())
        {
            PlayerData.Inst().TakeHealth(1F);

            // Find enemies
            List<Collider> enemies = new List<Collider>();
            closestEnemyDist = (float)radius1;

            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, radius1);
            Collider closestEnemy = hitColliders[0];
            foreach (var hitCollider in hitColliders)
            {
                if(BaseEnemy.isEnemyAndAlive(hitCollider)) 
                {
                    Vector3 enemyPosition = hitCollider.transform.position;
                    float thisDistance = Vector3.Magnitude(enemyPosition - gameObject.transform.position);

                    if(closestEnemyDist > thisDistance) { closestEnemyDist = thisDistance; }
                    closestEnemy = hitCollider;
                    enemies.Add(hitCollider);
                }
            }

            // if (currentRoom.roomType() == 1)
            // {
            //     Debug.Log("WE FOUND THE END ROOM");
            // }

            // Find next room
            if ( currentRoom.isCleared() || currentRoom.roomType() == 2 ) 
            { 
                if (currentRoom.roomType() == 2)
                {
                    // Debug.Log(LevelGeneration.Inst().m_roomsOpened);
                }

                
                if (LevelGeneration.Inst().m_roomsOpened.Count > 0)
                {
                    currentRoom = LevelGeneration.Inst().m_roomsOpened.Dequeue().gameObject.GetComponentInParent<ProtoRoom>();
                    Debug.Log("This room has been dequeued");
                    Debug.Log(currentRoom);
                } 
                else
                {
                    // if no enemies, NavAgent.GoTo(endRoom.gameObject.transform.position)
                    bool foundEnemy = false;
                    foreach (var hitCollider in Physics.OverlapSphere(gameObject.transform.position, 10000))
                    {
                        if(BaseEnemy.isEnemyAndAlive(hitCollider)) 
                        {
                            foundEnemy = true;
                            break;
                        }
                    } 
                    if(foundEnemy == false)
                    {
                        currentRoom = endRoom;
                        // Debug.Log("NO ENEMIES FOUND");
                    }      
                }    
            }




            // CONTROL MOVEMENT OF PLAYER
            m_attack = true;
            if(closestEnemyDist < 8)
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

                if((currentRoom.roomType() == 1 && Vector3.Magnitude(currentRoom.gameObject.transform.position - gameObject.transform.position) < 1) || foundEndRoom == true)
                {
		            // Behavior if it is the endroom to leave the level
                    // if(m_state != 5) {NavAgent.Teleport(gameObject.transform.position); }
                    foundEndRoom = true;
                    Debug.Log("asdf");
                    Target3D = new Vector3(0, 0, 1);

                    // NavAgent.GoTo(gameObject.transform.position + new Vector3(0, 0, 20));
                    NavAgent.Teleport(gameObject.transform.position);
                    m_state = 5;
                }
                else if(currentRoom.roomType() == 0)
                {
                    // if(m_state != 6) {NavAgent.Teleport(gameObject.transform.position); }

                    Target3D = new Vector3(0, 0, 2);
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


            // Vector3 move = NavAgent.getPosition() - gameObject.transform.position;
            // move.y = gameObject.transform.position.y;

            // gameObject.transform.position = gameObject.transform.position + .25F * move;

            if(foundEndRoom) 
            { 
                gameObject.transform.position = gameObject.transform.position + .2F * Vector3.forward;
            }
            else
            {
                Vector3 move = NavAgent.getPosition();
                move.y = gameObject.transform.position.y;
                gameObject.transform.position = move;
            }

            if(Vector3.Magnitude(NavAgent.getPosition() - gameObject.transform.position) > 2F) NavAgent.Teleport(gameObject.transform.position);
            m_displayState = m_state;




            // CONTROL ROTATION OF PLAYER
            m_mouseValue = Vector3.zero;

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
            if(hit.collider != null && BaseEnemy.CheckIfEnemy(hit.collider)) {} else{m_mouseValue.x = Angle;}
            rotation = m_camera.transform.rotation.eulerAngles.x;
            if(180 >= rotation && rotation >= 1) m_mouseValue.y = 1; 
            if(359 >= rotation && rotation > 180) m_mouseValue.y = -1; 
            m_displayAngle = Angle;
        }
    }



    /*
     * Tell whether demo mode is on 
     *
     * Returns:
     * bool -- True if demo mode is on
     */
    public static bool On() { if (m_isOn) return true; return false; }

    public static void ChangeDemoModeMode(bool val) { m_isOn = val;} 


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
            // Debug.Log("Demo Mode Turned Off.");
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

    public void ToggleIsOn()
    {
        m_isOn = !m_isOn;
    }
}