using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HaydenHelpers : MonoBehaviour
{

    private static HaydenHelpers instance;
    void Awake()
    {
        instance = this;
    }

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

    public static void SetAnimation(Animator animator, string animation)
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                if (param.name == animation)
                {
                    continue;
                }
                animator.ResetTrigger(param.name);
            }
        }

        animator.SetTrigger(animation);
    }

    public static void StartClock(float time, Action functionToCall)
    {
        IEnumerator coroutine = TimerCoroutine(time, functionToCall);
        instance.StartCoroutine(coroutine);
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
}