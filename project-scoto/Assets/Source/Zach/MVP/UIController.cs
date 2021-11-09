using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIController : MonoBehaviour
{
    public GameObject m_blackSquare;

    private static UIController m_instance;
    private bool m_isFading = false, m_black = true;
    private float m_timer;
    private const float m_timerDuration = 0.5f;

    void Update()
    {
        if (m_isFading)
        {
            // Decrease the timer with speed relative to the timer duration variable.
            m_timer -= Time.deltaTime / m_timerDuration;
            if (m_timer <= 0)
            {
                m_isFading = false;
                m_black = !m_black;
            }
            else
            {
                // Sets the transparency to the current value of the timer, to get a fade effect.
                Color doorFade = m_blackSquare.GetComponent<Image>().color;
                if (m_black)
                    doorFade.a = m_timer;
                else
                    doorFade.a = 1f - m_timer;
                m_blackSquare.GetComponent<Image>().color = doorFade;
            }
        }
    }

    public static UIController Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("Canvas").GetComponent<UIController>();
        }
        return m_instance;
    }

    public void Fade()
    {
        if (!m_isFading)
        {
            m_isFading = true;
            m_timer = 1f;
        }
    }

    public bool IsBlack()
    {
        return m_black;
    }

    private UIController() {}
}
