using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    public GameObject player;
    public Room room;
    public int level_num;
    public List<List<Room>> room_matrix = new List<List<Room>>();
    private float mw_scaling = 0.1f;
    private float mh_scaling = 0.2f;

    private void Start() {
        // DEBUG
        Room temp_room = Instantiate(room) as Room;
        bool[] test_room = new bool[4] {true, false, true, true};
        temp_room.setup(0, 0, test_room, 0);

        generate_layout(level_num);
    }

    private void generate_layout(int level) {
        // Choose the width-height variance.
        float size_seed = Random.value;

        // Choose a random starting size, excluding start and end room.
        // Both width and height start as round(random value between 2.5 and 3.5).
        // As the level number increases, the range of width and height increase by 0.2, but width must be odd.
        // The width and height are complementary; a wider level is less likely to be taller, and vice versa.
        int maze_width = 3 + 2 * (int)Mathf.Round((-0.5f + size_seed) + (mw_scaling * (level - 1)));
        int maze_height = 3 + (int)Mathf.Round((0.5f - size_seed) + (mh_scaling * (level - 1)));
        
        // DEBUG
        bool[] closed_room = new bool[4];

        // Set up empty level layout.
        for (int i = 0; i < maze_height; i++) {
            room_matrix.Add(new List<Room>());
            for (int j = 0; j < maze_width; j++) {
                Room temp_room = Instantiate(room) as Room;
                temp_room.setup(j, i, closed_room, 0);
                temp_room.gameObject.SetActive(false);
                room_matrix[i].Add(temp_room);
            }
        }

        // Declare variables.
        int maze_x = (maze_width - 1) / 2;
        int maze_z = 0;
        List<Vector3Int> maze_path = new List<Vector3Int>();

        // First, create room right after start room.
        maze_path.Add(create_room(maze_x, maze_z, closed_room));

        // Procedurally generate layout.
        int loop_count = 0;
        while (!((maze_x == (maze_width - 1) / 2) && (maze_z == maze_height - 1)) && loop_count < 100000) {
            // Check if surrounding room locations are out of bounds or room already exists.
            bool[] blocked = new bool[4];
            blocked[0] = maze_z + 1 >= maze_height || room_matrix[maze_x][maze_z + 1].get_type() != 0;
            blocked[1] = maze_x + 1 >= maze_width || room_matrix[maze_x + 1][maze_z].get_type() != 0;
            blocked[2] = maze_z - 1 < 0 || room_matrix[maze_x][maze_z - 1].get_type() != 0;
            blocked[3] = maze_x - 1 < 0 || room_matrix[maze_x - 1][maze_z].get_type() != 0;

            // Test for dead end.
            while (blocked[0] && blocked[1] && blocked[2] && blocked[3]) {
                // If at start of path, force stop.
                if (maze_path.Count == 0) {
                    Debug.LogError("ERROR: Maze is full before ending is reached.");
                    Application.Quit();
                    break;
                }

                // Backtrack to previous location in path.
                maze_x = maze_path[maze_path.Count - 1].x;
                maze_z = maze_path[maze_path.Count - 1].z;
                maze_path.RemoveAt(maze_path.Count - 1);

                // Test location again.
                blocked[0] = maze_z + 1 >= maze_height || room_matrix[maze_x][maze_z + 1].get_type() != 0;
                blocked[1] = maze_x + 1 >= maze_width || room_matrix[maze_x + 1][maze_z].get_type() != 0;
                blocked[2] = maze_z - 1 < 0 || room_matrix[maze_x][maze_z - 1].get_type() != 0;
                blocked[3] = maze_x - 1 < 0 || room_matrix[maze_x - 1][maze_z].get_type() != 0;
            }

            // Choose a new location, repeat until an empty one is chosen.
            int direction = Random.Range(0, 4); // returns 0, 1, 2, or 3
            while (blocked[direction] && loop_count < 100000) {
                direction = Random.Range(0, 4);
                loop_count++;
            }

            // Go to new location.
            if (direction == 0) {
                // North
                maze_z += 1;
                bool[] temp_doors = new bool[4] {false, false, true, false};
                maze_path.Add(create_room(maze_x, maze_z, temp_doors));
            } else if (direction == 1) {
                // East
                maze_x += 1;
                bool[] temp_doors = new bool[4] {false, false, false, true};
                maze_path.Add(create_room(maze_x, maze_z, temp_doors));
            } else if (direction == 2) {
                // South
                maze_z -= 1;
                bool[] temp_doors = new bool[4] {true, false, false, false};
                maze_path.Add(create_room(maze_x, maze_z, temp_doors));
            } else if (direction == 3) {
                // West
                maze_x -= 1;
                bool[] temp_doors = new bool[4] {false, true, false, false};
                maze_path.Add(create_room(maze_x, maze_z, temp_doors));
            } else {
                // Bad value.
                Debug.LogError("ERROR: Invalid direction in generate_layout().");
                Application.Quit();
                break;
            }

            loop_count++;
        }

        // Check if infinite loop was detected.
        if (loop_count >= 100000) {
            // Force stop.
            Debug.LogError("ERROR: Infinite loop detected in generate_layout().");
            Application.Quit();
        }

        // DEBUG: Print visualization of level.
        string matrix = "";
        for (int i = 0; i < maze_height; i++) {
            string row = "";
            for (int j = 0; j < maze_width; j++) {
                if (room_matrix[j][i].get_type() != 0) {
                    row += "[┼] ";
                } else {
                    row += "[   ] ";
                }
            }
            matrix = row + "\n" + matrix;
        }
        Debug.Log(matrix);
    }

    private Vector3Int create_room(int x, int z, bool[] doors) {
        Vector3Int temp_pos = Vector3Int.zero;
        temp_pos.x = x;
        temp_pos.z = z;
        room_matrix[x][z].setup(x, z, doors, 1);
        return temp_pos;
    }
}
