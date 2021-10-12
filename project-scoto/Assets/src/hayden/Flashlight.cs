using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Flashlight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int m_maxBatteryLevel;
    [SerializeField] private float m_timeBetwenFlashlightDeplete;
    [SerializeField] private int m_flashlightDepleteAmnt;
    [SerializeField] private AudioSource m_clickOnSound;
    [SerializeField] private AudioSource m_clickOffSound;
    [SerializeField] private Light m_light;
    [SerializeField] private float m_normalFlashlightAngle;
    [SerializeField] private float m_focusFlashlightAngle;
    private int m_batteryLevel;
    private bool m_isFlashlightOn;
    private bool m_isFlashlightFocused;
    private bool m_currentlyDepleting;
    private void Start()
    {
        m_batteryLevel = m_maxBatteryLevel;
        m_isFlashlightOn = true;
        m_light.enabled = true;
        m_currentlyDepleting = false;
        m_isFlashlightFocused = false;
        m_light.spotAngle = m_normalFlashlightAngle;
        
    }

    private void Update()
    {
        if (m_isFlashlightFocused)
        {
            Invoke(nameof(DepleteBattery), m_timeBetwenFlashlightDeplete);
            m_currentlyDepleting = true;
            if (m_batteryLevel == 0)
            {
                m_isFlashlightFocused = false;
            }
        }
    }
    public void OnToggleFocus()
    {
        if (m_isFlashlightFocused)
        {
            OffFocusFlashlight();
        }
        else if (!m_isFlashlightFocused)
        {
            OnFocusFlashlight();
        }
    }
    public bool AddBattery(int chargeAmount)
    {
        // failed to add charge to battery, it is at max
        if (m_batteryLevel == m_maxBatteryLevel)
        {
            return false;
        }
        m_batteryLevel += chargeAmount;
        if (m_batteryLevel > m_maxBatteryLevel)
        {
            m_batteryLevel = m_maxBatteryLevel;
        }
        return true;
    }
    public void OnToggleFlashlight()
    {
        if (m_isFlashlightOn)
        {
            m_isFlashlightOn = false;
            m_light.enabled = false;
            m_clickOffSound.Play();
        }
        else
        {
            m_isFlashlightOn = true;
            m_light.enabled = true;
            m_clickOnSound.Play();
        }
    }
    private void DepleteBattery()
    {
        if (m_batteryLevel == 0) {return;};
        m_batteryLevel -= m_flashlightDepleteAmnt;
        if (m_batteryLevel < 0)
        {
            m_batteryLevel = 0;
        }
        m_currentlyDepleting = false;
    }
    
    private IEnumerator ToFocusTransition()
    {
        StopCoroutine("ToNormalTransition");
        float totalChangeNeeded = m_normalFlashlightAngle - m_focusFlashlightAngle;
        float smoothness = 0.01f; // time between each function call in seconds
        float duration = 0.5f; // duration in seconds
        float increment = duration/smoothness; //The amount of change to apply.
        while(m_light.spotAngle > m_focusFlashlightAngle)
        {
            m_light.spotAngle -= totalChangeNeeded/increment;
            yield return new WaitForSeconds(smoothness);
        }
        m_light.spotAngle = m_focusFlashlightAngle;
    }

    private IEnumerator ToNormalTransition()
    {
        StopCoroutine("ToFocusTransition");
        float totalChangeNeeded = m_normalFlashlightAngle - m_focusFlashlightAngle;
        float smoothness = 0.01f; // time between each function call in seconds
        float duration = 0.5f; // duration in seconds
        float increment = duration/smoothness; //The amount of change to apply.
        while(m_light.spotAngle < m_normalFlashlightAngle)
        {
            m_light.spotAngle += totalChangeNeeded/increment;
            yield return new WaitForSeconds(smoothness);
        }
        m_light.spotAngle = m_normalFlashlightAngle;
    }
    private void OnFocusFlashlight()
    {
        if (m_isFlashlightOn && m_batteryLevel != 0)
        {
            StartCoroutine("ToFocusTransition");
            m_isFlashlightFocused = true;
        }

    }

    private void OffFocusFlashlight()
    {
        m_isFlashlightFocused = false;
        StartCoroutine("ToNormalTransition");
    }
}
