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
 *
 * Member variables:
 * m_gameLength -- Integer for the number of levels that need to be cleared to win.
 * m_isLoadReady -- Bool for determining if it's time to load the next level.
 */
public class LevelCompleteDetector : MonoBehaviour
{
    public int m_gameLength;

    private bool m_isLoadReady = false;

    /* Remvoes fade when level starts.
     */
    void Start()
    {
        UIController.Inst().Fade();
    }

    /* Loads the next level based on the timing of the fade to black.
     */
    void Update()
    {
        // Detect if removing fade has finished.
        if (!m_isLoadReady && !UIController.Inst().IsBlack())
        {
            // Prepare for next level completed.
            m_isLoadReady = true;
        }

        // Detect if fade to black has finished.
        if (m_isLoadReady && UIController.Inst().IsBlack())
        {
            // Increment level number.
            m_isLoadReady = false;
            LevelGeneration.Inst().SetLevelNum(LevelGeneration.Inst().GetLevelNum() + 1);

            // Test if game is over.
            if (LevelGeneration.Inst().GetLevelNum() > m_gameLength)
            {
                // If enough levels have been cleared, end game.
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
            else
            {
                // Otherwise, load next level.
                SceneManager.LoadScene("Game");
            }
        }
    }

    /* Detects if the next level trigger has been activated.
     *
     * Parameters:
     * other -- Collider for the GameObject that activated the trigger. Checks if it's the player.
     */
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Fade to black.
            UIController.Inst().Fade();
        }
    }
}

