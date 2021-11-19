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
    public int m_energyBonus;
    public int m_minbonus = 1;
    public int m_maxBonus = 100;
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

        Debug.Log("EnergyOrb#PowerUpPayload: Adding" + m_energyBonus + " charge to flashlight");
        m_Flashlight = GameObject.Find("Flashlight").GetComponent<Flashlight>();
        m_Flashlight.AddBattery(5f);
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    /* Setter function for Energy Bonus
    *
    * Parameters: none
    *
    * Returns: none
    */
    public void setEnergyBonus(int bonus)
    {
        if (bonus < m_minbonus)
        {
            m_energyBonus = m_minbonus;
        }
        else if (bonus > m_maxBonus)
        {
            m_energyBonus = m_maxBonus;
        }
        else
        {
            m_energyBonus = bonus;
        }
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
