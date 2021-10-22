/*
 * Filename: Door.cs
 * Developer: Zachariah Preston
 * Purpose: Controls the opening of doors when a room is cleared.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Controls the opening of doors when a room is cleared.
 */
public class Door : MonoBehaviour
{
    /* Opens the door.
     */
    public void OpenDoor()
    {
        GameObject link = new GameObject("link");
        link.transform.parent = transform;
        NavMeshLink meshLink = link.AddComponent<NavMeshLink>();

        meshLink.startPoint = new Vector3(0.0f, -2.5f,  1f);
        meshLink.startPoint += transform.position;
        meshLink.endPoint = new Vector3(0.0f, -2.5f, -1f);
        meshLink.endPoint += transform.position;
        meshLink.width = 2;
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }
}

