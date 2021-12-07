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
    [SerializeField] public bool m_lookingAtEnemy;
    public const int radius1 = 24;
    public static ProtoRoom currentRoom;

    private float closestEnemyDist;
    private static bool m_jump, m_sprint, m_isSuccessMode, m_attack;
    private static int m_slackTime, m_counter;
    private static float rotation;
    private static Vector2 m_moveValue;
    private static Vector3 m_mouseValue;

    void Awake()
    {
        theNavAgent = GameObject.Find("DemoPathfinder").GetComponent<NavAgent>();
        m_jump = m_sprint = false;
        m_isSuccessMode = true;
        m_attack = true;
        m_moveValue = Vector2.zero;
        m_counter = 0;
        if(m_slackSeconds < 1) {Debug.LogError("m_slackSeconds is to low. Must be 1 seconds or greater"); m_slackSeconds = 10;}
        m_slackTime = m_slackSeconds * 60;
        // StartCoroutine(Init());
    }

    void FixedUpdate()
    {
        m_counter++; 
        m_attack = false;
        if(m_counter == m_slackTime) { Debug.Log("Demo Mode Turned On"); NavAgent.SetActive(true);}
        if (currentRoom.isCleared()) 
        {
            currentRoom = LevelGeneration.Inst().m_roomsOpened[LevelGeneration.Inst().m_roomsOpened.Count - 1].gameObject.GetComponentInParent<ProtoRoom>();
        }

        Vector3 debugVec = Vector3.zero;

        if(On())
        {
            m_attack = true;
            m_moveValue = Vector2.zero;
            m_mouseValue = Vector3.zero;

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

            Vector3 deltaPos = closestEnemy.transform.position - gameObject.transform.position;
            float angleBetween = Vector3.Angle(transform.forward, deltaPos);

            // Always look straight forward. Not up or down
            if(180 >= rotation && rotation >= 1) m_mouseValue.y = 1; 
            if(359 >= rotation && rotation > 180) m_mouseValue.y = -1; 

            // Vector3.Angle only returns positive angle, so spin right as long as we arent looking at enemy
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 20);
            m_lookingAtEnemy = false;
            if(hit.collider != null)
            {
                BaseEnemy enemy = BaseEnemy.CheckIfEnemy(hit.collider);
                if (enemy) { m_lookingAtEnemy = true; }
            }


            if(closestEnemyDist < 6)
            {  
                m_displayState = 1;
                NavAgent.SetActive(false);

                m_moveValue = new Vector2(range(gameObject.transform.position.x - closestEnemy.transform.position.x, 1), 
                    range(gameObject.transform.position.x - closestEnemy.transform.position.z, 1));
            }
            else if(closestEnemyDist < 12) 
            {    
                m_displayState = 2;
                NavAgent.SetActive(false);
                // Vector3 destination = gameObject.transform.position - closestEnemy;
                // m_agent.SetDestination(gameObject.transform.position + 2*destination);   
            } 
            else if(closestEnemyDist < radius1)
            {
                m_displayState = 3;
                NavAgent.SetActive(true);

                m_moveValue = new Vector2(range(theNavAgent.transform.position.x - gameObject.transform.position.x, 1), 
                    range(theNavAgent.transform.position.z - gameObject.transform.position.z, 1));
                NavAgent.GoTo(closestEnemy.transform.position);
            }
            else if(closestEnemyDist == radius1)
            {
                m_displayState = 4;
                NavAgent.SetActive(true);

                
                m_moveValue = new Vector2(range(theNavAgent.transform.position.x - gameObject.transform.position.x, 1), 
                    range(theNavAgent.transform.position.z - gameObject.transform.position.z, 1));
                NavAgent.GoTo(currentRoom.gameObject.transform.position);

                if(currentRoom.roomType() == 1 && Vector3.Magnitude(currentRoom.gameObject.transform.position - gameObject.transform.position) < 1)
                {
		            // Behavior if it is the endroom to leave the level
                    m_displayState = 5;
                    Debug.Log("AT END ROOM");
                    NavAgent.GoTo(gameObject.transform.position + new Vector3(0, 0, 1));
                }
            }  


            if(Vector3.Magnitude(theNavAgent.transform.position - gameObject.transform.position) > 2F)
            {
                theNavAgent.transform.position = gameObject.transform.position;
            }
        }
    }

    // /*
    //  * Finds the next room
    //  */
    // public void NextRoom()
    // {
	//     int x = currentRoom.xPosition();
	//     int z = currentRoom.zPosition();

	//     bool[] doors = LevelGeneration.Inst().m_roomMatrix[x][z].GetDoors();
	//     if(doors[0]) x++;
	//     else if(doors[1]) z++;
	//     else if(doors[2]) x--;
	//     else if(doors[3]) z--;

    //     currentRoom = LevelGeneration.Inst().m_roomMatrix[x][z];
    // }


    /*
     * Returns minmum float value
     */
    private float range(float variable, float range)
    {
        if(variable >= 0 && variable > range) {return range;}
        if(variable < 0 && variable < -1 * range) {return -1*range;}
        return variable;
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
        return m_moveValue;
    }

    /*
     * Tells demo mode that a player input has occured
     */
    public static void ResetTimer() 
    { 
        if(On())
        {
            Debug.Log("Demo Mode Turned Off.");
            NavAgent.SetActive(false);
        }
        Demo.m_counter = 0; 
    }

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

