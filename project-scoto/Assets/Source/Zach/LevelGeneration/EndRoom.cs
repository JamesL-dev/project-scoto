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
    /* Sets up the room by creating walls and doors.
     *
     * Parameters:
     * d -- Door list from ProtoRoom.
     * x -- X position from ProtoRoom.
     * z -- Z position from ProtoRoom.
     */
    public override void Init(bool[] d, int x, int z)
    {
        // Store the values from the ProtoRoom.
        m_xPos = x;
        m_zPos = z;
        m_doorList = d;

        // Generate walls.
        for (int i = 0; i < 4; i++)
        {
            // Create new plain wall.
            GameObject tempWall;
            tempWall = Instantiate(m_wall, this.transform);
            tempWall.transform.position += m_wallPositions[i] * m_wallPosMultiplier;
            tempWall.transform.eulerAngles = m_wallRotations[i];
            tempWall.tag = m_wall.tag;

            // Add wall to array.
            m_wallList[i] = tempWall;
        }

        // Start with room cleared.
        m_isCleared = true;
        for (int i = 0; i < 4; i++)
        {
            if (m_doorList[i])
                OpenDoorInRoom(i);
        }
    }
}

