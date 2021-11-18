/*
 * Filename: OptionsMenu.cs
 * Developer: Austin Kugler
 * Purpose:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class OptionsMenu : MonoBehaviour
{
    private static OptionsMenu m_instance;

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
     * qualityIndex -- Integer index of the quality from 1 to 6.
     */
    public void SetGraphics(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /*
     * Setter function for the mouse sensitivity.
     *
     * Parameters:
     * sensitivity -- The mouse sensitivity to set.
     */
    public void SetSensitivity(float sensitivity)
    {
        PlayerController.Inst().m_mouseSens.x = sensitivity;
        PlayerController.Inst().m_mouseSens.y = sensitivity;
        Debug.Log("Sensitivty: " + sensitivity);
    }

    /*
     * Setter function for the Unity volume mixer.
     *
     * Parameters:
     * volume -- Float value from -80 to 0 that the volume is updated to.
     */
    public void SetVolume(float volume)
    {
        // Set the volume
    }
}
