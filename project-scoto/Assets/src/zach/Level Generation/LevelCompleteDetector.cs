using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteDetector : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // Load next level.
            LevelGeneration.set_level_num(LevelGeneration.get_level_num() + 1);
            SceneManager.LoadScene("Game");
        }
    }
}
