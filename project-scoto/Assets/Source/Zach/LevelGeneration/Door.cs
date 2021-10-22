/*
 * Filename: Door.cs
 * Developer: Zachariah Preston
 * Purpose: Controls the opening of doors when a room is cleared.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Controls the opening of doors when a room is cleared.
 *
 * Member variables:
 * m_isOpen -- Boolean for tracking if the doors in the room have been opened.
 * m_timer -- Float for how much time is left on the door fade.
 * m_timerDuration -- Float for the time it takes for a door to open, in seconds.
 */
public class Door : MonoBehaviour
{
    private bool m_isOpen = false;
    private float m_timer;
    private const float m_timerDuration = 0.25f;

    /* Sets the shader to transparent.
     */
    private void Awake()
    {
        GetComponent<MeshRenderer>().material.shader = Shader.Find("Transparent/Diffuse");
    }

    /* If the door is opened, fade it away and then delete it.
     */
    private void Update()
    {
        if (m_isOpen)
        {
            // Decrease the timer with speed relative to the timer duration variable.
            m_timer -= Time.deltaTime / m_timerDuration;
            if (m_timer <= 0)
            {
                // When timer reaches zero, delete the door.
                Destroy(gameObject);
            }
            else
            {
                // Set the transparency to the current value of the timer, to get a fade effect.
                Color doorFade = GetComponent<MeshRenderer>().material.GetColor("_Color");
                doorFade.a = m_timer;
                GetComponent<MeshRenderer>().material.SetColor("_Color", doorFade);
            }
        }
    }

    /* Opens the door.
     */
    public void OpenDoor()
    {
        m_isOpen = true;
        m_timer = 1f;
    }
}

