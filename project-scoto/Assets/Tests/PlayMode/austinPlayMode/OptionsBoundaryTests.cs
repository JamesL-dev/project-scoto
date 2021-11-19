/*
 * Filename: OptionsBoundaryTest.cs
 * Developer: Austin Kugler
 * Purpose: Boundary testing for elements of the options menu.
 */
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

/*
 * Class for boundary testing elements of the options menu.
 */
public class OptionsBoundaryTest
{
    /*
     * Ensures both x and y mouse sensitivity are the same.
     *
     * Returns:
     * IEnumerator -- Allows the progression of the game.
     */
    [UnityTest]
    public IEnumerator SensitivityEqual()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2);

        float xSens = PlayerController.Inst().m_mouseSens.x;
        float ySens = PlayerController.Inst().m_mouseSens.y;
        Assert.AreEqual(xSens, ySens);

        yield return null;
    }

    /*
     * Ensures mouse sensitivity does not fall below a lower bound.
     *
     * Returns:
     * IEnumerator -- Allows the progression of the game.
     */
    [UnityTest]
    public IEnumerator SensitivityLowerBound()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2);

        OptionsMenu.Inst().SetSensitivity(-1.0f);
        Assert.AreEqual(0.1f, PlayerController.Inst().m_mouseSens.x);
        yield return null;

        OptionsMenu.Inst().SetSensitivity(-0.1f);
        Assert.AreEqual(0.1f, PlayerController.Inst().m_mouseSens.x);
        yield return null;

        OptionsMenu.Inst().SetSensitivity(0.0f);
        Assert.AreEqual(0.1f, PlayerController.Inst().m_mouseSens.x);
        yield return null;

        OptionsMenu.Inst().SetSensitivity(0.1f);
        Assert.AreEqual(0.1f, PlayerController.Inst().m_mouseSens.x);
        yield return null;
    }

    /*
     * Ensures mouse sensitivity does not reach above an upper bound.
     *
     * Returns:
     * IEnumerator -- Allows the progression of the game.
     */
    [UnityTest]
    public IEnumerator SensitivityUpperBound()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2);

        OptionsMenu.Inst().SetSensitivity(2.0f);
        Assert.AreEqual(1.0f, PlayerController.Inst().m_mouseSens.x);
        yield return null;

        OptionsMenu.Inst().SetSensitivity(1.1f);
        Assert.AreEqual(1.0f, PlayerController.Inst().m_mouseSens.x);
        yield return null;

        OptionsMenu.Inst().SetSensitivity(1.0f);
        Assert.AreEqual(1.0f, PlayerController.Inst().m_mouseSens.x);
        yield return null;
    }

    /*
     * Ensures game graphics does not fall below a lower bound.
     *
     * Returns:
     * IEnumerator -- Allows the progression of the game.
     */
    [UnityTest]
    public IEnumerator GraphicsLowerBound()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2);

        OptionsMenu.Inst().SetGraphics(-2);
        Assert.AreEqual(0, QualitySettings.GetQualityLevel());
        yield return null;

        OptionsMenu.Inst().SetSensitivity(-1);
        Assert.AreEqual(0, QualitySettings.GetQualityLevel());
        yield return null;

        OptionsMenu.Inst().SetSensitivity(0);
        Assert.AreEqual(0, QualitySettings.GetQualityLevel());
        yield return null;
    }

    /*
     * Ensures game graphics does not reach above an upper bound.
     *
     * Returns:
     * IEnumerator -- Allows the progression of the game.
     */
    [UnityTest]
    public IEnumerator GraphicsUpperBound()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2);

        OptionsMenu.Inst().SetGraphics(12);
        Assert.AreEqual(5, QualitySettings.GetQualityLevel());
        yield return null;

        OptionsMenu.Inst().SetSensitivity(6);
        Assert.AreEqual(5, QualitySettings.GetQualityLevel());
        yield return null;

        OptionsMenu.Inst().SetSensitivity(5);
        Assert.AreEqual(5, QualitySettings.GetQualityLevel());
        yield return null;
    }
}
