using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour
{
    [SerializeField] public bool CurrentlyActive;

    public static GameObject m_camera, m_player, m_thisGameObject;
    public static NavMeshAgent m_agent;
    public static Vector2 destination, position;

    void Start()
    {
        m_thisGameObject = gameObject;
        m_player = GameObject.Find("Player");
        m_camera = GameObject.Find("Main Camera");
        m_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        gameObject.transform.position = m_player.transform.position;
        m_agent.autoTraverseOffMeshLink = false;
        m_agent.speed = 15F;
        destination = new Vector3(0, 0, 20);
        position = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {    
        if(position != Vector2.zero)
        {
            gameObject.transform.position = new Vector3(position.x, gameObject.transform.position.y, position.y);
            position = Vector2.zero;
        }

        m_agent.SetDestination(new Vector3(destination.x, gameObject.transform.position.y, destination.y));
    }

    public static void Teleport(Vector3 location) { position = new Vector2(location.x, location.z); }

    public static void Teleport(int x, int z) { position = new Vector2(x, z); }

    public static void GoTo(Vector3 location) { destination = new Vector2(location.x, location.z); }

    public static void GoTo(int x, int z) { destination = new Vector2(x, z); }

    public static Vector3 getPosition() {return m_thisGameObject.transform.position;}
}
