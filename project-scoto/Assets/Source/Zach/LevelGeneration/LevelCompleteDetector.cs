/*
 * Filename: LevelCompleteDetector.cs
 * Developer: Zachariah Preston
 * Purpose: Triggers the next level.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * Triggers the next level.
 */
public class LevelCompleteDetector : MonoBehaviour
{
    /* Detects if the next level trigger has been activated and loads the next level.
     *
     * Parameters:
     * other -- Collider for the GameObject that activated the trigger. Checks if it's the player.
     */
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Load next level.
            LevelGeneration.Inst().SetLevelNum(LevelGeneration.Inst().GetLevelNum() + 1);
            SceneManager.LoadScene("Game");
        }
    }
}

