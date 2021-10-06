using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class PuzzleFrequencyTest
{
    [UnityTest]
    public IEnumerator FrequencyUpperBound()
    {
        GameObject puzzleGameObj = new GameObject();
        puzzleGameObj.AddComponent<BasePuzzle>();
        BasePuzzle puzzle = puzzleGameObj.GetComponent<BasePuzzle>();

        puzzle.SetFrequency(2.0f);
        yield return null;
        Assert.AreEqual(1.0f, puzzle.GetFrequency());
        yield return null;
    }

    [UnityTest]
    public IEnumerator FrequencyLowerBound()
    {
        GameObject puzzleGameObj = new GameObject();
        puzzleGameObj.AddComponent<BasePuzzle>();
        BasePuzzle puzzle = puzzleGameObj.GetComponent<BasePuzzle>();

        puzzle.SetFrequency(-1.0f);
        yield return null;
        Assert.AreEqual(0.0f, puzzle.GetFrequency());
        yield return null;
    }

    [UnityTest]
    public IEnumerator FrequencyBorders()
    {
        GameObject puzzleGameObj = new GameObject();
        puzzleGameObj.AddComponent<BasePuzzle>();
        BasePuzzle puzzle = puzzleGameObj.GetComponent<BasePuzzle>();

        puzzle.SetFrequency(0.0f);
        yield return null;
        Debug.Log(puzzle.GetFrequency());
        Assert.AreEqual(0.0f, puzzle.GetFrequency());
        yield return null;

        puzzle.SetFrequency(1.0f);
        yield return null;
        Debug.Log(puzzle.GetFrequency());
        Assert.AreEqual(1.0f, puzzle.GetFrequency());
        yield return null;
    }
}
