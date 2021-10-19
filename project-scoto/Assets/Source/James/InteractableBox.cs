/*
* Filename: InteractableBox.cs
* Developer: James Lasso
* Purpose: Add an interactable box space around object
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Class containing Interaction features
*
* Member variables:
* m_isInRange -- boolean to signify if player is in range of interacting
* m_inTeracTKey -- Key used to interact
* m_interactAction -- What happens when object is interacted with
* 
*/
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
    
    /* Function to see if player is in range of the box
    *
    * Parameters:
    * collision -- Player collision box
    *
    * Returns: none
    */    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            m_isInRange = true;
            Debug.Log("Player now in range");
        }
    }

    /* Function to see if player is not in range of the box
    *
    * Parameters:
    * collision -- Player collision box
    *
    * Returns: none
    */    
    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            m_isInRange = false;
            Debug.Log("Player now not in range");
        }
    }
}
