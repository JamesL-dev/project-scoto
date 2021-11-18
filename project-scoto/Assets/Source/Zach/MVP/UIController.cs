/*
 * Filename: UIController.cs
 * Developer: Zachariah Preston
 * Purpose: Controls any elements of the UI.
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/*
 * Singleton that controls any elements of the UI.
 *
 * Member variables:
 * m_blackSquare -- GameObject for black fade that is affected.
 * m_levelCounter -- GameObject for level counter text.
 * m_instance -- Static intance of itself for the Singleton pattern.
 * m_isFading -- Boolean for the fading animation.
 * m_isBlack -- Boolean for tracking if the screen is currently black.
 * m_isCounterFading -- Boolean for the level counter fading animation.
 * m_timer -- Float for how much time is left on the fade effect.
 * m_counterTimer -- Float for how much time is left on the level counter fade effect.
 * m_timerDuration -- Float for the time it takes to fade in or fade out, in seconds.
 */
public sealed class UIController : MonoBehaviour
{
    public GameObject m_blackSquare;
    public TextMeshProUGUI m_levelCounter;

    private static UIController m_instance;
    private bool m_isFading = false, m_isBlack = true, m_isCounterFading = false;
    private float m_timer, m_counterTimer;
    private const float m_timerDuration = 0.5f;

    /* Tests for fade effects.
     */
    void Update()
    {
        if (m_isFading)
            Inst().FadeUpdate();

        if (m_isCounterFading)
            Inst().CounterFadeUpdate();
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

    /* Fade in/fade out effect.
     */
    private void FadeUpdate()
    {
        // Decrease the timer with speed relative to the timer duration variable.
        m_timer -= Time.deltaTime / m_timerDuration;
        if (m_timer <= 0)
        {
            // Finished fading.
            m_isFading = false;
            m_isBlack = !m_isBlack;

            // Show level counter.
            if (!m_isBlack)
            {
                Inst().LevelCounterPopup();
            }
        }
        else
        {
            // Sets the transparency to the current value of the timer, to get a fade effect.
            Color fade = m_blackSquare.GetComponent<Image>().color;
            if (m_isBlack)
                fade.a = m_timer;
            else
                fade.a = 1f - m_timer;
            m_blackSquare.GetComponent<Image>().color = fade;
        }
    }

    /* Fade in/fade out effect for level counter.
     */
    private void CounterFadeUpdate()
    {
        // Decrease the timer with speed relative to the timer duration variable.
        m_counterTimer -= Time.deltaTime / m_timerDuration;
        if (m_counterTimer <= 0f)
        {
            // Finished fading.
            m_isCounterFading = false;
        }
        else if (m_counterTimer > 0f && m_counterTimer <= 1f)
        {
            // Sets the transparency to the current value of the timer, to get a fade effect.
            Color fade = m_levelCounter.color;
            fade.a = m_counterTimer;
            m_levelCounter.color = fade;
        }
        else if (m_counterTimer > 4f && m_counterTimer <= 5f)
        {
            // Sets the transparency to the current value of the timer, to get a fade effect.
            Color fade = m_levelCounter.color;
            fade.a = 5f - m_counterTimer;
            m_levelCounter.color = fade;
        }
    }

    /* Displays the current level number.
     */
    private void LevelCounterPopup()
    {
        m_isCounterFading = true;
        m_counterTimer = 5f;
        m_levelCounter.SetText("LEVEL {0}", LevelGeneration.Inst().GetLevelNum());
        m_levelCounter.gameObject.SetActive(true);
    }
}

