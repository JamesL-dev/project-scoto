/*
* Filename: Follow.cs
* Developer: James Lasso
* Purpose: Makes object this is attatched to follow a set target
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform m_target;
    public float MinModifier = 5;
    public float MaxModifier = 12;
    Vector3 m_velocity = Vector3.zero;
    bool isFollowing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, m_target.position, ref m_velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
        }
    }
}
