/*
 * Filename: HaydenHelpers.cs
 * Developer: Hayden Carroll
 * Purpose: This file implements the HaydenHelpers class, 
            a collection of helpful static member variables.
 */
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Singleton class with helpful static functions that are used throughout
 * other files
 */
public sealed class HaydenHelpers : MonoBehaviour
{
    private static HaydenHelpers m_instance = null;

    /* Returns a GameObject if a parent element has a certain tag
    * 
    * Parameters:
    * childObject - child object to search up parent tree from
    * tag - tag of parent to check for
    *
    * Returns:
    * GameObject - game object of first parent with specified tag. If no parent
    * is found, return null
    */
   public static GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }

    /* Removes all current animation triggers and adds a new one given an Animator
    * and a string of the animation trigger name
    * 
    * Parameters:
    * animator - animator to remove animations/set animations to
    * animationTrigger - string with the name of an animation trigger to set
    */
    public static void SetAnimation(Animator animator, string animationTrigger)
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                if (param.name == animationTrigger)
                {
                    continue;
                }
                animator.ResetTrigger(param.name);
            }
        }

        animator.SetTrigger(animationTrigger);
    }

    /* Creates a clock and calls a function when the timer goes off
    * 
    * Parameters:
    * time - time in seconds until clock should go off
    * functionToCall - function to call once clock goes off
    */
    public static void StartClock(float time, Action functionToCall)
    {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("Player").AddComponent<HaydenHelpers>();
        }
        IEnumerator coroutine = TimerCoroutine(time, functionToCall);
        m_instance.StartCoroutine(coroutine);
    }

    private static IEnumerator TimerCoroutine(float time, Action functionToCall)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime <= time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        functionToCall();
    }

    private void Awake()
    {
        m_instance = this;
    }
}
