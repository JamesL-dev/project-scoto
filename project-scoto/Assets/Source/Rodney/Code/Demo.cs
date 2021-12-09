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
    [SerializeField] public static NavAgent theNavAgent;
    [SerializeField] public static GameObject m_camera;
    [SerializeField] public bool m_isSuccessMode;
    public static bool m_isOn;
    public const int radius1 = 25;
    public static ProtoRoom currentRoom;
    public static ProtoRoom endRoom;

    private float closestEnemyDist, Angle;
    private static bool m_jump, m_sprint, m_attack, foundEndRoom;
    private static float rotation;
    private static Vector3 m_mouseValue;

    void Awake()
    {
        m_isOn = false;
        theNavAgent = GameObject.Find("DemoPathfinder").GetComponent<NavAgent>();
        m_camera = GameObject.Find("Main Camera");
        m_jump = m_sprint = foundEndRoom = false;
        m_isSuccessMode = m_attack = true;
    }

    void FixedUpdate()
    {
        if(On())
        {
            if(m_isSuccessMode) { PlayerData.Inst().TakeHealth(1F); }
            Flashlight.Inst().AddBattery(1F);
            Vector3 Target3D = Vector3.zero;
            m_attack = false;        

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

            // Find next room
            if ( currentRoom.isCleared() || currentRoom.roomType() == 2 ) 
            {             
                if (LevelGeneration.Inst().m_roomsOpened.Count > 0)
                {
                    currentRoom = LevelGeneration.Inst().m_roomsOpened.Dequeue().gameObject.GetComponentInParent<ProtoRoom>();
                } 
                else
                {
                    bool foundEnemy = false;
                    foreach (var hitCollider in Physics.OverlapSphere(gameObject.transform.position, 10000))
                    {
                        if(BaseEnemy.isEnemyAndAlive(hitCollider)) { foundEnemy = true; break; }
                    } 
                    if(foundEnemy == false) { currentRoom = endRoom; }      
                }    
            }




            // CONTROL MOVEMENT OF PLAYER
            Target3D = closestEnemy.transform.position - gameObject.transform.position;
            if(closestEnemyDist < 8)
            {  
                if(m_isSuccessMode) 
                { 
                    NavAgent.GoTo(gameObject.transform.position - closestEnemy.transform.position); 
                }
            }
            else if(closestEnemyDist < 12) 
            {
                if(!m_isSuccessMode) { NavAgent.GoTo(closestEnemy.transform.position);}
            }
            else if(closestEnemyDist < radius1)
            {
                NavAgent.GoTo(closestEnemy.transform.position);
            } 
            else if(currentRoom.roomType() == 1 && Vector3.Magnitude(currentRoom.gameObject.transform.position - gameObject.transform.position) < 1)
            {
                foundEndRoom = true;
            }
            else if(currentRoom.roomType() == 0)
            {
                Target3D = new Vector3(0, 0, 1);
                NavAgent.GoTo(new Vector3(0, 0, 35));
            }
            else
            {
                Target3D = NavAgent.getPosition() - gameObject.transform.position;
                NavAgent.GoTo(currentRoom.gameObject.transform.position);
            }                             


            if(foundEndRoom) 
            { 
                gameObject.transform.position = gameObject.transform.position + .15F * Vector3.forward;
                Target3D = new Vector3(0, 0, 1);
            }
            else
            {
                Vector3 move = NavAgent.getPosition();
                move.y = gameObject.transform.position.y;
                gameObject.transform.position = move;
            }



            // CONTROL ROTATION OF PLAYER
            m_mouseValue = Vector3.zero;
            Vector2 Target, Current;
            Target.x = Target3D.x; 
            Target.y = Target3D.z; 
            Current.x = gameObject.transform.forward.x;
            Current.y = gameObject.transform.forward.z;
            Angle = 1.5F*Vector2.SignedAngle(Target, Current);
            Angle = (Angle > 25 ? 25 : Angle);
            Angle = (Angle < -25 ? -25 : Angle);

            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0)), out hit, radius1);
            if(hit.collider != null && BaseEnemy.CheckIfEnemy(hit.collider)) 
            {
                if(m_isSuccessMode) { m_attack = true; }
            } 
            else
            {
                m_mouseValue.x = .5F*Angle;
            }
            rotation = m_camera.transform.rotation.eulerAngles.x;
            if(180 >= rotation && rotation > 1) m_mouseValue.y = 1 + rotation/10; 
            if(359 > rotation && rotation > 180) m_mouseValue.y = -1 - (360-rotation)/10; 
        }
        else
        {
            NavAgent.Teleport(gameObject.transform.position); 
            NavAgent.GoTo(gameObject.transform.position);
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

    }


    public void ToggleIsOn()
    {
        m_isOn = !m_isOn;
    }
}