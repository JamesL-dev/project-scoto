using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public GameObject enemy, pickup, wall, wall_door;
    public GameObject[] wall_list = new GameObject[4];
    private int x_pos = 0, z_pos = 0, type = 0;
    private Vector3[] wall_positions = new Vector3[4];
    private Vector3[] wall_rotations = new Vector3[4];

    private void Awake() {
        // Set starting values for position and rotation arrays.
        set_transform_arrays();

        // Create test enemy and pickup GameObjects.
        enemy = Instantiate(enemy, this.transform);
        enemy.transform.position += new Vector3(0, 1, 4);

        pickup = Instantiate(pickup, this.transform);
        pickup.transform.position += new Vector3(1, 2, 6);

        // Add walls.
        for (int i = 0; i < 4; i++) {
            add_wall(wall, i);
        }
    }

    public void setup(int x, int z, bool[] d, int t) {
        // Set starting values.
        x_pos = x;
        z_pos = z;
        type = t;

        // Set walls.
        for (int i = 0; i < 4; i++) {
            // Delete old wall.
            Destroy(wall_list[i].gameObject);

            // Add new wall.
            if (d[i]) {
                add_wall(wall_door, i);
            } else {
                add_wall(wall, i);
            }
        }
    }

    public Vector3 get_position() {
        Vector3 temp = Vector2.zero;
        temp.x = x_pos;
        temp.z = z_pos;
        return temp;
    }

    public int get_type() {
        return type;
    }

    public bool[] get_doors() {
        bool[] doors = new bool[4];
        for (int i = 0; i < 4; i++) {
            doors[i] = (wall_list[i].tag == "Wall With Door");
        }
        return doors;
    }

    private void set_transform_arrays() {
        wall_positions[0] = new Vector3(0, 5, 10);
        wall_positions[1] = new Vector3(10, 5, 0);
        wall_positions[2] = new Vector3(0, 5, -10);
        wall_positions[3] = new Vector3(-10, 5, 0);

        wall_rotations[0] = new Vector3(-90, 0, 0);
        wall_rotations[1] = new Vector3(-90, 90, 0);
        wall_rotations[2] = new Vector3(-90, 180, 0);
        wall_rotations[3] = new Vector3(-90, -90, 0);
    }

    private void add_wall(GameObject wall_type, int index) {
        // Create new wall.
        GameObject temp_wall = Instantiate(wall_type, this.transform);
        temp_wall.transform.position = wall_positions[index];
        temp_wall.transform.eulerAngles = wall_rotations[index];
        temp_wall.tag = wall_type.tag;

        // Add new wall to array.
        wall_list[index] = temp_wall;
    }
}
