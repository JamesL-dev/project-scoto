using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour
{
    [SerializeField] public bool CurrentlyActive;

    public static GameObject m_camera, m_player, m_thisGameObject;
    public static NavMeshAgent m_agent;
    public static bool Active;
    public static Vector3 destination;

    private static bool deactivated;

    void Start()
    {
        m_thisGameObject = GetComponent<GameObject>();
        m_player = GameObject.Find("Player");
        m_camera = GameObject.Find("Main Camera");
        m_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        gameObject.transform.position = m_player.transform.position;
        m_agent.autoTraverseOffMeshLink = false;
        m_agent.speed = 2F;
        deactivated = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(deactivated)
        {
            Active = false;
            deactivated = false;
            gameObject.transform.position = Vector3.zero;
            m_agent.SetDestination(Vector3.zero);
        }
        
        if(Active)
        {
            m_agent.SetDestination(destination);
        }

        CurrentlyActive = Active;

    }

    public static void GoTo(Vector3 location)
    {
        destination = location;
    }

    public static void SetActive(bool value)
    {
        if(value && !Active)
        {
            Active = true;
            m_thisGameObject.transform.position = m_player.transform.position;
        }
        if(!value && Active)
        {
            deactivated = true;
        }
    }
}
