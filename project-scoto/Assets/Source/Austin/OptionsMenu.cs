/*
 * Filename: OptionsMenu.cs
 * Developer: Austin Kugler
 * Purpose: This file includes a singleton class for functionality related to the options menu.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Singleton class for options functionality, allowing changes to be made.
 */
public sealed class OptionsMenu : MonoBehaviour
{
    private static OptionsMenu m_instance;

    /* Gets a reference to the instance of the singleton, creating the instance if necessary.
     *
     * Returns:
     * OptionsMenu -- Reference to the OptionsMenu instance.
     */
    public static OptionsMenu Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("OptionsMenu").GetComponent<OptionsMenu>();
        }
        return m_instance;
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

    /*
     * Setter function for the Unity graphics quality.
     *
     * Parameters:
     * qualityIndex -- Integer index of the quality from 1 to 6.
     */
    public void SetGraphics(int qualityIndex)
    {
        Debug.Log(qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
