/*
 * Filename: HealthBar.cs
 * Developer: Hayden Carroll
 * Purpose: This file implements the HealthBar class,
            which is used for enemies
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Implements all logic for the Health bar of enemies
 */
public class HealthBar : MonoBehaviour
{
    private GameObject m_camera;
    private Slider m_slider;
    private BaseEnemy m_enemy;

    /* Turns the healthbar on or off
    *
    * Parameters:
    * isActive - if true, turn the healthbar on, if false, turn the healthbar off
    */
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void Start()
    {
        m_camera = GameObject.Find("Main Camera");
        m_slider = GameObject.Find("_HealthBar").GetComponent<Slider>();
        m_enemy = gameObject.GetComponentInParent<BaseEnemy>();
        transform.Find("HealthBarCanvas").GetComponent<Canvas>().worldCamera = m_camera.GetComponent<Camera>();

    }

    private void Update()
    {
        transform.rotation = m_camera.transform.rotation;
        m_slider.value = m_enemy.GetHealthPercent();
    }
}
