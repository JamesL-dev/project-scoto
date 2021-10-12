using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour 
{
    private static bool jump, sprint, IsSuccessMode;
    private static int Counter, SlackTime;
            
    public static bool On() { if (Demo.Counter >= Demo.SlackTime) return true; return false; }

    public static float Jump() { if(Demo.jump) return 1F; return 0F; }

    public static float Sprint() { if(Demo.sprint) return 1F; return 0F; }

    public static Vector2 Move()
    {
        return Vector2.zero;
    }


    public static void ResetTimer() { if(On()) Debug.Log("Demo Mode Turned Off."); Demo.Counter = 0; }

    public static void SwapSuccessMode() {Demo.IsSuccessMode = !Demo.IsSuccessMode; }

    public static int MaxSeconds() { return Demo.SlackTime/60; }

    public static void ChangeSlackTime(int x) 
    {
        switch(x) 
        {
            case 1:
                SlackTime = 300; // 5 seconds
                break;
            case 2:
                SlackTime = 600; // 10 seconds
                break;
            case 3:
                SlackTime = 1200; // 20 seconds
                break;
            case 4:
                SlackTime = 2400; // 40 seconds
                break;
            case 5:
                SlackTime = 7200; // 2 minutes
                break;
            default:
                SlackTime = 300;
                Debug.LogError("Input to ChangeSlackTime() Out of Bounds. Must be 1 - 5 inclusive.");
                break;
        }
    }


    void Awake()
    {
        jump = sprint = false;
        IsSuccessMode = true;
        Counter = 0;
        ChangeSlackTime(3);
        if(SlackTime < 0) {Debug.LogError("Slack Time must be a positive value!");}
    }

    void FixedUpdate()
    {
        Counter++; 
        if(Counter == SlackTime) { Debug.Log("Demo Mode Turned On");}
        if(On() && IsSuccessMode)
        {
            // Success Mode

        }
        else if(On() && !IsSuccessMode)
        {
            // Failure Mode

        }
    }
}
