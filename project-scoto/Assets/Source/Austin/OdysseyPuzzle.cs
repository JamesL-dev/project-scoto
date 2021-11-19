/*
 * Filename: OdysseyPuzzle.cs
 * Developer: Austin Kugler
 * Purpose: This file includes a singleton class for functionality related to odyssey puzzles.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Singleton subclass for odyssey puzzle functionality, inheriting from BasePuzzle.
 */
public sealed class OdysseyPuzzle : BasePuzzle
{
    private static OdysseyPuzzle m_instance;

    /* Gets a reference to the instance of the singleton, creating the instance if necessary.
     *
     * Returns:
     * OdysseyPuzzle -- Reference to the OdysseyPuzzle instance.
     */
    public static OdysseyPuzzle Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("OdysseyPuzzle").GetComponent<OdysseyPuzzle>();
        }
        return m_instance;
    }

    protected override void RewardPlayer()
    {
        float maxHealth = PlayerData.Inst().GetMaxHealth();
        PlayerData.Inst().TakeHealth(-maxHealth / 2);
    }
}
