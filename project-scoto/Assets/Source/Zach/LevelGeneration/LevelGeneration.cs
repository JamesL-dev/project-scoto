/*
 * Filename: LevelGeneration.cs
 * Developer: Zachariah Preston
 * Purpose: Procedurally generates each level's layout and controls the creation of each room within the level.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Singleton that procedurally generates each level's layout and controls the creation of each room within the level.
 *
 * Member variables:
 * m_protoRoom -- Abstract factory prefab for generating the layout.
 * m_roomMatrix -- 2D List of ProtoRoom prefabs to store the layout.
 * m_instance -- Static intance of itself for the Singleton pattern.
 * m_mwScaling -- Const float for the maze width scaling as the level number increases.
 * m_mhScaling -- Const float for the maze height scaling as the level number increases.
 * m_levelNum -- Class variable int for the current level number.
 * m_roomCount -- Integer for the current number of rooms in the level.
 * m_isBaked -- Boolean for if the level navmesh has been baked yet or not
 */
public sealed class LevelGeneration : MonoBehaviour
{
    public ProtoRoom m_protoRoom;
    public List<List<ProtoRoom>> m_roomMatrix = new List<List<ProtoRoom>>();

    private static LevelGeneration m_instance;
    private const float m_mwScaling = 0.1f, m_mhScaling = 0.2f;
    private static int m_levelNum = 1;
    private int m_roomCount = 0;

    private bool m_isBaked = false;

    /* Generates the level.
     */
    void Start()
    {
        //////////////// PART 1: Generate level ////////////////

        // Procedurally generate maze layout.
        Inst().GenerateLayout(m_levelNum);

        // Make start room.
        ProtoRoom startRoom = Instantiate(m_protoRoom) as ProtoRoom;
        startRoom.SetPosition((m_roomMatrix.Count - 1) / 2, -1);
        startRoom.SetRoomType(0);
        startRoom.SetDoors(new bool[4] {true, false, false, false});
        startRoom.transform.parent = transform;
        m_roomCount++;

        // Make end room.
        ProtoRoom endRoom = Instantiate(m_protoRoom) as ProtoRoom;
        endRoom.SetPosition((m_roomMatrix.Count - 1) / 2, m_roomMatrix[0].Count);
        endRoom.SetRoomType(1);
        endRoom.SetDoors(new bool[4] {false, false, true, false});
        endRoom.transform.parent = transform;
        m_roomCount++;

        // // DEBUG: Print visualization of level.
        // Inst().PrintLevel();


        //////////////// PART 2: Initialize rooms ////////////////
  
        // Loop through matrix to initialize rooms.
        for (int x = 0; x < m_roomMatrix.Count; x++)
        {
            for (int z = 0; z < m_roomMatrix[x].Count; z++)
            {
                // Check if room exists at this location.
                if (m_roomMatrix[x][z] != null)
                {
                    // Choose room types.
                    if (Inst().IsTreasure(x, z))
                    {
                        // If it's a dead-end, make it a treasure room.
                        m_roomMatrix[x][z].SetRoomType(2);
                    }
                    else
                    {
                        // Otherwise, randomly pick a size.
                        int size = Random.Range(3, 6); // returns 3, 4, or 5
                        m_roomMatrix[x][z].SetRoomType(size);
                    }
                
                    // Set up rooms.
                    if (m_roomMatrix[x][z] != null)
                    {
                        m_roomMatrix[x][z].Init(m_roomMatrix.Count);
                    }
                }
            }
        }
        
        // Initialize start room via dynamic binding.
        startRoom.Init(m_roomMatrix.Count);

        // Initialize end room via dynamic binding.
        endRoom.Init(m_roomMatrix.Count);

        NavMeshBaker.CreateLevelMesh();
        m_isBaked = true;
    }

    /* Gets a reference to the instance of the singleton, creating the instance if necessary.
     *
     * Returns:
     * LevelGeneration -- Reference to the level generator.
     */
    public static LevelGeneration Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("LevelGenerator").GetComponent<LevelGeneration>();
        }
        return m_instance;
    }

    /* Sets the level number.
     *
     * Parameters:
     * level -- Integer for the level number to set to.
     */
    public void SetLevelNum(int level)
    {
        if (level < 1)
        {
            Debug.LogWarning("Warning: Level number is less than 1, setting to 1 instead.");
            m_levelNum = 1;
        }
        else if (level > 100)
        {
            Debug.LogWarning("Warning: Level number is over 100, setting to 100 instead.");
            m_levelNum = 100;
        }
        else
        {
            m_levelNum = level;
        }
    }

    /* Returns the boolean m_isBaked
     *
     * Returns:
     * bool - true if navmesh has been baked, false otherwise
     */
    public bool GetIsBaked()
    {
        return m_isBaked;
    }

    /* Gets the level number.
     *
     * Returns:
     * int -- Current level number.
     */
    public int GetLevelNum()
    {
        return m_levelNum;
    }

    /* Gets the room count.
     *
     * Returns:
     * int -- Current number of rooms.
     */
    public int GetRoomCount()
    {
        return m_roomCount;
    }

    /* Gets a reference to a Room object.
     *
     * Parameters:
     * x -- Integer for the room's x position within the level layout grid.
     * z -- Integer for the room's z position within the level layout grid
     *
     * Returns:
     * Room -- Room at the given location. If one doesn't exist, returns null.
     */
    public Room GetRoom(int x, int z)
    {
        if (x < 0 || x >= m_roomMatrix.Count || z < 0 || z >= m_roomMatrix[x].Count)
        {
            // Out of bounds.
            return null;
        }
        else if (m_roomMatrix[x][z] != null)
        {
            // Returns room's Room object.
            return m_roomMatrix[x][z].GetComponentInChildren<Room>();
        }
        else
        {
            // Room doesn't exist.
            Debug.LogError("Error: Attempted to access a room that doesn't exist in GetRoom().");
            return null;
        }
    }

    /* Makes the singleton's constructor static.
     */
    private LevelGeneration() {}

    /* Generates the maze layout.
     *
     * Parameters:
     * level -- Integer for the level number to generate for.
     */
    private void GenerateLayout(int level)
    {
        // Choose the width-height variance.
        float sizeSeed = Random.value;

        // Choose a random starting size, excluding start and end room.
        // Both width and height start as round(random value between 2.5 and 3.5).
        // As the level number increases, the range of width and height increase by 0.2, but width must be odd.
        // The width and height are complementary; a wider level is less likely to be taller, and vice versa.
        int mazeWidth = 3 + 2 * Mathf.RoundToInt((-0.5f + sizeSeed) + (m_mwScaling * (level - 1)));
        int mazeHeight = 3 + Mathf.RoundToInt((0.5f - sizeSeed) + (m_mhScaling * (level - 1)));

        // Determine minimum number of rooms, not counting start and end room.
        // Approximately equal to (width upper bound - 1) * (height upper bound - 1)
        int minRooms = Mathf.RoundToInt((level / 5f + 2f) * (level / 5f + 2f));
        
        // Set up empty level layout.
        for (int x = 0; x < mazeWidth; x++)
        {
            m_roomMatrix.Add(new List<ProtoRoom>());
            for (int z = 0; z < mazeHeight; z++)
            {
                m_roomMatrix[x].Add(null);
            }
        }

        // Declare variables.
        int mazeX = (mazeWidth - 1) / 2;
        int mazeZ = 0;
        List<Vector3Int> mazePath = new List<Vector3Int>();

        // First, create room right after start room.
        mazePath.Add(Inst().CreateRoom(mazeX, mazeZ));
        bool[] tempDoors = m_roomMatrix[mazeX][mazeZ].GetDoors();
        tempDoors[2] = true;
        m_roomMatrix[mazeX][mazeZ].SetDoors(tempDoors);

        // Procedurally generate layout until ending is reached and the number of maze rooms is at least minRooms.
        // (The "- 1" is to account for the start room.)
        int loopCount = 0;
        bool isComplete = false;
        while ((m_roomCount - 1 < minRooms || !isComplete) && loopCount < 10000)
        {
            // Check if surrounding room locations are out of bounds or room already exists.
            bool[] blocked = new bool[4];
            blocked[0] = (mazeZ + 1 >= mazeHeight) || (m_roomMatrix[mazeX][mazeZ + 1] != null);
            blocked[1] = (mazeX + 1 >= mazeWidth) || (m_roomMatrix[mazeX + 1][mazeZ] != null);
            blocked[2] = (mazeZ - 1 < 0) || (m_roomMatrix[mazeX][mazeZ - 1] != null);
            blocked[3] = (mazeX - 1 < 0) || (m_roomMatrix[mazeX - 1][mazeZ] != null);

            // Test for dead end.
            while (blocked[0] && blocked[1] && blocked[2] && blocked[3])
            {
                // If at start of path, force stop.
                if (mazePath.Count == 0)
                {
                    throw new System.Exception("Maze is full before ending is reached in generate_layout().");
                }

                // Backtrack to previous location in path.
                mazeX = mazePath[mazePath.Count - 1].x;
                mazeZ = mazePath[mazePath.Count - 1].z;
                mazePath.RemoveAt(mazePath.Count - 1);

                // Test surrounding locations again.
                blocked[0] = (mazeZ + 1 >= mazeHeight) || (m_roomMatrix[mazeX][mazeZ + 1] != null);
                blocked[1] = (mazeX + 1 >= mazeWidth) || (m_roomMatrix[mazeX + 1][mazeZ] != null);
                blocked[2] = (mazeZ - 1 < 0) || (m_roomMatrix[mazeX][mazeZ - 1] != null);
                blocked[3] = (mazeX - 1 < 0) || (m_roomMatrix[mazeX - 1][mazeZ] != null);
            }

            // Choose a new location, repeat until an empty one is chosen.
            int direction = Random.Range(0, 4); // returns 0, 1, 2, or 3
            while (blocked[direction] && loopCount < 10000)
            {
                direction = Random.Range(0, 4);
                loopCount++;
            }

            // Add door in old room.
            tempDoors = m_roomMatrix[mazeX][mazeZ].GetDoors();
            tempDoors[direction] = true;
            m_roomMatrix[mazeX][mazeZ].SetDoors(tempDoors);

            // Go to new room location and create a room.
            if (direction == 0)
            {
                // North
                mazeZ += 1;
            }
            else if (direction == 1)
            {
                // East
                mazeX += 1;
            }
            else if (direction == 2)
            {
                // South
                mazeZ -= 1;
            }
            else if (direction == 3)
            {
                // West
                mazeX -= 1;
            }
            else
            {
                // Bad value.
                throw new System.Exception("Invalid direction in generate_layout().");
            }
            mazePath.Add(Inst().CreateRoom(mazeX, mazeZ));

            // Add door in new room.
            tempDoors = m_roomMatrix[mazeX][mazeZ].GetDoors();
            tempDoors[(direction + 2) % 4] = true;
            m_roomMatrix[mazeX][mazeZ].SetDoors(tempDoors);

            // Test for end of maze. If so, add door to end room in the last room.
            if ((mazeX == (mazeWidth - 1) / 2) && (mazeZ == mazeHeight - 1))
            {
                tempDoors = m_roomMatrix[mazeX][mazeZ].GetDoors();
                tempDoors[0] = true;
                m_roomMatrix[mazeX][mazeZ].SetDoors(tempDoors);
                isComplete = true;
            }

            loopCount++;
        }

        // Check if infinite loop was detected.
        if (loopCount >= 10000)
        {
            throw new System.Exception("Infinite loop detected in generate_layout().");
        }
    }

    /* Creates one room within the maze.
     *
     * Parameters:
     * x -- Integer for the room's x position within the level layout grid.
     * z -- Integer for the room's z position within the level layout grid.
     *
     * Returns:
     * Vector3Int -- Room's position within the level layout grid.
     */
    private Vector3Int CreateRoom(int x, int z)
    {
        ProtoRoom tempRoom = Instantiate(m_protoRoom) as ProtoRoom;
        tempRoom.SetPosition(x, z);
        m_roomMatrix[x][z] = tempRoom;
        tempRoom.transform.parent = transform;

        // Increase room counter.
        m_roomCount++;

        // Retun room position.
        Vector3Int tempPos = Vector3Int.zero;
        tempPos.x = x;
        tempPos.z = z;
        return tempPos;
    }

    /* Determines if a room is a dead-end and should be a treasure room.
     *
     * Parameters:
     * x -- Integer for the room's x position within the level layout grid.
     * z -- Integer for the room's z position within the level layout grid.
     *
     * Returns:
     * bool -- If the room should be a treasure room or not.
     */
    private bool IsTreasure(int x, int z)
    {
        // Test if the room exists.
        if (m_roomMatrix[x][z] != null)
        {
            // If so, return true if the room has exactly one door.
            bool[] doors = m_roomMatrix[x][z].GetDoors();
            return (doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == true ||
                    doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == false ||
                    doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == false ||
                    doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == false);
        }
        else
        {
            // If not, return false.
            return false;
        }
    }

    /* DEBUG: Prints the maze to the console.
     */
    private void PrintLevel()
    {
        // Create a list of strings with size equal to the maze height.
        List<string> printMatrix = new List<string>();
        for (int z = 0; z < m_roomMatrix[0].Count; z++)
        {
            printMatrix.Add("");
        }

        // Add symbols to indicate the doors in each room.
        for (int x = 0; x < m_roomMatrix.Count; x++)
        {
            for (int z = 0; z < m_roomMatrix[x].Count; z++)
            {
                printMatrix[z] += Inst().DrawDoors(x, z);
            }
        }

        // Combine into one string and print.
        string message = "";
        for (int z = 0; z < m_roomMatrix[0].Count; z++)
        {
            message = printMatrix[z] + "\n" + message;
        }
        Debug.Log(message);
    }

    /* DEBUG: Determines how to represent the room's doors.
     *
     * Parameters:
     * x -- Integer for the room's x position within the level layout grid.
     * z -- Integer for the room's z position within the level layout grid.
     *
     * Returns:
     * string -- Text representation of the room's doors.
     */
    private string DrawDoors(int x, int z)
    {
        if (m_roomMatrix[x][z] != null)
        {
            bool[] doors = m_roomMatrix[x][z].GetDoors();
            if (doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == false)
            {
                return "e";
            }
            else if (doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == true)
            {
                return "[o] ";
            }
            else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == false)
            {
                return "[o] ";
            }
            else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == true)
            {
                return "[┐] ";
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == false)
            {
                return "[o] ";
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == true)
            {
                return "[─] ";
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == false)
            {
                return "[┌] ";
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == true)
            {
                return "[┬] ";
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == false)
            {
                return "[o] ";
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == true)
            {
                return "[┘] ";
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == false)
            {
                return "[│] ";
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == true)
            {
                return "[┤] ";
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == false)
            {
                return "[└] ";
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == true)
            {
                return "[┴] ";
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == false)
            {
                return "[├] ";
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == true)
            {
                return "[┼] ";
            }
            else
            {
                return "e";
            }
        }
        else
        {
            return "[  ] ";
        }
    }
}

