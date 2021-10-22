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
 * m_isOpen -- Boolean for tracking if the doors in the room have been opened.
 * m_timer -- Float for how much time is left on the door fade.
 * m_timerDuration -- Float for the time it takes for a door to open, in seconds.
 */
public class Door : MonoBehaviour
{
    private bool m_isOpening = false;
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
        if (m_isOpening)
        {
            // Decrease the timer with speed relative to the timer duration variable.
            m_timer -= Time.deltaTime / m_timerDuration;
            if (m_timer <= 0 && !m_isOpen)
            {
                m_isOpen = true;
                m_isOpening = false;
                // When timer reaches zero, delete the door.
                DisappearDoor();
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
        m_isOpening = true;
        m_timer = 1f;
    }
    
    /* Makes the door disappear.
    */
    public void DisappearDoor()
    {
        GameObject link = new GameObject("link");
        link.transform.parent = transform;
        NavMeshLink meshLink = link.AddComponent<NavMeshLink>();

        meshLink.startPoint = new Vector3(0.0f, -2.5f,  1f);
        meshLink.startPoint += transform.position;
        meshLink.endPoint = new Vector3(0.0f, -2.5f, -1f);
        meshLink.endPoint += transform.position;
        meshLink.width = 2;
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }
}

