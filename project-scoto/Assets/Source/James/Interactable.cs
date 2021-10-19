using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool m_isInRange;
    public KeyCode m_interactKey;
    public UnityEvent m_interactAction;

    void Start()
    {
        
    }

    void Update()
    {
        if(m_isInRange) // Check if in range to interact
        {

        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            m_isInRange = true;
            Debug.Log("Player now in range");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            m_isInRange = false;
            Debug.Log("Player now not in range");
        }
    }
}
