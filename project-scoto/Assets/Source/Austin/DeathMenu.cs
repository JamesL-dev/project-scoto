/*
 * Filename: DeathMenu.cs
 * Developer: Austin Kugler
 * Purpose: Includes functionality for player interaction with the death menu.
 * Software Pattern: Singleton
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Singleton superclass for the death menu.
 *
 * Member variables:
 * m_instance -- The singleton instance of DeathMenu.
 */
public sealed class DeathMenu : BaseMenu
{
    private static DeathMenu m_instance;

    /*
     * Gets a reference to the instance of the singleton; otherwise creates the necessary.
     *
     * Returns:
     * DeathMenu -- Reference to the singleton instance.
     */
    public static DeathMenu Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("DeathMenu").GetComponent<DeathMenu>();
        }
        return m_instance;
    }
}
