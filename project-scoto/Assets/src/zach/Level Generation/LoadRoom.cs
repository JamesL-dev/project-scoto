using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRoom : MonoBehaviour {
    public GameObject enemy;
    public GameObject pickup;

    private void Start() {
        enemy = Instantiate(enemy, this.transform);
        enemy.transform.position += new Vector3(0, 1, 4);

        pickup = Instantiate(pickup, this.transform);
        pickup.transform.position += new Vector3(1, 2, 6);
    }
}
