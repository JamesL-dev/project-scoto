/*
 * Filename: BatteryBar.cs
 * Developer: Hayden Carroll
 * Purpose: This file implements the BatteryBar class.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Provides logic for the player battery bar for the flashlight
 */
public class BatteryBar : MonoBehaviour
{
    private Slider m_slider;
    private Flashlight m_flashlight;

    private void Start()
    {
        m_slider = GameObject.Find("_BatteryBar").GetComponent<Slider>();
        m_flashlight = GameObject.Find("Flashlight").GetComponent<Flashlight>();
    }

    private void Update()
    {
        m_slider.value = m_flashlight.GetBatteryPercent();
    }
}
