using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class LevelGenStressTests {
    [UnityTest]
    public IEnumerator expand_level() {
        int level = 1;
        int cycles = 0;

        for (int i = 0; i < 100; i++) {
            // Set the level number.
            LevelGeneration.set_level_num(level);

            // Load scene.
            SceneManager.LoadScene("Game");
            yield return new WaitForSeconds(2f);
            cycles++;

            // Check for FPS decrease.
            // If I set it a little lower than 10, Unity crashes before the FPS is detected.
            if (1f / Time.deltaTime < 10f) {
                break;
            }

            // Increase the level size.
            level = Mathf.RoundToInt(level * 1.5f);
        }

        // Print results.
        if (1f / Time.deltaTime < 10f) {
            GameObject level_generator = GameObject.Find("Level Generator");
            Debug.Log("Less than 10 FPS reached | Cycles: " + cycles + " | Level: " + LevelGeneration.get_level_num() + " | Rooms: " + level_generator.GetComponent<LevelGeneration>().get_room_count());
        } else {
            Debug.Log("100 cycles completed without critical FPS reached");
        }

        yield return null;
    }
}
