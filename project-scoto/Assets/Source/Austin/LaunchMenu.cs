/*
 * Filename: LaunchMenu.cs
 * Developer: Austin Kugler
 * Purpose:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LaunchMenu : BaseMenu
{
    private static LaunchMenu m_instance;

    public static LaunchMenu Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("LaunchMenu").GetComponent<LaunchMenu>();
        }
        return m_instance;
    }

    public override void Load()
    {
        SceneManager.LoadScene("LaunchMenu");
    }
}
