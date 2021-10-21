/*
 * Filename: LaunchMenu.cs
 * Developer: Austin Kugler
 * Purpose: This file includes a class for functionality related to the game launch menu.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Class for functionality related to the game launch menu.
 */
public class LaunchMenu : MonoBehaviour
{
    /*
     * Starts the next scene when the play button is pressed.
     */
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*
     * Closes the game when the quit button is pressed.
     */
    public void QuitGame()
    {
        Debug.Log("Application.Quit();");
        Application.Quit();
    }
}
