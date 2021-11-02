/*
 * Filename: ProtoRoom.cs
 * Developer: Zachariah Preston
 * Purpose: Abstract factory for the maze generation that later creates an actual room.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Room type list
    [-1] Undefined
    [0] Start
    [1] End
    [2] Treasure
    [3] Small
    [4] Medium
    [5] Large
*/


/*
 * Abstract factory for the maze generation that later creates an actual room.
 *
 * Member variables:
 * m_startRoom -- GameObject for the start room prefab.
 * m_endRoom -- GameObject for the end room prefab.
 * m_treasureRoom -- GameObject for the treasure room prefab.
 * m_smallRoom -- GameObject for the small room prefab.
 * m_mediumRoom -- GameObject for the medium room prefab.
 * m_largeRoom -- GameObject for the large room prefab.
 * m_room -- Room object that the prefabs will create when instantiated.
 * m_xPos -- Integer for the room's x position within the level layout grid.
 * m_zPos -- Integer for the room's z position within the level layout grid.
 * m_roomType -- Integer for the type of room, used to decide what room parts to add.
 * m_doorList -- Array of booleans that represent if each wall has a door or not.
 * m_roomSpread -- Integer for the distance between the center of each room.
 */
public class ProtoRoom : MonoBehaviour
{
    public GameObject m_startRoom, m_endRoom, m_treasureRoom, m_smallRoom, m_mediumRoom, m_largeRoom;

    private Room m_room;
    private int m_xPos = 0, m_zPos = 0, m_roomType = -1;
    private bool[] m_doorList = new bool[] {false, false, false, false};
    private const int m_roomSpread = 44;

    /* Sets up the abstract factory by setting position and choosing which room to instantiate.
     *
     * Parameters:
     * mazeWidth -- Integer for the width of the maze.
     */
    public void Init(int mazeWidth)
    {
        // Set transform from positions.
        Vector3 roomPos = Vector3.zero;
        roomPos.x = (m_xPos - ((mazeWidth - 1) / 2)) * m_roomSpread;
        roomPos.z = (m_zPos + 1) * m_roomSpread;
        transform.position = roomPos;

        // Create room based on type.
        if (GetRoomType() < 0)
        {
            Debug.LogError("Error: ProtoRoom has undefined type in Init().");
        }
        else if (GetRoomType() == 0)
        {
            m_startRoom = Instantiate(m_startRoom, transform);
            m_room = m_startRoom.GetComponent<StartRoom>();
        }
        else if (GetRoomType() == 1)
        {
            m_endRoom = Instantiate(m_endRoom, transform);
            m_room = m_endRoom.GetComponent<EndRoom>();
        }
        else if (GetRoomType() == 2)
        {
            m_treasureRoom = Instantiate(m_treasureRoom, transform);
            m_room = m_treasureRoom.GetComponent<Room>();
        }
        else if (GetRoomType() == 3)
        {
            m_smallRoom = Instantiate(m_smallRoom, transform);
            m_room = m_smallRoom.GetComponent<Room>();
        }
        else if (GetRoomType() == 4)
        {
            m_mediumRoom = Instantiate(m_mediumRoom, transform);
            m_room = m_mediumRoom.GetComponent<Room>();
        }
        else if (GetRoomType() == 5)
        {
            m_largeRoom = Instantiate(m_largeRoom, transform);
            m_room = m_largeRoom.GetComponent<Room>();
        }

        // Initialize room.
        m_room.Init(m_doorList, m_xPos, m_zPos);
    }

    /* Sets the room's position.
     *
     * Parameters:
     * x -- Integer for the room's x position within the level layout grid.
     * z -- Integer for the room's z position within the level layout grid.
     */
    public void SetPosition(int x, int z)
    {
        m_xPos = x;
        m_zPos = z;
    }

    /* Sets the room's type.
     *
     * Parameters:
     * t -- Integer for the room's type.
     */
    public void SetRoomType(int t)
    {
        m_roomType = t;
    }

    /* Sets the room's doors.
     *
     * Parameters:
     * d -- Array of booleans for the room's doors.
     */
    public void SetDoors(bool[] d)
    {
        m_doorList = d;
    }

    /* Gets the room's position.
     *
     * Returns:
     * Vector3Int -- Room's position within the level layout grid.
     */
    public Vector3Int GetPosition()
    {
        Vector3Int temp = Vector3Int.zero;
        temp.x = m_xPos;
        temp.z = m_zPos;
        return temp;
    }

    /* Gets the room's type.
     *
     * Returns:
     * int -- The room's type.
     */
    public int GetRoomType()
    {
        return m_roomType;
    }

    /* Gets the room's doors.
     *
     * Returns:
     * bool[] -- Whether or not a door exists for each of the 4 walls of the room.
     */
    public bool[] GetDoors()
    {
        return m_doorList;
    }
}

