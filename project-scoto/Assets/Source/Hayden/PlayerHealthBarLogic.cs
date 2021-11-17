/*
 * Filename: PlayerHealthBarLogic.cs
 * Developer: Hayden Carroll
 * Purpose: This file implements the PlayerHealthBarLogic class.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Provides logic for the player battery bar for the flashlight
 */
public class PlayerHealthBarLogic : MonoBehaviour
{
    private Slider m_slider;
    private PlayerData m_player;

    private void Start()
    {
        m_slider = GameObject.Find("_HealthBar").GetComponent<Slider>();
        m_player = GameObject.Find("Player").GetComponent<PlayerData>();
    }

    private void Update()
    {
        m_slider.value = m_player.GetHealth() / 100.0f;
    }
}
