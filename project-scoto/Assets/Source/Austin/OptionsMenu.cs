/*
 * Filename: OptionsMenu.cs
 * Developer: Austin Kugler
 * Purpose: Includes functionality for player interaction with the options menu.
 * Software Pattern: Singleton
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Singleton superclass for the options menu.
 *
 * Member variables:
 * m_instance -- The singleton instance of OptionsMenu.
 */
public sealed class OptionsMenu : BaseMenu
{
    [SerializeField] public float m_sensitivity = 0.4f;
    private static OptionsMenu m_instance;

    /*
     * Gets a reference to the instance of the singleton; otherwise creates the necessary.
     *
     * Returns:
     * OptionsMenu -- Reference to the singleton instance.
     */
    public static OptionsMenu Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("OptionsMenu").GetComponent<OptionsMenu>();
        }
        return m_instance;
    }

    /*
     * Setter function for the Unity graphics quality.
     *
     * Parameters:
     * qualityIndex -- Integer index of the quality from 0 to 5.
     */
    public void SetGraphics(int qualityIndex)
    {
        // Set quality index (1 to 6)
        if (qualityIndex > 5)
        {
            qualityIndex = 5;
        }
        else if (qualityIndex < 0)
        {
            qualityIndex = 0;
        }
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /*
     * Setter function for the player mouse sensitivity.
     *
     * Parameters:
     * sensitivity -- Float value of the sensitivity from 0.1 to 1.
     */
    public void SetSensitivity(float sensitivity)
    {
        // Set sensitivity (0.1 to 1)
        if (sensitivity > 1.0f)
        {
            sensitivity = 1.0f;
        }
        else if (sensitivity < 0.1f)
        {
            sensitivity = 0.1f;
        }
        m_sensitivity = sensitivity;
        PlayerController.Inst().m_mouseSens.x = sensitivity;
        PlayerController.Inst().m_mouseSens.y = sensitivity;
    }

    /*
     * Setter function for the player mouse sensitivity.
     *
     * Parameters:
     * volume -- Float value of the volume from -80 to 0.
     */
    public void SetVolume(float volume)
    {
        // Set volume (-80 to 0)
    }
}
