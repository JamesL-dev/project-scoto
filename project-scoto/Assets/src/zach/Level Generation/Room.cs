using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public GameObject enemy;
    public GameObject pickup;
    private int x_pos = 0, z_pos = 0, type = 0;

    private void Start() {
        enemy = Instantiate(enemy, this.transform);
        enemy.transform.position += new Vector3(0, 1, 4);

        pickup = Instantiate(pickup, this.transform);
        pickup.transform.position += new Vector3(1, 2, 6);
    }

    public void setup(int x, int z, int t) {
        x_pos = x;
        z_pos = z;
        type = t;
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
}
