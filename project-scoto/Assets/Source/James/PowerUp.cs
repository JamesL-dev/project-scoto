/*
* Filename: PowerUp.cs
* Developer: James Lasso
* Purpose: Superclass for powerup/pickup system
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* PowerUp Class
* Default values and characteristics of all powerups and pickups
*
* Member variables: none
* 
*/
public class PowerUp : MonoBehaviour
{
    public string powerUpName;
    public bool expiresImmediately;
    public GameObject m_specialEffect;
    [SerializeField]
    public AudioClip m_soundEffect;
    protected AudioSource m_soundEffectSource;
    protected PowerUpState powerUpState;

    // keep reference of player here
    // protected playerStuff playerstuff;

    /* Function to keep track of what state the powerup is in.
    *
    * Parameters: none
    *
    * Returns:
    * enum -- current state
    */
    protected enum PowerUpState
    {
        InAttractMode, IsCollected, IsExpiring
    }

    /* Function to set the state the powerup is in when it is intially spawned.
    *  Powerups start in 'attract mode' by default
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected virtual void Start()
    {
        powerUpState = PowerUpState.InAttractMode;

        m_soundEffectSource = gameObject.AddComponent<AudioSource>();
        m_soundEffectSource.clip = m_soundEffect;
        m_soundEffectSource.volume = 0.5f; 
        m_soundEffectSource.spatialBlend = 0;
        m_soundEffectSource.maxDistance = 25.0f;
        m_soundEffectSource.rolloffMode = AudioRolloffMode.Linear;
    }

    /* Function for 3D object interaction.
    *
    * Parameters:
    * Collider other -- any other collider than itself
    *
    * Returns: none
    */
    protected virtual void OnTriggerEnter (Collider collision)
    {
            PowerUpCollected(collision.gameObject);
    }

    /* Function handles when powerup is collected by the player.
    *  Special effects and payload are then called.
    *
    * Parameters:
    * gameObjectCollectingPowerUp -- what is collecting powerup
    *
    * Returns: none
    */
    protected virtual void PowerUpCollected(GameObject gameObjectCollectingPowerUp)
    {
        if (gameObjectCollectingPowerUp.tag != "Player")
        {
            return;
        }

        if (powerUpState == PowerUpState.IsCollected || powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsCollected;

        PowerUpEffects(); // Collect special effects

        PowerUpPayload(); // Call paylod
    }

    /* Function to instantiate visual effects and sound
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected virtual void PowerUpEffects()
    {
        if (m_specialEffect != null)
        {
            Instantiate (m_specialEffect, transform.position, transform.rotation, transform);
        }

        if (m_soundEffect != null)
        {
            m_soundEffectSource.Play();
        }
    }

    /* Function to apply the powerups payload to the player
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected virtual void PowerUpPayload()
    {
        Debug.Log("Power Up collected, applying payload for: " + gameObject.name);

        // if instant despawn immmediately
        if (expiresImmediately)
        {
            PowerUpHasExpired();
        }
    }

    /* Function to send a message that the powerup has expired
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected virtual void PowerUpHasExpired()
    {
        if (powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsExpiring;

        // send message powerup has expired
        Debug.Log("Power Up has expired, removing after a delay for: " + gameObject.name);
        DestroySelfAfterDelay();
    }

    /* Function to destroy the powerup.
    *
    * Parameters: none
    *
    * Returns: none
    */
    protected virtual void DestroySelfAfterDelay()
    {
        //Destroy(transform.parent.gameObject);
        Debug.Log("PowerUp#DestroySelfAfterDelay: I have been destroyed");
        Destroy(gameObject, 0.5f);
    }
}
