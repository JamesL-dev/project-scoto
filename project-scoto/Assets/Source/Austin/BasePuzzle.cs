/*
 * Filename: BasePuzzle.cs
 * Developer: Austin Kugler
 * Purpose: This file includes a base class for Ainigma, Odyssey, and other puzzles.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base class for puzzles of all types.
 *
 * Member variables:
 * m_isActive -- Boolean for whether the puzzle is active.
 * m_frequency -- Frequency of puzzle occurence from 0 to 1.
 * m_delay -- The time to wait in seconds before attempting puzzle initiation.
 */
public class BasePuzzle : MonoBehaviour
{
    [SerializeField] protected bool m_isActive = false;
    [SerializeField] protected float m_frequency = 0.5f;
    [SerializeField] protected int m_delay = 300;

    void Update()
    {
        if (m_frequency < 0.0f)
        {
            m_frequency = 0.0f;
        }
        else if (m_frequency > 1.0f)
        {
            m_frequency = 1.0f;
        }
    }

    /*
     * Getter for the m_isActive member variable.
     *
     * Returns:
     * bool -- The value of the m_isActive member variable.
     */
    public bool IsActive()
    {
        return m_isActive;
    }

    /*
     * Getter for the m_frequency member variable.
     *
     * Returns:
     * float -- The value of the m_frequency member variable.
     */
    public float GetFrequency()
    {
        return m_frequency;
    }

    /*
     * Getter for the m_delay member variable.
     *
     * Returns:
     * int -- The value of the m_delay member variable.
     */
    public int GetDelay()
    {
        return m_delay;
    }

    /*
     * Setter function for the m_frequency member variable.
     *
     * Parameters:
     * frequency -- Float value from 0 to 1 that is the chance of a puzzle occuring.
     */
    public void SetFrequency(float frequency)
    {
        m_frequency = frequency;
    }
}
