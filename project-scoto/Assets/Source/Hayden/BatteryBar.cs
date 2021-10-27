using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider m_slider;
    private Flashlight m_flashlight;
    private void Start()
    {
        m_slider = GameObject.Find("_BatteryBar").GetComponent<Slider>();
        m_flashlight = GameObject.Find("Flashlight").GetComponent<Flashlight>();
    }

    // Update is called once per frame
    private void Update()
    {
        m_slider.value = m_flashlight.CheckBatteryPercent();
        Debug.Log(m_flashlight.CheckBatteryPercent());
    }
}
