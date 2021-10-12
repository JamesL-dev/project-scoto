using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour 
{
    public static bool jump, sprint, IsSuccessMode;
    public static int Counter, SlackTime;
            
    public static void SwapSuccessMode() {Demo.IsSuccessMode = !Demo.IsSuccessMode; }

    public static int MaxTime() { return Demo.SlackTime*60; }

    public static bool On() { if (Demo.Counter >= Demo.MaxTime()) return true; return false; }

    public static bool Jump() { if(Demo.On() && Demo.jump) return true; return false; }

    public static bool Sprint() { if(Demo.On() && Demo.sprint) return true; return false; }

    public static void ResetTimer() { Demo.Counter = 0;}

    public static Vector2 Move()
    {
        return Vector2.zero;
    }

    void Awake()
    {
        jump = sprint = false;
        IsSuccessMode = true;
        Counter = 0;
        SlackTime = 5; // seconds  
    }

    void FixedUpdate()
    {
        Counter++; 
        if(On() && IsSuccessMode)
        {
            // Success Mode
            Debug.Log("IT WORKS");

        }
        else if(On() && !IsSuccessMode)
        {
            // Failure Mode
            Debug.Log("IT WORKS, BUT FALSE");

        }
    }
}
