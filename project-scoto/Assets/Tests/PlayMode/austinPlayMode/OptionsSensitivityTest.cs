using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class OptionsSensitivityTest
{
    // [SetUp]
    // public void SetUp()
    // {
    //     SceneManager.LoadScene("Game");
    // }

    // [UnityTest]
    // public IEnumerator SensitivityUpperBound()
    // {
    //     OptionsMenu optionsMenu = OptionsMenu.Inst();
    //     // OptionsMenu.Inst().SetSensitivity(2.0f);
    //     Assert.AreEqual(1.0f, 1.0f);
    //     yield return null;
    //     // SceneManager.LoadScene("Game");
    //     // yield return new WaitForSeconds(3);

    //     // OptionsMenu.Inst().SetSensitivity(2.0f);
    //     // yield return null;
    //     // Assert.AreEqual(1.0f, 1.0f);
    //     // Assert.AreEqual(1.0f, 1.0f);
    //     // yield return null;
    //     // GameObject puzzleGameObj = new GameObject();
    //     // puzzleGameObj.AddComponent<BasePuzzle>();
    //     // BasePuzzle puzzle = puzzleGameObj.GetComponent<BasePuzzle>();

    //     // puzzle.SetFrequency(2.0f);
    //     // yield return null;
    //     // Debug.Log(puzzle.GetFrequency());
    //     // Assert.AreEqual(1.0f, puzzle.GetFrequency());
    //     // yield return null;

    //     // puzzle.SetFrequency(1.1f);
    //     // yield return null;
    //     // Debug.Log(puzzle.GetFrequency());
    //     // Assert.AreEqual(1.0f, puzzle.GetFrequency());
    //     // yield return null;

    //     // puzzle.SetFrequency(1.0f);
    //     // yield return null;
    //     // Assert.AreEqual(1.0f, puzzle.GetFrequency());
    //     // yield return null;

    //     // puzzle.SetFrequency(0.9f);
    //     // yield return null;
    //     // Assert.AreEqual(0.9f, puzzle.GetFrequency());
    //     // yield return null;
    // }

    // // [UnityTest]
    // // public IEnumerator FrequencyLowerBound()
    // // {
    // //     GameObject puzzleGameObj = new GameObject();
    // //     puzzleGameObj.AddComponent<BasePuzzle>();
    // //     BasePuzzle puzzle = puzzleGameObj.GetComponent<BasePuzzle>();

    // //     puzzle.SetFrequency(-1.0f);
    // //     yield return null;
    // //     Debug.Log(puzzle.GetFrequency());
    // //     Assert.AreEqual(0.0f, puzzle.GetFrequency());
    // //     yield return null;

    // //     puzzle.SetFrequency(-0.1f);
    // //     yield return null;
    // //     Debug.Log(puzzle.GetFrequency());
    // //     Assert.AreEqual(0.0f, puzzle.GetFrequency());
    // //     yield return null;

    // //     puzzle.SetFrequency(0.0f);
    // //     yield return null;
    // //     Assert.AreEqual(0.0f, puzzle.GetFrequency());
    // //     yield return null;

    // //     puzzle.SetFrequency(0.1f);
    // //     yield return null;
    // //     Assert.AreEqual(0.1f, puzzle.GetFrequency());
    // //     yield return null;
    // // }
}
