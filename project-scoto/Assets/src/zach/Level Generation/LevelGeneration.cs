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
        generate_layout(level_num);
        room = Instantiate(room);
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

        // Set up empty level layout.
        for (int i = 0; i < maze_height; i++) {
            room_matrix.Add(new List<Room>());
            for (int j = 0; j < maze_width; j++) {
                Room temp_room = Instantiate(room) as Room;
                temp_room.setup(j, i, 0);
                temp_room.gameObject.SetActive(false);
                room_matrix[i].Add(temp_room);
            }
        }

        // Declare variables.
        int gen_x = (maze_width - 1) / 2;
        int gen_z = 0;
        List<Vector3Int> gen_path = new List<Vector3Int>();

        // First, create room right after start room.
        gen_path.Add(create_room(gen_x, gen_z));

        // Procedurally generate layout.
        int loop_count = 0;
        while (!((gen_x == (maze_width - 1) / 2) && (gen_z == maze_height - 1)) && loop_count < 100000) {
            // Check if surrounding room locations are out of bounds or room already exists.
            bool[] blocked = new bool[4];
            blocked[0] = gen_z + 1 >= maze_height || room_matrix[gen_x][gen_z + 1].get_type() != 0;
            blocked[1] = gen_x + 1 >= maze_width || room_matrix[gen_x + 1][gen_z].get_type() != 0;
            blocked[2] = gen_z - 1 < 0 || room_matrix[gen_x][gen_z - 1].get_type() != 0;
            blocked[3] = gen_x - 1 < 0 || room_matrix[gen_x - 1][gen_z].get_type() != 0;

            // Test for dead end.
            while (blocked[0] && blocked[1] && blocked[2] && blocked[3]) {
                // If at start of path, force stop.
                if (gen_path.Count == 0) {
                    Debug.LogError("ERROR: Maze is full before ending is reached.");
                    Application.Quit();
                    break;
                }

                // Backtrack to previous location in path.
                gen_x = gen_path[gen_path.Count - 1].x;
                gen_z = gen_path[gen_path.Count - 1].z;
                gen_path.RemoveAt(gen_path.Count - 1);

                // Test location again.
                blocked[0] = gen_z + 1 >= maze_height || room_matrix[gen_x][gen_z + 1].get_type() != 0;
                blocked[1] = gen_x + 1 >= maze_width || room_matrix[gen_x + 1][gen_z].get_type() != 0;
                blocked[2] = gen_z - 1 < 0 || room_matrix[gen_x][gen_z - 1].get_type() != 0;
                blocked[3] = gen_x - 1 < 0 || room_matrix[gen_x - 1][gen_z].get_type() != 0;
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
                 gen_z += 1;
            } else if (direction == 1) {
                // East
                gen_x += 1;
            } else if (direction == 2) {
                // South
                gen_z -= 1;
            } else if (direction == 3) {
                // West
                gen_x -= 1;
            } else {
                // Bad value.
                Debug.LogError("ERROR: Invalid direction in generate_layout().");
                Application.Quit();
                break;
            }

            // Create a room at location and add it to generation path.
            gen_path.Add(create_room(gen_x, gen_z));

            loop_count++;
        }

        // Check if infinite loop was detected.
        if (loop_count >= 100000) {
            // Force stop.
            Debug.LogError("ERROR: Infinite loop detected in generate_layout().");
            Application.Quit();
        }
    }

    private Vector3Int create_room(int x, int z) {
        Vector3Int temp = Vector3Int.zero;
        temp.x = x;
        temp.z = z;
        room_matrix[x][z].setup(x, z, 1);
        return temp;
    }
}
