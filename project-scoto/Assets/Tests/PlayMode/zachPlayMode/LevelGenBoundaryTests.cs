using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class LevelGenBoundaryTests {
    [UnityTest]
    public IEnumerator room_count_lower_bound() {
        // Set the level number.
        LevelGeneration.level_num = 1;

        // Load scene.
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2f);

        // Test for too few rooms.
        GameObject level_generator = GameObject.Find("Level Generator");
        Debug.Log("Lower bound test (>= 4) | Level: " + LevelGeneration.level_num + " | Rooms: " + level_generator.GetComponent<LevelGeneration>().room_count);
        Assert.IsTrue(level_generator.GetComponent<LevelGeneration>().room_count >= 4);
        yield return null;
    }

    [UnityTest]
    public IEnumerator room_count_upper_bound() {
        // Set the level number.
        LevelGeneration.level_num = 1;

        // Load scene.
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2f);

        // Test for too many rooms.
        GameObject level_generator = GameObject.Find("Level Generator");
        Debug.Log("Upper bound test (<= 10) | Level: " + LevelGeneration.level_num + " | Rooms: " + level_generator.GetComponent<LevelGeneration>().room_count);
        Assert.IsTrue(level_generator.GetComponent<LevelGeneration>().room_count <= 10);
        yield return null;
    }
}
