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
     * Loads the launch menu.
     */
    public virtual void Load()
    {
        SceneManager.LoadScene("LaunchMenu");
    }

    /*
     * Starts the next scene when the play button is pressed.
     */
    public void NextScene()
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
