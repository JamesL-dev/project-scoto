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
        
        // Spawn player.
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().Tp(new Vector3(0, 0, -5));

        // Start with room cleared.
        m_isCleared = true;
        for (int i = 0; i < 4; i++)
        {
            if (m_doorList[i])
                OpenDoorInRoom(i);
        }

        // Open the connected doors in adjacent rooms.
        if (m_doorList[0])
        {
            // North door, so open south door in room with z + 1.
            Room tempRoom = LevelGeneration.Inst().GetRoom(m_xPos, m_zPos + 1);
            if (tempRoom != null)
                tempRoom.OpenDoorInRoom(2);
        }
        if (m_doorList[1])
        {
            // East door, so open west door in room with x + 1.
            Room tempRoom = LevelGeneration.Inst().GetRoom(m_xPos + 1, m_zPos);
            if (tempRoom != null)
                tempRoom.OpenDoorInRoom(3);
        }
        if (m_doorList[2])
        {
            // South door, so open north door in room with z - 1.
            Room tempRoom = LevelGeneration.Inst().GetRoom(m_xPos, m_zPos - 1);
            if (tempRoom != null)
                tempRoom.OpenDoorInRoom(0);
        }
        if (m_doorList[3])
        {
            // West door, so open east door in room with x - 1.
            Room tempRoom = LevelGeneration.Inst().GetRoom(m_xPos - 1, m_zPos);
            if (tempRoom != null)
                tempRoom.OpenDoorInRoom(1);
        }
    }
}

