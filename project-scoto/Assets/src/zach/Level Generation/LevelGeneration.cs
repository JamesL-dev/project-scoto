using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    public Room room;
    public StartRoom sr;
    public EndRoom er;
    public List<List<Room>> room_matrix = new List<List<Room>>();
    private const float mw_scaling = 0.1f, mh_scaling = 0.2f;
    private static int level_num = 1;
    private int room_count = 0;

    private void Start() {
        // Make start room.
        Room start_room = Instantiate(sr) as StartRoom;
        bool[] start_room_doors = new bool[4] {true, false, false, false};
        start_room.set_values(0, 0, start_room_doors, 1);
        start_room.setup();
        room_count++;

        // Procedurally generate level layout.
        generate_layout(level_num);

        // // DEBUG: Print visualization of level.
        // print_level();
  
        // Loop through matrix to finish building rooms.
        for (int x = 0; x < room_matrix.Count; x++) {
            for (int z = 0; z < room_matrix[x].Count; z++) {
                // Identify treasure rooms.
                if (is_treasure(x, z)) {
                    room_matrix[x][z].set_type(3);
                }
            
                // Set up rooms.
                if (room_matrix[x][z] != null) {
                    room_matrix[x][z].setup(room_matrix.Count, room_matrix[x].Count);
                }
            }
        }

        // Make end room.
        Room end_room = Instantiate(er) as EndRoom;
        bool[] end_room_doors = new bool[4] {false, false, true, false};
        end_room.set_values(0, room_matrix[0].Count, end_room_doors, 2);
        end_room.setup();
        room_count++;
    }

    public void generate_layout(int level) {
        // Choose the width-height variance.
        float size_seed = Random.value;

        // Choose a random starting size, excluding start and end room.
        // Both width and height start as round(random value between 2.5 and 3.5).
        // As the level number increases, the range of width and height increase by 0.2, but width must be odd.
        // The width and height are complementary; a wider level is less likely to be taller, and vice versa.
        int maze_width = 3 + 2 * Mathf.RoundToInt((-0.5f + size_seed) + (mw_scaling * (level - 1)));
        int maze_height = 3 + Mathf.RoundToInt((0.5f - size_seed) + (mh_scaling * (level - 1)));

        // Determine minimum number of rooms, not counting start and end room.
        // Approximately equal to (width upper bound - 1) * (height upper bound - 1)
        int min_rooms = Mathf.RoundToInt((level / 5f + 2f) * (level / 5f + 2f));
        
        // Set up empty level layout.
        for (int x = 0; x < maze_width; x++) {
            room_matrix.Add(new List<Room>());
            for (int z = 0; z < maze_height; z++) {
                room_matrix[x].Add(null);
            }
        }

        // Declare variables.
        int maze_x = (maze_width - 1) / 2;
        int maze_z = 0;
        List<Vector3Int> maze_path = new List<Vector3Int>();

        // First, create room right after start room.
        maze_path.Add(create_room(maze_x, maze_z));
        bool[] temp_doors = room_matrix[maze_x][maze_z].get_doors();
        temp_doors[2] = true;
        room_matrix[maze_x][maze_z].set_doors(temp_doors);

        // Procedurally generate layout until ending is reached and the number of maze rooms is at least min_rooms.
        // (The "- 1" is to account for the start room.)
        int loop_count = 0;
        bool is_complete = false;
        while ((room_count - 1 < min_rooms || !is_complete) && loop_count < 10000) {
            // Check if surrounding room locations are out of bounds or room already exists.
            bool[] blocked = new bool[4];
            blocked[0] = (maze_z + 1 >= maze_height) || (room_matrix[maze_x][maze_z + 1] != null);
            blocked[1] = (maze_x + 1 >= maze_width) || (room_matrix[maze_x + 1][maze_z] != null);
            blocked[2] = (maze_z - 1 < 0) || (room_matrix[maze_x][maze_z - 1] != null);
            blocked[3] = (maze_x - 1 < 0) || (room_matrix[maze_x - 1][maze_z] != null);

            // Test for dead end.
            while (blocked[0] && blocked[1] && blocked[2] && blocked[3]) {
                // If at start of path, force stop.
                if (maze_path.Count == 0) {
                    Debug.LogError("ERROR: Maze is full before ending is reached.");
                    Application.Quit();
                }

                // Backtrack to previous location in path.
                maze_x = maze_path[maze_path.Count - 1].x;
                maze_z = maze_path[maze_path.Count - 1].z;
                maze_path.RemoveAt(maze_path.Count - 1);

                // Test surrounding locations again.
                blocked[0] = (maze_z + 1 >= maze_height) || (room_matrix[maze_x][maze_z + 1] != null);
                blocked[1] = (maze_x + 1 >= maze_width) || (room_matrix[maze_x + 1][maze_z] != null);
                blocked[2] = (maze_z - 1 < 0) || (room_matrix[maze_x][maze_z - 1] != null);
                blocked[3] = (maze_x - 1 < 0) || (room_matrix[maze_x - 1][maze_z] != null);
            }

            // Choose a new location, repeat until an empty one is chosen.
            int direction = Random.Range(0, 4); // returns 0, 1, 2, or 3
            while (blocked[direction] && loop_count < 10000) {
                direction = Random.Range(0, 4);
                loop_count++;
            }

            // Add door in old room.
            temp_doors = room_matrix[maze_x][maze_z].get_doors();
            temp_doors[direction] = true;
            room_matrix[maze_x][maze_z].set_doors(temp_doors);

            // Go to new room location and create a room.
            if (direction == 0) {
                // North
                maze_z += 1;
            } else if (direction == 1) {
                // East
                maze_x += 1;
            } else if (direction == 2) {
                // South
                maze_z -= 1;
            } else if (direction == 3) {
                // West
                maze_x -= 1;
            } else {
                // Bad value.
                Debug.LogError("ERROR: Invalid direction in generate_layout().");
                Application.Quit();
            }
            maze_path.Add(create_room(maze_x, maze_z));

            // Add door in new room.
            temp_doors = room_matrix[maze_x][maze_z].get_doors();
            temp_doors[(direction + 2) % 4] = true;
            room_matrix[maze_x][maze_z].set_doors(temp_doors);

            // Test for end of maze. If so, add door to end room in the last room.
            if ((maze_x == (maze_width - 1) / 2) && (maze_z == maze_height - 1)) {
                temp_doors = room_matrix[maze_x][maze_z].get_doors();
                temp_doors[0] = true;
                room_matrix[maze_x][maze_z].set_doors(temp_doors);
                is_complete = true;
            }

            loop_count++;
        }

        // Check if infinite loop was detected.
        if (loop_count >= 10000) {
            // Force stop.
            Debug.LogError("ERROR: Infinite loop detected in generate_layout().");
            Application.Quit();
        }
    }

    private Vector3Int create_room(int x, int z) {
        // Create room at position.
        Room temp_room = Instantiate(room) as Room;
        temp_room.set_position(x, z);
        temp_room.set_type(0);
        room_matrix[x][z] = temp_room;

        // Increase room counter.
        room_count++;

        // Retun room position.
        Vector3Int temp_pos = Vector3Int.zero;
        temp_pos.x = x;
        temp_pos.z = z;
        return temp_pos;
    }

    // DEBUG
    private void print_level() {
        // Create a list of strings with size equal to the maze height.
        List<string> print_matrix = new List<string>();
        for (int z = 0; z < room_matrix[0].Count; z++) {
            print_matrix.Add("");
        }

        // Add symbols to indicate the doors in each room.
        for (int x = 0; x < room_matrix.Count; x++) {
            for (int z = 0; z < room_matrix[x].Count; z++) {
                print_matrix[z] += draw_doors(x, z);
            }
        }

        // Combine into one string and print.
        string message = "";
        for (int z = 0; z < room_matrix[0].Count; z++) {
            message = print_matrix[z] + "\n" + message;
        }
        Debug.Log(message);
    }

    // DEBUG
    private string draw_doors(int x, int z) {
        if (room_matrix[x][z] != null) {
            bool[] doors = room_matrix[x][z].get_doors();
            if (doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == false) {
                return "e";
            } else if (doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == true) {
                return "[o] ";
            } else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == false) {
                return "[o] ";
            } else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == true) {
                return "[┐] ";
            } else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == false) {
                return "[o] ";
            } else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == true) {
                return "[─] ";
            } else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == false) {
                return "[┌] ";
            } else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == true) {
                return "[┬] ";
            } else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == false) {
                return "[o] ";
            } else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == true) {
                return "[┘] ";
            } else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == false) {
                return "[│] ";
            } else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == true) {
                return "[┤] ";
            } else if (doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == false) {
                return "[└] ";
            } else if (doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == true) {
                return "[┴] ";
            } else if (doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == false) {
                return "[├] ";
            } else if (doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == true) {
                return "[┼] ";
            } else {
                return "e";
            }
        } else {
            return "[  ] ";
        }
    }

    public static void set_level_num(int level) {
        if (level < 0) {
            Debug.LogWarning("Warning: Level number is less than 0, setting to 0 instead.");
            level_num = 0;
        } else if (level > 100) {
            Debug.LogWarning("Warning: Level number is over 100, setting to 100 instead.");
            level_num = 100;
        } else {
            level_num = level;
        }
    }

    public static int get_level_num() {
        return level_num;
    }

    public int get_room_count() {
        return room_count;
    }

    private bool is_treasure(int x, int z) {
        // Test if the room exists.
        if (room_matrix[x][z] != null) {
            // If so, return true if the room has exactly one door.
            bool[] doors = room_matrix[x][z].get_doors();
            return (doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == true ||
                    doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == false ||
                    doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == false ||
                    doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == false);
        } else {
            // If not, return false.
            return false;
        }
    }
}
