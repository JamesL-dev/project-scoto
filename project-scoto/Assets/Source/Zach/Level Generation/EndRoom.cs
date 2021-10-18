using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoom : Room {
    public override void setup(int maze_width = 0, int maze_height = 0) {
        // Generate walls.
        for (int i = 0; i < 4; i++) {
            // Create new wall.
            GameObject temp_wall;
            if (door_list[i]) {
                temp_wall = Instantiate(wall_door, this.transform);
                temp_wall.transform.position = wall_positions[i];
                temp_wall.transform.eulerAngles = wall_rotations[i];
                temp_wall.tag = wall_door.tag;
            } else {
                temp_wall = Instantiate(wall, this.transform);
                temp_wall.transform.position = wall_positions[i];
                temp_wall.transform.eulerAngles = wall_rotations[i];
                temp_wall.tag = wall.tag;
            }

            // Add wall to array.
            wall_list[i] = temp_wall;
        }

        // Set transform from positions.
        Vector3 room_pos = Vector3.zero;
        room_pos.x = (x_pos - ((maze_width - 1) / 2)) * 20;
        room_pos.z = (z_pos + 1) * 20;
        transform.position = room_pos;

        // Create type (just check if type exists, for now)
        if (get_type() == 0) {
            Debug.LogError("ERROR: Room has type of 0 in setup().");
            Application.Quit();
        }
    }
}
