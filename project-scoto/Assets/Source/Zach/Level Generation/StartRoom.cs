using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoom : Room {
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

        // Spawn player.
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().tp(0, 0, -5);
    }
}
