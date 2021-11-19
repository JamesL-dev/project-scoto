/*
 * Filename: LinkTest.cs
 * Developer: Austin Kugler
 * Purpose: Testing navigation between menu scenes.
 */
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

/*
 * Class for testing navigation between menu scenes.
 */
public class LinkTest : MonoBehaviour
{
    /*
     * Ensures the pause menu can toggled properly.
     *
     * Returns:
     * IEnumerator -- Allows the progression of the game.
     */
    [UnityTest]
    public IEnumerator PauseMenuLinkTest()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2);
        PauseMenu.Inst().LoadGame();
        PauseMenu.Inst().LoadGame();
        Assert.AreEqual(1, Time.timeScale);

        yield return null;
    }

    /*
     * Ensures the game scene loads correctly.
     *
     * Returns:
     * IEnumerator -- Allows the progression of the game.
     */
    [UnityTest]
    public IEnumerator GameLinkTest()
    {
        string activeScene;
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2);
        activeScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("Game", SceneManager.GetActiveScene().name);
        yield return null;
    }

    /*
     * Ensures the launch menu scene loads correctly.
     *
     * Returns:
     * IEnumerator -- Allows the progression of the game.
     */
    [UnityTest]
    public IEnumerator LaunchMenuLinkTest()
    {
        string activeScene;
        SceneManager.LoadScene("LaunchMenu");
        yield return new WaitForSeconds(2);
        activeScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("LaunchMenu", SceneManager.GetActiveScene().name);
        yield return null;
    }

    /*
     * Ensures the death menu scene loads correctly.
     *
     * Returns:
     * IEnumerator -- Allows the progression of the game.
     */
    [UnityTest]
    public IEnumerator DeathMenuLinkTest()
    {
        string activeScene;
        SceneManager.LoadScene("DeathMenu");
        yield return new WaitForSeconds(2);
        activeScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("DeathMenu", SceneManager.GetActiveScene().name);
        yield return null;
    }

    /*
     * Ensures the win menu scene loads correctly.
     *
     * Returns:
     * IEnumerator -- Allows the progression of the game.
     */
    [UnityTest]
    public IEnumerator WinMenuLinkTest()
    {
        string activeScene;
        SceneManager.LoadScene("WinMenu");
        yield return new WaitForSeconds(2);
        activeScene = SceneManager.GetActiveScene().name;
        Assert.AreEqual("WinMenu", SceneManager.GetActiveScene().name);
        yield return null;
    }
}
