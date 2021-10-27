/*
 * Filename: Room.cs
 * Developer: Zachariah Preston
 * Purpose: Represents one room in the level, including its doors and any room parts within it.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Room type list (WIP)
    [-1] Undefined
    [0] Empty
    [1] Start room
    [2] End room
    [3] Treasure room
    [4] ...
*/


/*
 * Represents one room in the level, including its doors and any room parts within it.
 *
 * Member variables:
 * m_enemySpawner -- GameObject for creating enemies.
 * m_pickupSpawner -- GameObject for creating pickups.
 * m_wall -- GameObject for one wall without a door.
 * m_wallDoor -- GameObject for one wall with a door.
 * m_wallList -- Array of GameObjects for the 4 walls of a room.
 * m_opener -- GameObject for a test pickup to open doors.
 * m_doorList -- Array of booleans that represent if each wall has a door or not.
 * m_xPos -- Integer for the room's x position within the level layout grid.
 * m_zPos -- Integer for the room's z position within the level layout grid.
 * m_roomType -- Integer for the type of room, used to decide what room parts to add.
 * m_wallPositions -- Vector3 for the preset positions of the 4 walls of a room.
 * m_wallRotations -- Vector3 for the preset rotations of the 4 walls of a room.
 * m_isCleared -- Boolean that stores if a room has been cleared or not.
 * m_roomSpread -- Integer for the distance between the center of each room.
 */
public class Room : MonoBehaviour
{
    public GameObject m_enemySpawner, m_pickupSpawner, m_wall, m_wallDoor;
    public GameObject[] m_wallList = new GameObject[4];

    // DEBUG
    public GameObject m_opener;

    protected bool[] m_doorList = new bool[] {false, false, false, false};
    protected int m_xPos = 0, m_zPos = 0, m_roomType = -1;
    protected Vector3[] m_wallPositions = new Vector3[4], m_wallRotations = new Vector3[4];
    protected bool m_isCleared = false;
    protected const int m_roomSpread = 24;

    /* Sets preset values for wall positions and rotations.
     */
    protected void Awake()
    {
        // Set starting values for position and rotation arrays.
        SetTransformArrays();
    }

    /* Detects if the room's doors should be opened.
     */
    protected void Update()
    {
        // DEBUG: Detect if pickup is deleted.
        if (m_opener == null && !m_isCleared)
        {
            Debug.Log("Opener deleted in room (" + m_xPos + ", " + m_zPos + ")");
            m_isCleared = true;

            for (int i = 0; i < 4; i++)
            {
                if (m_doorList[i])
                {
                    m_wallList[i].GetComponentInChildren<Door>().OpenDoor();
                }
            }
        }
    }

    /* Sets up the room by setting position and creating walls, doors, and room parts.
     *
     * Parameters:
     * mazeWidth -- Integer for the width of the maze.
     * mazeHeight -- Integer for the height of the maze.
     */
    public virtual void Setup(int mazeWidth = 0, int mazeHeight = 0)
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
        roomPos.x = (m_xPos - ((mazeWidth - 1) / 2)) * m_roomSpread;
        roomPos.z = (m_zPos + 1) * m_roomSpread;
        transform.position = roomPos;

        // Create room parts based on type (just check if type exists, for now)
        if (GetRoomType() < 0)
        {
            Debug.LogError("Error: Room has undefined type in setup().");
        }

        // Create enemy spawner.
        m_enemySpawner = Instantiate(m_enemySpawner, transform);
        m_enemySpawner.transform.position = (transform.position + new Vector3(2, 1, 0));

        // Create pickup spawner.
        m_pickupSpawner = Instantiate(m_pickupSpawner, transform);
        m_pickupSpawner.transform.position = (transform.position + new Vector3(-4, 0, 0));

        // DEBUG: Create test opener.
        m_opener = Instantiate(m_opener, transform);
        m_opener.transform.position = (transform.position + new Vector3(0, 1, 0));
    }

    /* Sets the room's position, doors, and type.
     *
     * Parameters:
     * x -- Integer for the room's x position within the level layout grid.
     * z -- Integer for the room's z position within the level layout grid.
     * d -- Array of booleans for the room's doors.
     * t -- Integer for the room's type.
     */
    public void SetValues(int x, int z, bool[] d, int t)
    {
        SetPosition(x, z);
        SetDoors(d);
        SetRoomType(t);
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

    /* Sets the room's doors.
     *
     * Parameters:
     * d -- Array of booleans for the room's doors.
     */
    public void SetDoors(bool[] d)
    {
        m_doorList = d;
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

    /* Gets the room's doors.
     *
     * Returns:
     * bool[] -- Whether or not a door exists for each of the 4 walls of the room.
     */
    public bool[] GetDoors()
    {
        return m_doorList;
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

    /* Sets preset values for wall positions and rotations.
     */
    protected void SetTransformArrays()
    {
        m_wallPositions[0] = new Vector3(0, 5, 10);
        m_wallPositions[1] = new Vector3(10, 5, 0);
        m_wallPositions[2] = new Vector3(0, 5, -10);
        m_wallPositions[3] = new Vector3(-10, 5, 0);

        m_wallRotations[0] = new Vector3(-90, 0, 0);
        m_wallRotations[1] = new Vector3(-90, 90, 0);
        m_wallRotations[2] = new Vector3(-90, 180, 0);
        m_wallRotations[3] = new Vector3(-90, -90, 0);
    }
}

