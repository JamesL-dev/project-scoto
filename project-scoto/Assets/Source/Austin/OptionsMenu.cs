/*
 * Filename: OptionsMenu.cs
 * Developer: Austin Kugler
 * Purpose: This file includes a class for functionality related to the options menu.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for options functionality, allowing changes to be made.
 */
public class OptionsMenu : MonoBehaviour
{
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
