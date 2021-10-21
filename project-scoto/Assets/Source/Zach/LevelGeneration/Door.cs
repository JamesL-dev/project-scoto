/*
 * Filename: Door.cs
 * Developer: Zachariah Preston
 * Purpose: Controls the opening of doors when a room is cleared.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the opening of doors when a room is cleared.
 */
public class Door : MonoBehaviour
{
    /* Opens the door.
     */
    public void OpenDoor()
    {
        Destroy(gameObject);
    }
}

