/*
 * Filename: BaseMenu.cs
 * Developer: Austin Kugler
 * Purpose: Serves as an interface for all menu types.
 * Software Pattern: Factory Method
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Base factory superclass for all menu subclasses.
 */
public abstract class BaseMenu : MonoBehaviour
{
    /*
     * Closes the game completely.
     */
    public virtual void QuitGame()
    {
        Application.Quit();
    }

    /*
     * Loads the next sequential game scene.
     */
    public virtual void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*
     * Loads the game scene specifically.
     */
    public virtual void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    /*
     * Loads the launch menu scene specifically.
     */
    public virtual void LoadLaunchMenu()
    {
        SceneManager.LoadScene("LaunchMenu");
    }

    /*
     * Loads the death menu specifically.
     */
    public virtual void LoadDeathMenu()
    {
        SceneManager.LoadScene("DeathMenu");
    }
}
