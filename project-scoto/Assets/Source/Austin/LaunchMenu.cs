/*
 * Filename: LaunchMenu.cs
 * Developer: Austin Kugler
 * Purpose: This file includes a singleton class for functionality related to the game launch menu.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Singletone class for functionality related to the game launch menu.
 */
public sealed class LaunchMenu : MonoBehaviour
{
    private static LaunchMenu m_instance;

    /* Gets a reference to the instance of the singleton, creating the instance if necessary.
     *
     * Returns:
     * LaunchMenu -- Reference to the OptionsMenu instance.
     */
    public static LaunchMenu Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("LaunchMenu").GetComponent<LaunchMenu>();
        }
        return m_instance;
    }

    /*
     * Loads the launch menu.
     */
    public void Load()
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
