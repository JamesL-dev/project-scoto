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

    private float m_focusedTime;
    private void Start()
    {
        m_batteryLevel = m_maxBatteryLevel;
        m_isFlashlightOn = true;
        m_light.enabled = true;
        m_isFlashlightFocused = false;
        m_light.spotAngle = m_normalFlashlightAngle;
        m_focusedTime = 0;
    }

    private void Update()
    {
        if (m_isFlashlightFocused)
        {
            m_focusedTime += Time.deltaTime;

            if (m_focusedTime >= m_timeBetwenFlashlightDeplete)
            {
                DepleteBattery();
                m_focusedTime = 0;
            }
            if (m_batteryLevel == 0)
            {
                OnNormalFlashlight();
            }
        }

        Debug.Log(m_batteryLevel);
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
        m_isFlashlightFocused = true;
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
        }

    }
    private void OnNormalFlashlight()
    {
        m_isFlashlightFocused = false;
        StartCoroutine("ToNormalTransition");
    }
}
