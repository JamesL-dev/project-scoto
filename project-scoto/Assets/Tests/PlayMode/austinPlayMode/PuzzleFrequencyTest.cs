/*
 * Filename: PuzzleFrequencyTest.cs
 * Developer: Austin Kugler
 * Purpose: This file includes a class for boundary testing puzzle frequency in the BasePuzzle class.
 */
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

/*
 * Class for boundary testing puzzle frequency.
 */
public class PuzzleFrequencyTest
{
    // /*
    //  * Tests the upper bound of puzzle freqeuncy, should be <= 1.0f (100%).
    //  *
    //  * Returns:
    //  * IEnumerator -- Allows the progression of the game.
    //  */
    // [UnityTest]
    // public IEnumerator FrequencyUpperBound()
    // {
    //     GameObject puzzleGameObj = new GameObject();
    //     puzzleGameObj.AddComponent<BasePuzzle>();
    //     BasePuzzle puzzle = puzzleGameObj.GetComponent<BasePuzzle>();

    //     puzzle.SetFrequency(2.0f);
    //     yield return null;
    //     Debug.Log(puzzle.GetFrequency());
    //     Assert.AreEqual(1.0f, puzzle.GetFrequency());
    //     yield return null;

    //     puzzle.SetFrequency(1.1f);
    //     yield return null;
    //     Debug.Log(puzzle.GetFrequency());
    //     Assert.AreEqual(1.0f, puzzle.GetFrequency());
    //     yield return null;

    //     puzzle.SetFrequency(1.0f);
    //     yield return null;
    //     Assert.AreEqual(1.0f, puzzle.GetFrequency());
    //     yield return null;

    //     puzzle.SetFrequency(0.9f);
    //     yield return null;
    //     Assert.AreEqual(0.9f, puzzle.GetFrequency());
    //     yield return null;
    // }

    // /*
    //  * Tests the upper bound of puzzle freqeuncy, should be >= 0.0f (0%).
    //  *
    //  * Returns:
    //  * IEnumerator -- Allows the progression of the game.
    //  */
    // [UnityTest]
    // public IEnumerator FrequencyLowerBound()
    // {
    //     GameObject puzzleGameObj = new GameObject();
    //     puzzleGameObj.AddComponent<BasePuzzle>();
    //     BasePuzzle puzzle = puzzleGameObj.GetComponent<BasePuzzle>();

    //     puzzle.SetFrequency(-1.0f);
    //     yield return null;
    //     Debug.Log(puzzle.GetFrequency());
    //     Assert.AreEqual(0.0f, puzzle.GetFrequency());
    //     yield return null;

    //     puzzle.SetFrequency(-0.1f);
    //     yield return null;
    //     Debug.Log(puzzle.GetFrequency());
    //     Assert.AreEqual(0.0f, puzzle.GetFrequency());
    //     yield return null;

    //     puzzle.SetFrequency(0.0f);
    //     yield return null;
    //     Assert.AreEqual(0.0f, puzzle.GetFrequency());
    //     yield return null;

    //     puzzle.SetFrequency(0.1f);
    //     yield return null;
    //     Assert.AreEqual(0.1f, puzzle.GetFrequency());
    //     yield return null;
    // }
}
