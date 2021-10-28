/*
 * Filename: LevelGenStressTests.cs
 * Developer: Zachariah Preston
 * Purpose: Stress tests for the level generation feature.
 */
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


/*
 * Stress tests for the level generation feature.
 */
public class LevelGenStressTests
{
    /* Repeatedly generates larger and larger levels until the FPS gets below a critical value.
     */
    [UnityTest]
    public IEnumerator ExpandLevel()
    {
        int level = 1;
        int cycles = 0;

        for (int i = 0; i < 100; i++)
        {
            // Set the level number.
            LevelGeneration.Inst().SetLevelNum(level);

            // Load scene.
            SceneManager.LoadScene("Game");
            yield return new WaitForSeconds(2f);
            cycles++;

            // Check for FPS decrease.
            // If I set it a little lower than 10, Unity crashes before the FPS is detected.
            if (1f / Time.deltaTime < 10f)
            {
                break;
            }

            // Increase the level size.
            level = Mathf.RoundToInt(level * 1.5f);
        }

        // Print results.
        if (1f / Time.deltaTime < 10f)
        {
            Debug.Log("Less than 10 FPS reached | Cycles: " + cycles + " | Level: " + LevelGeneration.Inst().GetLevelNum() +
                      " | Rooms: " + LevelGeneration.Inst().GetRoomCount());
        }
        else
        {
            Debug.Log("100 cycles completed without critical FPS reached");
        }

        yield return null;
    }
}

