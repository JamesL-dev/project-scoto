using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRoom : MonoBehaviour {
    public BaseEnemy enemy;

    private void Start() {
        enemy = new BaseEnemy(Vector3.forward);
        enemy = Instantiate(enemy);
    }
}
