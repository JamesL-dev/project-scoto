/*
 * Filename: Room.cs
 * Developer: Zachariah Preston
 * Purpose: Represents one room in the level, including its doors and any room parts within it.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Represents one room in the level, including its doors and any room parts within it.
 *
 * Member variables:
 * m_wall -- GameObject for one wall without a door.
 * m_wallDoor -- GameObject for one wall with a door.
 * m_wallList -- Array of GameObjects for the 4 walls of a room.
 * m_wallPosMultiplier -- Float for the amount to space out the walls depending on the room size.
 * m_opener -- GameObject for a test pickup to open doors.
 * m_wallPositions -- Vector3 for the preset positions of the 4 walls of a room.
 * m_wallRotations -- Vector3 for the preset rotations of the 4 walls of a room.
 * m_doorList -- Array of booleans copied from the room's factory.
 * m_isCleared -- Boolean that stores if a room has been cleared or not.
 * m_timer -- Integer for the number of frames since the room was created.
 */
public class Room : MonoBehaviour
{
    public GameObject m_wall, m_wallDoor;
    public GameObject[] m_wallList = new GameObject[4];
    public float m_wallPosMultiplier;

    protected Vector3[] m_wallPositions = new Vector3[4], m_wallRotations = new Vector3[4];
    protected bool[] m_doorList = new bool[] {false, false, false, false};
    protected bool m_isCleared = false;
    protected int m_timer = 0;

    /* Sets preset values for wall positions and rotations.
     */
    void Awake()
    {
        // Set starting values for position and rotation arrays.
        SetTransformArrays();
    }

    /* Detects if the room's doors should be opened.
     */
    void Update()
    {
        // Wait for a bit after the room is created so that the doors don't open before the enemies have spawned.
        if (m_timer < 30)
        {
            m_timer++;
            return;
        }

        // Detect if all enemies are defeated.
        int enemyCount = 0;
        EnemySpawner enemySpawner = GetComponentInChildren<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemyCount = enemySpawner.GetEnemyCount();
        }

        // If so, open the doors.
        if (enemyCount == 0 && !m_isCleared)
        {
            m_isCleared = true;
            for (int i = 0; i < 4; i++)
            {
                if (m_doorList[i])
                {
                    // Delete old wall.
                    Destroy(m_wallList[i].gameObject);

                    // Create new wall with a door.
                    GameObject tempWall;
                    tempWall = Instantiate(m_wallDoor, this.transform);
                    tempWall.transform.position += m_wallPositions[i] * m_wallPosMultiplier;
                    tempWall.transform.eulerAngles = m_wallRotations[i];
                    tempWall.tag = m_wallDoor.tag;

                    // Add wall to array.
                    m_wallList[i] = tempWall;

                    // Open door.
                    m_wallList[i].GetComponentInChildren<Door>().OpenDoor();
                }
            }
        }
    }

    /* Sets up the room by creating walls and doors and spawning the player.
     *
     * Parameters:
     * d -- Door list from ProtoRoom.
     */
    public virtual void Init(bool[] d)
    {
        // Store the door list.
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
    }

    /* Sets preset values for wall positions and rotations.
     */
    protected void SetTransformArrays()
    {
        m_wallPositions[0] = new Vector3(0, 0, 10);
        m_wallPositions[1] = new Vector3(10, 0, 0);
        m_wallPositions[2] = new Vector3(0, 0, -10);
        m_wallPositions[3] = new Vector3(-10, 0, 0);

        m_wallRotations[0] = new Vector3(-90, 0, 0);
        m_wallRotations[1] = new Vector3(-90, 90, 0);
        m_wallRotations[2] = new Vector3(-90, 180, 0);
        m_wallRotations[3] = new Vector3(-90, -90, 0);
    }
}

