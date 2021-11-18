/*
 * Filename: BaseMenu.cs
 * Developer: Austin Kugler
 * Purpose:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseMenu : MonoBehaviour
{
    public abstract void Load();

    public virtual void QuitGame()
    {
        Application.Quit();
    }

    public virtual void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public virtual void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public virtual void LoadLaunchMenu()
    {
        SceneManager.LoadScene("LaunchMenu");
    }

    public virtual void LoadDeathMenu()
    {
        SceneManager.LoadScene("DeathMenu");
    }
}
