using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    bool jump, sprint, IsSuccessMode;
    static int Counter, SlackTime;

    void Awake()
    {
        jump = sprint = false;
        IsSuccessMode = true;
        Counter = 0;
        SlackTime = 10; // seconds
    }

    void FixedUpdate()
    {
        Counter++; 
        if(On() && IsSuccessMode)
        {
            // Success Mode

        }
        else if(On() && !IsSuccessMode)
        {
            // Failure Mode

        }
    }

    /*
    1. Ensure rodneySrc is an assembly directives for the PlayerController.cs script
    


    2. Add this block of code to FixedUpdate Function
    if (movement_value != 0 || jump_value != 0 || mouse_value != 0 || sprinting_value != 0)
    {
        Demo.ResetTimer();
    }



    3a. In MovePlayer(), replace
            if (jump_value > 0) {
    with
            if (jump_value > 0 || Demo.Jump()) {
             
    3b. In HorizontalMovement(), replace
        if (sprinting_value > 0 && movement_value.y > 0) {
    with
        if ((sprinting_value > 0 || Demo.Sprint() ) && movement_value.y > 0) {



    4. I will do nothing with rotate, since i have transform.LookAt(), but i have no clue how to do the player movement. SO FRICK
        I think for your code we can do, 
            if (Demo.On()) {movement_value = Demo.Move();}
        at the beginning of horizontal movement function. But Demo.Move currently has nothing, so no movement will happen

    */
    
    public void SwapSuccessMode() {IsSuccessMode = !IsSuccessMode; }

    public int MaxTime() { return SlackTime*60; }

    public bool On() { if (Counter >= MaxTime()) return true; return false; }

    public bool Jump() { if(On() && jump) return true; return false; }

    public bool Sprint() { if(On() && sprint) return true; return false; }

    public void ResetTimer() { Counter = 0; }

    public Vector2 Move()
    {
        return Vector2.zero;
    }




}
