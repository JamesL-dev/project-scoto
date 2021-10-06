using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePuzzle : MonoBehaviour
{
    [SerializeField] protected bool m_isActive = false;
    [SerializeField] protected float m_frequency = 0.5f;
    [SerializeField] protected int m_delay = 300;

    // Update is called once per frame
    void Update()
    {
        if (m_frequency < 0.0f)
        {
            m_frequency = 0.0f;
        }
        else if (m_frequency > 1.0f)
        {
            m_frequency = 1.0f;
        }
    }

    public bool IsActive()
    {
        return m_isActive;
    }

    public float GetFrequency()
    {
        return m_frequency;
    }

    public int GetDelay()
    {
        return m_delay;
    }

    public void SetFrequency(float frequency)
    {
        m_frequency = frequency;
    }


}
