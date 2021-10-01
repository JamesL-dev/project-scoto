    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Flashlight : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] private int m_maxBatteryLevel;
        [SerializeField] private float m_timeBetwenFlashlightDeplete;
        [SerializeField] private int m_flashlightDepleteAmnt;
        [SerializeField] private AudioSource clickOnSound;
        [SerializeField] private AudioSource clickOffSound;
        private int m_batteryLevel;
        private bool m_isFlashlightOn;
        private bool m_isBatteryZero;
        private bool m_currentlyDepleting;



        void Start()
        {
            m_batteryLevel = m_maxBatteryLevel;
            m_isFlashlightOn = true;
            m_isBatteryZero = false;
            m_currentlyDepleting = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_isFlashlightOn && m_currentlyDepleting == false)
            {
                Invoke(nameof(DepleteBattery), m_timeBetwenFlashlightDeplete);
                m_currentlyDepleting = true;
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

        public void ToggleFlashlight()
        {
            if (m_isFlashlightOn)
            {
                m_isFlashlightOn = false;
                clickOffSound.Play();
            }
            else
            {
                m_isFlashlightOn = true;
                clickOnSound.Play();
            }
        }

        private void DepleteBattery()
        {
            if (m_isBatteryZero) return;

            m_batteryLevel -= m_flashlightDepleteAmnt;
            if (m_batteryLevel < 0)
            {
                m_batteryLevel = 0;
                m_isBatteryZero = true;
            }
            m_currentlyDepleting = false;
        }
    }
