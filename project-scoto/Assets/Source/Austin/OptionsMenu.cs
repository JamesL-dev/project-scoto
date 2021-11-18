/*
 * Filename: OptionsMenu.cs
 * Developer: Austin Kugler
 * Purpose:
 * Software Pattern: Singleton
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class OptionsMenu : BaseMenu
{
    private static OptionsMenu m_instance;

    public static OptionsMenu Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("OptionsMenu").GetComponent<OptionsMenu>();
        }
        return m_instance;
    }

    public void SetGraphics(int qualityIndex)
    {
        // Set quality index (1 to 6)
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetSensitivity(float sensitivity)
    {
        // Set sensitivity (0.1 to 1)
        PlayerController.Inst().m_mouseSens.x = sensitivity;
        PlayerController.Inst().m_mouseSens.y = sensitivity;
    }

    public void SetVolume(float volume)
    {
        // Set volume (-80 to 0)
    }
}
