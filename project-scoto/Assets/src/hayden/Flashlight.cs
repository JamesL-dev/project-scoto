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
    [SerializeField] private float m_focusZoomLvl;


    private Light m_baseLight;
    private float m_normalFov;
    private float m_normalIntensity;
    private float m_focusIntensity;
    private float m_focusFov;
    private Camera m_mainCamera;
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

        m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_normalFov = m_mainCamera.fieldOfView;
        m_focusFov = m_normalFov/m_focusZoomLvl;

        m_normalIntensity = m_light.intensity;
        m_focusIntensity = m_normalIntensity * 3;

        m_baseLight = GameObject.Find("baseLightSource").GetComponent<Light>();
    }

    private void Update()
    {
        if (m_isFlashlightFocused)
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
                OnNormalFlashlight();
            }
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

    public float CheckBatteryPercent()
    {
        return m_batteryLevel / m_maxBatteryLevel;
    }
    public void OnToggleFlashlight()
    {
        if (m_isFlashlightOn)
        {
            m_isFlashlightOn = false;
            m_light.enabled = false;
            m_baseLight.enabled = false;
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
            if (hitInfo.collider.tag == "Enemy")
            {
                BaseEnemy enemy = hitInfo.collider.gameObject.GetComponent<BaseEnemy>();
                enemy.OnFlashlightHit();
            }
        }
    }
    private IEnumerator ToFocusTransition()
    {
        StopCoroutine("ToNormalTransition");
        float totalChangeNeeded = m_normalFlashlightAngle - m_focusFlashlightAngle;
        float totalFovNeeded = m_normalFov - m_focusFov;
        float totalIntensityNeeded = m_focusIntensity - m_normalIntensity;

        float smoothness = 0.01f; // time between each function call in seconds
        float duration = 0.2f; // duration in seconds
        float increment = duration/smoothness; //The amount of change to apply.
        while(m_light.spotAngle > m_focusFlashlightAngle)
        {
            m_light.spotAngle -= totalChangeNeeded/increment;
            m_mainCamera.fieldOfView -= totalFovNeeded/increment;
            m_light.intensity += totalIntensityNeeded/increment;
            yield return new WaitForSeconds(smoothness);
        }
        m_light.spotAngle = m_focusFlashlightAngle;
        m_mainCamera.fieldOfView = m_focusFov;
        m_light.intensity = m_focusIntensity;
        m_isFlashlightFocused = true;
    }

    private IEnumerator ToNormalTransition()
    {
        StopCoroutine("ToFocusTransition");
        float totalChangeNeeded = m_normalFlashlightAngle - m_focusFlashlightAngle;
        float totalFovNeeded = m_normalFov - m_focusFov;
        float totalIntensityNeeded = m_focusIntensity - m_normalIntensity;

        float smoothness = 0.01f; // time between each function call in seconds
        float duration = 0.2f; // duration in seconds
        float increment = duration/smoothness; //The amount of change to apply.
        while(m_light.spotAngle < m_normalFlashlightAngle)
        {
            m_light.spotAngle += totalChangeNeeded/increment;
            m_mainCamera.fieldOfView += totalFovNeeded/increment;
            m_light.intensity -= totalIntensityNeeded/increment;
            yield return new WaitForSeconds(smoothness);
        }
        m_light.spotAngle = m_normalFlashlightAngle;
        m_mainCamera.fieldOfView = m_normalFov;
        m_light.intensity = m_normalIntensity;
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
