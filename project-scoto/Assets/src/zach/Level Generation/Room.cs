using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public GameObject enemy, pickup, wall, wall_door;
    public GameObject[] wall_list = new GameObject[4];
    protected bool[] door_list = new bool[] {false, false, false, false};
    protected int x_pos = 0, z_pos = 0, type = 0;
    protected Vector3[] wall_positions = new Vector3[4], wall_rotations = new Vector3[4];

    protected void Awake() {
        // Set starting values for position and rotation arrays.
        set_transform_arrays();
    }

    public void set_values(int x, int z, bool[] d, int t) {
        set_position(x, z);
        set_doors(d);
        set_type(t);
    }

    public void set_position(int x, int z) {
        x_pos = x;
        z_pos = z;
    }

    public void set_doors(bool[] d) {
        door_list = d;
    }

    public void set_type(int t) {
        type = t;
    }

    public Vector3Int get_position() {
        Vector3Int temp = Vector3Int.zero;
        temp.x = x_pos;
        temp.z = z_pos;
        return temp;
    }

    public bool[] get_doors() {
        return door_list;
    }

    public int get_type() {
        return type;
    }

    public virtual void setup(int maze_width = 0, int maze_height = 0) {
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

        // DEBUG: Create test enemy and pickup GameObjects.
        enemy = Instantiate(enemy, transform);
        enemy.transform.position = (transform.position + new Vector3(0, 0, 0));

        pickup = Instantiate(pickup, transform);
        pickup.transform.position = (transform.position + new Vector3(0, 0, 0));
    }

    protected void set_transform_arrays() {
        wall_positions[0] = new Vector3(0, 5, 10);
        wall_positions[1] = new Vector3(10, 5, 0);
        wall_positions[2] = new Vector3(0, 5, -10);
        wall_positions[3] = new Vector3(-10, 5, 0);

        wall_rotations[0] = new Vector3(-90, 0, 0);
        wall_rotations[1] = new Vector3(-90, 90, 0);
        wall_rotations[2] = new Vector3(-90, 180, 0);
        wall_rotations[3] = new Vector3(-90, -90, 0);
    }
}
