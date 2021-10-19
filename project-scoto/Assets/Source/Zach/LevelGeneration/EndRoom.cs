/*
 * Filename: EndRoom.cs
 * Developer: Zachariah Preston
 * Purpose: A subclass of Room for the final room of each level that takes the player to the next one.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * A subclass of Room for the final room of each level that takes the player to the next one.
 */
public class EndRoom : Room
{
    /* Sets up the end room by setting position and creating walls and doors.
     *
     * Parameters:
     * mazeWidth -- Integer for the width of the maze.
     * mazeHeight -- Integer for the height of the maze.
     */
    public override void Setup(int mazeWidth = 0, int mazeHeight = 0)
    {
        // Generate walls.
        for (int i = 0; i < 4; i++)
        {
            // Create new wall.
            GameObject tempWall;
            if (m_doorList[i])
            {
                tempWall = Instantiate(m_wallDoor, this.transform);
                tempWall.transform.position = m_wallPositions[i];
                tempWall.transform.eulerAngles = m_wallRotations[i];
                tempWall.tag = m_wallDoor.tag;
            }
            else
            {
                tempWall = Instantiate(m_wall, this.transform);
                tempWall.transform.position = m_wallPositions[i];
                tempWall.transform.eulerAngles = m_wallRotations[i];
                tempWall.tag = m_wall.tag;
            }

            // Add wall to array.
            m_wallList[i] = tempWall;
        }

        // Set transform from positions.
        Vector3 roomPos = Vector3.zero;
        roomPos.x = (m_xPos - ((mazeWidth - 1) / 2)) * 20;
        roomPos.z = (m_zPos + 1) * 20;
        transform.position = roomPos;
    }
}

