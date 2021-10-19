/*
 * Filename: StartRoom.cs
 * Developer: Zachariah Preston
 * Purpose: A subclass of Room for the room that the player spawns in for each level.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * A subclass of Room for the room that the player spawns in for each level.
 */
public class StartRoom : Room {
    /* Sets up the start room by creating walls, doors, and room parts and spawning the player.
     *
     * Parameters:
     * mazeWidth -- Integer for the width of the maze.
     * mazeHeight -- Integer for the height of the maze.
     */
    public override void Setup(int mazeWidth = 0, int mazeHeight = 0) {
        // Generate walls.
        for (int i = 0; i < 4; i++) {
            // Create new wall.
            GameObject tempWall;
            if (m_doorList[i]) {
                tempWall = Instantiate(m_wallDoor, this.transform);
                tempWall.transform.position = m_wallPositions[i];
                tempWall.transform.eulerAngles = m_wallRotations[i];
                tempWall.tag = m_wallDoor.tag;
            } else {
                tempWall = Instantiate(m_wall, this.transform);
                tempWall.transform.position = m_wallPositions[i];
                tempWall.transform.eulerAngles = m_wallRotations[i];
                tempWall.tag = m_wall.tag;
            }

            // Add wall to array.
            m_wallList[i] = tempWall;
        }
        
        // DEBUG: Create test pickup.
        m_pickup = Instantiate(m_pickup, transform);
        m_pickup.transform.position = (transform.position + new Vector3(0, 1, 0));

        // Spawn player.
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().Tp(new Vector3(0, 0, -5));
    }
}

