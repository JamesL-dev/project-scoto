/*
 * Filename: OdysseyPuzzle.cs
 * Developer: Austin Kugler
 * Purpose: This file includes a class for functionality related to odyssey puzzles.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for odyssey puzzle functionality, inheriting from BasePuzzle.
 */
public class OdysseyPuzzle : BasePuzzle
{
    void Start()
    {
        InvokeRepeating("ChancePuzzle", m_delay, m_delay);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    /*
     * Initilizes a puzzle if a random chance occurs.
     */
    void ChancePuzzle()
    {
        if (!m_isActive && m_frequency != 0.0f && m_frequency <= Random.Range(0.0f, 1.0f))
        {
            Initialize();
        }
    }

    /*
     * Initilizes a new puzzle when called.
     */
    void Initialize()
    {
        m_isActive = true;
    }
}
