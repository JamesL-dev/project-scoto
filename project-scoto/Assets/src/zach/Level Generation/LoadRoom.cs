using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRoom : MonoBehaviour {
    public GameObject enemy;

    private void Start() {
        enemy = Instantiate(enemy, this.transform);
        enemy.transform.position += 5 * Vector3.forward;
    }
}
