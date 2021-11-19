/*
 * Filename: WinMenu.cs
 * Developer: Austin Kugler
 * Purpose: Includes functionality for player interaction with the win menu.
 * Software Pattern: Singleton
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Singleton subclass for the win menu.
 *
 * Member variables:
 * m_instance -- The singleton instance of WinMenu.
 */
public sealed class WinMenu : BaseMenu
{
    private static WinMenu m_instance;

    /*
     * Gets a reference to the instance of the singleton; otherwise creates the necessary.
     *
     * Returns:
     * WinMenu -- Reference to the singleton instance.
     */
    public static WinMenu Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("WinMenu").GetComponent<WinMenu>();
        }
        return m_instance;
    }
}
