/*
 * Filename: DeathMenu.cs
 * Developer: Austin Kugler
 * Purpose:
 * Software Pattern: Singleton
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class DeathMenu : BaseMenu
{
    private static DeathMenu m_instance;

    public static DeathMenu Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("DeathMenu").GetComponent<DeathMenu>();
        }
        return m_instance;
    }
}
