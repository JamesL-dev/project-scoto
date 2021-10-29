/*
 * Filename: Door.cs
 * Developer: Zachariah Preston
 * Purpose: Controls the opening of doors when a room is cleared.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
 * Controls the opening of doors when a room is cleared.
 *
 * Member variables:
 * m_isOpening -- Boolean for the door-opening animation.
 * m_isOpened -- Boolean for tracking if the door has been opened.
 * m_timer -- Float for how much time is left on the door fade.
 * m_timerDuration -- Float for the time it takes for a door to open, in seconds.
 */
public class Door : MonoBehaviour
{
    private bool m_isOpening = false, m_isOpened = false;
    private float m_timer;
    private const float m_timerDuration = 0.25f;

    /* Sets the shader to transparent.
     */
    void Awake()
    {
        GetComponent<MeshRenderer>().material.shader = Shader.Find("Transparent/Diffuse");
    }

    /* If the door is opened, fades it away and then delete it.
     */
    void Update()
    {
        if (m_isOpening)
        {
            // Decrease the timer with speed relative to the timer duration variable.
            m_timer -= Time.deltaTime / m_timerDuration;
            if (m_timer <= 0)
            {
                // When timer reaches zero, disable the door.
                DisappearDoor();
                m_isOpening = false;
                m_isOpened = true;
            }
            else
            {
                // Sets the transparency to the current value of the timer, to get a fade effect.
                Color doorFade = GetComponent<MeshRenderer>().material.GetColor("_Color");
                doorFade.a = m_timer;
                GetComponent<MeshRenderer>().material.SetColor("_Color", doorFade);
            }
        }
    }

    /* Opens the door, if it hasn't been opened already.
     */
    public void OpenDoor()
    {
        if (!m_isOpened) {
            m_isOpening = true;
            m_timer = 1f;
        }
    }
    
    /* Makes the door disappear.
    */
    public void DisappearDoor()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        NavMeshBaker.CreateLevelMesh();
    }
}

