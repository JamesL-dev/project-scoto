using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    public GameObject room;

    private void Start() {
        room = Instantiate(room);
    }
}
