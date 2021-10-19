/*
 * Filename: Demo.cs
 * Developer: Rodney McCoy
 * Purpose: Control the demo mode
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Main Class
 *
 * Member Variables:
 * m_jump -- pass message to jump to player movement algorithm
 * m_sprint -- pass message to sprint to player movement algorithm
 * m_isSuccessMode -- determines if player should survive or fail
 * m_counter -- determines how long since last player input
 * m_slackTime -- delta time between last player input and start of demo mode
 */
public class Demo : MonoBehaviour 
{
    [SerializeField] public static int m_counter;

    private static bool m_jump, m_sprint, m_isSuccessMode;
    private static int m_slackTime;
            
    void Awake()
    {
        m_jump = m_sprint = false;
        m_isSuccessMode = true;
        m_counter = 0;
        ChangeSlackTime(2);
        if(m_slackTime < 0) {Debug.LogError("Slack Time must be a positive value!");}
    }

    void FixedUpdate()
    {
        m_counter++; 
        if(m_counter == m_slackTime) { Debug.Log("Demo Mode Turned On");}

        if(On() && m_isSuccessMode)
        {
            // Success Mode

        }
        else if(On() && !m_isSuccessMode)
        {
            // Failure Mode

        }
    }

    /*
     * Tell whether demo mode is on 
     *
     * Returns:
     * bool -- True if demo mode is on
     */
    public static bool On() { if (Demo.m_counter >= Demo.m_slackTime) return true; return false; }

    /*
     * Tell Player Movement Algorithm to Jump
     *
     * Returns:
     * float -- 1F if player should jump
     */
    public static float Jump() { if(Demo.m_jump) return 1F; return 0F; }

    /*
     * Tell Player Movement Algorithm to Sprint
     *
     * Returns:
     * float -- 1F if player should sprint
     */
    public static float Sprint() { if(Demo.m_sprint) return 1F; return 0F; }

    /*
     * Tell Player Movement Algorithm to Move Player
     *
     * Returns:
     * Vector2 showing x and y player movement
     */
    public static Vector2 Move()
    {
        return Vector2.zero;
    }

    /*
     * Tells demo mode that a player input has occured
     */
    public static void ResetTimer() { if(On()) Debug.Log("Demo Mode Turned Off."); Demo.m_counter = 0; }

    /*
     * Tell demo mode to swap from success mode to failure mode and vice versa
     */
    public static void SwapSuccessMode() {Demo.m_isSuccessMode = !Demo.m_isSuccessMode; }

    /*
     * Returns seconds it takes to go into demo mode
     *
     * Returns:
     * seconds tell demo starts with no player input
     */
    public static int MaxSeconds() { return Demo.m_slackTime/60; }

    /*
     * Tell demo mode to change slack time
     *
     * Parameters:
     * int -- seconds of slack time
     */
    public static void ChangeSlackTime(int x) 
    {
        m_slackTime = x * 60;
        if(x < 1) {Debug.LogError("ChangeSlackTime() value is to low. Must be 1 or greater"); m_slackTime = 600;}
    }
}

