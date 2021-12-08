using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    public AudioClip m_ambience, m_pauseMusic;
    private bool m_isPaused;

    void Start()
    {
        m_isPaused = true;
        PlayAmbience();
    }

    void Update()
    {
        PlayAmbience();
    }

    private void PlayAmbience()
    {
        AudioSource audioData = GetComponent<AudioSource>();

        if (Time.deltaTime == 0 && !m_isPaused)
        {
            m_isPaused = true;
            audioData.clip = m_pauseMusic;
            audioData.Play();
        }
        else if (Time.deltaTime != 0 && m_isPaused)
        {
            m_isPaused = false;
            audioData.clip = m_ambience;
            audioData.Play();
        }
    }
}
