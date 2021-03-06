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
* m_interactAction -- What happens when object is interacted with
* 
*/
public class InteractableBox : MonoBehaviour
{
    public bool m_isInRange;
    public UnityEvent m_interactAction;
    public LootTable thisLoot;
    bool IsLooted = false;

    void Start()
    {

    }

    void Update()
    {

    }
    
    /* Function to generate loot from loot table
    *
    *
    * Returns: none
    */    
    private void MakeLoot()
    {
        if (IsLooted == false)
        {
            if (thisLoot != null)
            {
                PowerUp current = thisLoot.LootPowerup();
                if (current != null)
                {
                    Instantiate(current.gameObject, transform.position + new Vector3(-0.6f, 2f, 0), Quaternion.identity);
                    Debug.Log("Spawning " + current.gameObject.name);
                    IsLooted = true;
                }
            }
        }
        else
        {
            Debug.Log("InteractableBox#MakeLoot: Chest has been looted");
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
            //Debug.Log("Player now in range");
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
            //Debug.Log("Player now not in range");
        }
    }

    /* Function to handle interact action
    *
    * Parameters:
    *
    * Returns: none
    */   
    private void OnInteract()
    {
        if(m_isInRange) // Check if in range to interact
        {
            string objectName = gameObject.name;
            Debug.Log(objectName + " was interacted with");
            MakeLoot();
        }
    }
}
