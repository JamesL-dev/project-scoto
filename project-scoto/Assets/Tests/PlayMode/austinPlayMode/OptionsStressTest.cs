/*
 * Filename: OptionsStressTest.cs
 * Developer: Austin Kugler
 * Purpose: This file includes a class for stress testing the settings menu.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

/*
 * Class for boundary testing puzzle frequency.
 *
 * Member variables:
 * m_fps -- The current frames per second of the game.
 */
public class OptionsStressTest : MonoBehaviour
{
    float m_fps = 0;

    /*
     * Stress tests the ability of graphics quality settings changes to be quickly applied.
     *
     * Returns:
     * IEnumerator -- Allows the progression of the game.
     */
    [UnityTest]
    public IEnumerator QualityStressTest()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(5);

        int updates = 0;
        while (true)
        {
            m_fps = 1.0f / Time.deltaTime;

            QualitySettings.SetQualityLevel(5, true);
            yield return null;
            QualitySettings.SetQualityLevel(1, true);

            if (m_fps < 30)
            {
                Debug.Log("[SettingsStress.cs -- QualityStressTest()] Stress test ended: 30 FPS reached after " + updates + " quality updates.");
                break;
            }

            updates++;
        }
        yield return null;
    }
}
