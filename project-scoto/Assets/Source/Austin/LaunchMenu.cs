/*
 * Filename: LaunchMenu.cs
 * Developer: Austin Kugler
 * Purpose: Includes functionality for player interaction with the launch menu.
 * Software Pattern: Singleton
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Singleton subclass for the death menu.
 *
 * Member variables:
 * m_instance -- The singleton instance of LaunchMenu.
 */
public sealed class LaunchMenu : BaseMenu
{
    private static LaunchMenu m_instance;

    /*
     * Gets a reference to the instance of the singleton; otherwise creates the necessary.
     *
     * Returns:
     * LaunchMenu -- Reference to the singleton instance.
     */
    public static LaunchMenu Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("LaunchMenu").GetComponent<LaunchMenu>();
        }
        return m_instance;
    }
}
