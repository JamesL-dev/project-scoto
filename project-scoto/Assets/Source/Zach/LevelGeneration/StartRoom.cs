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
public class StartRoom : Room
{
    /* Sets up the room by creating walls and doors and spawning the player.
     *
     * Parameters:
     * d -- Door list from ProtoRoom.
     * t -- Room type from ProtoRoom.
     */
    public override void Init(bool[] d, int t)
    {
        // Store the door list.
        m_doorList = d;

        // Generate walls.
        for (int i = 0; i < 4; i++)
        {
            // Create new wall.
            GameObject tempWall;
            if (m_doorList[i])
            {
                // Wall with a door.
                tempWall = Instantiate(m_wallDoor, this.transform);
                tempWall.transform.position += m_wallPositions[i];
                tempWall.transform.eulerAngles = m_wallRotations[i];
                tempWall.tag = m_wallDoor.tag;
            }
            else
            {
                // Plain wall.
                tempWall = Instantiate(m_wall, this.transform);
                tempWall.transform.position += m_wallPositions[i];
                tempWall.transform.eulerAngles = m_wallRotations[i];
                tempWall.tag = m_wall.tag;
            }

            // Add wall to array.
            m_wallList[i] = tempWall;
        }

        // Spawn player.
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().Tp(new Vector3(0, 0, -5));
    }
}

