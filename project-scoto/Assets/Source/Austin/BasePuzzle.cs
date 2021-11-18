/*
 * Filename: BasePuzzle.cs
 * Developer: Austin Kugler
 * Purpose: This file includes a base class for Ainigma, Odyssey, and other puzzles.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePuzzle : MonoBehaviour
{
    [SerializeField] protected float m_frequency = 0.5f;
    [SerializeField] protected bool m_isActive = false;

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
     * Setter for the m_frequency member variable.
     *
     * Returns:
     * float -- The value to assign to the m_frequency member variable.
     */
    public void SetFrequency(float frequency)
    {
        m_frequency = frequency;
    }

    /*
     * Initilizes a puzzle if a random chance occurs.
     */
    public void ChancePuzzle()
    {
        if (!m_isActive && m_frequency != 0.0f && m_frequency <= Random.Range(0.0f, 1.0f))
        {
            m_isActive = true;
        }
    }

    protected virtual void RewardPlayer()
    {
        float maxHealth = PlayerData.Inst().GetMaxHealth();
        PlayerData.Inst().TakeHealth(-maxHealth / 4);
    }
}
