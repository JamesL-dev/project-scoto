/*
 * Filename: ArrowTest.cs
 * Developer: Rodney McCoy
 * Purpose: added to arrow for stress test
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
 * Main Class
 *
 * Member Variables:
 * m_timer -- time since instantiate
 * m_maxTime -- time until test fails
 */
public class ArrowTest : MonoBehaviour
{
    int m_timer = 0, m_maxTime = 60;

    void FixedUpdate()
    {
        m_timer ++;
        if((m_timer >= m_maxTime || gameObject.transform.position.y <= 0) && gameObject.transform.parent == null)
        {
            RodneyStressTest.m_testSucceeded = true;
            //Debug.LogError("Stress Test Succedded. An arrow didn't connect with the heavy enemy. Arrow position is " + gameObject.transform.position.ToString());
        }
    }
}

