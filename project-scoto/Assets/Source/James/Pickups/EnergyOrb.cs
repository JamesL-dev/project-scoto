/*
* Filename: EnergyOrb.cs
* Developer: James Lasso
* Purpose: Health pickup contains payload information and other overrides.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Energy powerup.
* Contains payload and overrides.
*
* Member variables: none
*/
public class EnergyOrb : PowerUp
{
    public int m_EnergyBonus;
    GameObject m_player;
    public Transform m_target;
    public float MinModifier = 5;
    public float MaxModifier = 12;
    Vector3 m_velocity = Vector3.zero;
    bool isFollowing = false;
    private Flashlight m_Flashlight;

    protected override void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("DropLootTracker");
        m_target = m_player.transform;
        base.Start();
    }
    /* Function that contains payload information.
    *  Default powerup payload is overridden here
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected override void PowerUpPayload()
    {
        base.PowerUpPayload();

        Debug.Log("EnergyOrb#PowerUpPayload: Adding" + m_EnergyBonus + " charge to flashlight");
        m_Flashlight = GameObject.Find("Flashlight").GetComponent<Flashlight>();
        m_Flashlight.AddBattery(5f);
    }

    /* Function to destroy object when picked up
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected override void DestroySelfAfterDelay()
    {
        Destroy(gameObject); 
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    /* Function that updates every frame.
    *  In this case its used to animate the object.
    *
    * Parameters: none
    *
    * Returns: none
    */
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, m_target.position, ref m_velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
        }
    }
}
