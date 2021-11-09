/*
 * Filename: UIController.cs
 * Developer: Zachariah Preston
 * Purpose: Controls any elements of the UI.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Singleton that controls any elements of the UI.
 *
 * Member variables:
 * m_blackSquare -- GameObject for black fade that is affected.
 * m_instance -- Static intance of itself for the Singleton pattern.
 * m_isFading -- Boolean for the fading animation.
 * m_isBlack -- Boolean for tracking if the screen is currently black.
 * m_timer -- Float for how much time is left on the fade effect.
 * m_timerDuration -- Float for the time it takes to fade in or fade out, in seconds.
 */
public sealed class UIController : MonoBehaviour
{
    public GameObject m_blackSquare;

    private static UIController m_instance;
    private bool m_isFading = false, m_isBlack = true;
    private float m_timer;
    private const float m_timerDuration = 0.5f;

    /* Fade in/fade out effect.
     */
    void Update()
    {
        if (m_isFading)
        {
            // Decrease the timer with speed relative to the timer duration variable.
            m_timer -= Time.deltaTime / m_timerDuration;
            if (m_timer <= 0)
            {
                // Finished fading.
                m_isFading = false;
                m_isBlack = !m_isBlack;
            }
            else
            {
                // Sets the transparency to the current value of the timer, to get a fade effect.
                Color doorFade = m_blackSquare.GetComponent<Image>().color;
                if (m_isBlack)
                    doorFade.a = m_timer;
                else
                    doorFade.a = 1f - m_timer;
                m_blackSquare.GetComponent<Image>().color = doorFade;
            }
        }
    }

    /* Gets a reference to the instance of the singleton, creating the instance if necessary.
     *
     * Returns:
     * UIController -- Reference to the UI controller.
     */
    public static UIController Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("Canvas").GetComponent<UIController>();
        }
        return m_instance;
    }

    /* Triggers the fade effect.
     */
    public void Fade()
    {
        if (!m_isFading)
        {
            m_isFading = true;
            m_timer = 1f;
        }
    }

    /* Gets the status of the screen fade.
     *
     * Returns:
     * bool -- True/false for if the screen is currently black.
     */
    public bool IsBlack()
    {
        return m_isBlack;
    }

    /* Makes the singleton's constructor static.
     */
    private UIController() {}
}

