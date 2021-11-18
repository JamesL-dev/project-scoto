/*
 * Filename: Flashlight.cs
 * Developer: Hayden Carroll
 * Purpose: This file implements the Flashlight class.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Implements all logic for the player flashlight
 */
public class Flashlight : MonoBehaviour
{
    [SerializeField] private AudioSource m_clickOnSound;
    [SerializeField] private AudioSource m_clickOffSound;
    private Light m_light;
    [SerializeField] private InputActionMap m_inputActionMap;

    private float m_maxBatteryLevel;
    private float m_timeBetwenFlashlightDeplete;
    private float m_flashlightDepleteAmnt;
    private Light m_baseLight;
    private float m_normalFov;
    private float m_normalRange;
    private float m_focusRange;
    private float m_normalIntensity;
    private float m_focusIntensity;
    private float m_focusFov;
    private Camera m_mainCamera;
    private float m_batteryLevel;
    private bool m_isFlashlightOn;
    private bool m_isFlashlightFocused;
    private float m_focusedTime;
    private float m_normalFlashlightAngle;
    private float m_focusFlashlightAngle;
    private float m_focusZoomLvl;

    /* Adds a certain amount of charge to the flashlight battery
    * 
    * Parameters:
    * chargeAmount - amount to add to flashlight battery
    *
    * Returns:
    * bool - True if charge was added to battery, False otherwise
    */
    public bool AddBattery(float chargeAmount)
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
        Debug.Log("ADD BATTERY CALLED");
        return true;
    }

    /* Gets the current percent of the battery
    * 
    * Returns:
    * float - battery percent. Should range from [0, 1]
    */
    public float GetBatteryPercent()
    {
        return m_batteryLevel / m_maxBatteryLevel;
    }

    private void OnEnable()
    {
        m_inputActionMap["FocusFlashlight"].Enable();
        m_inputActionMap["NormalFlashlight"].Enable();
        m_inputActionMap["ToggleFlashlight"].Enable();
    }

    private void OnDisable()
    {
        m_inputActionMap["FocusFlashlight"].Disable();
        m_inputActionMap["NormalFlashlight"].Disable();
        m_inputActionMap["ToggleFlashlight"].Disable();
    }



    private void Awake()
    {   
        m_inputActionMap["FocusFlashlight"].performed += OnFocusFlashlight;
        m_inputActionMap["NormalFlashlight"].performed += OnNormalFlashlight;
        m_inputActionMap["ToggleFlashlight"].performed += OnToggleFlashlight;
    }

    private void Start()
    {
        m_maxBatteryLevel = 100.0f;
        m_timeBetwenFlashlightDeplete = 0.075f;
        m_flashlightDepleteAmnt = 0.5f; // 0.25 works good
        m_focusZoomLvl = 2.0f;
        m_normalFlashlightAngle = 100.0f;
        m_focusFlashlightAngle = 30.0f;
        m_light = transform.Find("lightSource").GetComponent<Light>();
        m_batteryLevel = m_maxBatteryLevel;
        m_isFlashlightOn = true;
        m_light.enabled = true;
        m_isFlashlightFocused = false;
        m_light.spotAngle = m_normalFlashlightAngle;
        m_focusedTime = 0;

        m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_normalFov = m_mainCamera.fieldOfView;
        m_focusFov = m_normalFov/m_focusZoomLvl;

        m_normalRange = m_light.range;
        m_focusRange = m_normalRange * 1.2f;
        m_normalIntensity = m_light.intensity;
        m_focusIntensity = m_normalIntensity * 5.0f;

        m_baseLight = GameObject.Find("baseLightSource").GetComponent<Light>();
    }

    private void Update()
    {
        if (m_isFlashlightFocused && m_isFlashlightOn)
        {
            m_focusedTime += Time.deltaTime;
            CheckRaycastEnemy();
            if (m_focusedTime >= m_timeBetwenFlashlightDeplete)
            {
                DepleteBattery();
                m_focusedTime = 0;
            }
            if (m_batteryLevel == 0)
            {
                OnNormalFlashlight(new InputAction.CallbackContext());
            }
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

    private void CheckRaycastEnemy()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(m_light.transform.position, m_light.transform.forward, out hitInfo, m_light.range))
        {
            BaseEnemy enemy = BaseEnemy.CheckIfEnemy(hitInfo.collider);
            if (enemy)
            {
                enemy.HitEnemy(BaseEnemy.WeaponType.Flashlight, 0);
            }
        }
    }

    private IEnumerator ToFocusTransition()
    {
        StopCoroutine("ToNormalTransition");
        float totalChangeNeeded = m_normalFlashlightAngle - m_focusFlashlightAngle;
        float totalFovNeeded = m_normalFov - m_focusFov;
        float totalRangeNeeded = m_focusRange - m_normalRange;
        float totalIntensityNeeded = m_focusIntensity - m_normalIntensity;

        float smoothness = 0.01f; // time between each function call in seconds
        float duration = 0.2f; // duration in seconds
        float increment = duration/smoothness; //The amount of change to apply.
        while(m_light.spotAngle > m_focusFlashlightAngle)
        {
            m_light.spotAngle -= totalChangeNeeded/increment;
            m_mainCamera.fieldOfView -= totalFovNeeded/increment;
            m_light.range += totalRangeNeeded/increment;
            m_light.intensity += totalIntensityNeeded/increment;
            yield return new WaitForSeconds(smoothness);
        }
        m_light.spotAngle = m_focusFlashlightAngle;
        m_mainCamera.fieldOfView = m_focusFov;
        m_light.range = m_focusRange;
        m_light.intensity = m_focusIntensity;
        m_isFlashlightFocused = true;
    }

    private IEnumerator ToNormalTransition()
    {
        m_isFlashlightFocused = false;
        StopCoroutine("ToFocusTransition");
        float totalChangeNeeded = m_normalFlashlightAngle - m_focusFlashlightAngle;
        float totalFovNeeded = m_normalFov - m_focusFov;
        float totalRangeNeeded = m_focusRange - m_normalRange;
        float totalIntensityNeeded = m_focusIntensity - m_normalIntensity;

        float smoothness = 0.01f; // time between each function call in seconds
        float duration = 0.2f; // duration in seconds
        float increment = duration/smoothness; //The amount of change to apply.
        while(m_light.spotAngle < m_normalFlashlightAngle)
        {
            m_light.spotAngle += totalChangeNeeded/increment;
            m_mainCamera.fieldOfView += totalFovNeeded/increment;
            m_light.range -= totalRangeNeeded/increment;
            m_light.intensity -= totalIntensityNeeded/increment;
            yield return new WaitForSeconds(smoothness);
        }
        m_light.spotAngle = m_normalFlashlightAngle;
        m_mainCamera.fieldOfView = m_normalFov;
        m_light.range = m_normalRange;
        m_light.intensity = m_normalIntensity;
    }

    private void OnFocusFlashlight(InputAction.CallbackContext context)
    {
        if (m_isFlashlightOn && m_batteryLevel != 0)
        {
            StartCoroutine("ToFocusTransition");
        }
    }

    private void OnNormalFlashlight(InputAction.CallbackContext context)
    {
        m_isFlashlightFocused = false;
        StartCoroutine("ToNormalTransition");
    }

    private void OnToggleFlashlight(InputAction.CallbackContext context)
    {
        if (m_isFlashlightOn)
        {
            m_isFlashlightOn = false;
            m_light.enabled = false;
            m_baseLight.enabled = false;
            if (m_isFlashlightFocused)
            {
                m_isFlashlightFocused = false;
                StartCoroutine("ToNormalTransition");
            }
            m_clickOffSound.Play();
        }
        else
        {
            m_isFlashlightOn = true;
            m_light.enabled = true;
            m_baseLight.enabled = true;
            m_clickOnSound.Play();
        }
    }
}
