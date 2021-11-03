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

    /* Turns the healthbar on or off
    *
    * Parameters:
    * isActive - if true, turn the healthbar on, if false, turn the healthbar off
    */
    public void SetActive(bool isActive)
    {
        transform.Find("HealthBarCanvas").gameObject.SetActive(isActive);
    }

    /* Sets the healthbar value given a percent ranging from [0,1]
    *
    * Parameters:
    * healthPercent - health value ranging from [0,1] to set healthbar to
    */
    public void SetHealth(float healthPercent)
    {
        m_slider.value = healthPercent;
    }

    private void Start()
    {
        m_camera = GameObject.Find("Main Camera");
        m_slider = transform.Find("HealthBarCanvas/_HealthBar").gameObject.GetComponent<Slider>();
        transform.Find("HealthBarCanvas").GetComponent<Canvas>().worldCamera = m_camera.GetComponent<Camera>();

    }

    private void Update()
    {
        transform.rotation = m_camera.transform.rotation;
    }
}
