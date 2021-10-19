// Powerup Superclass

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string powerUpName;
    public bool expiresImmediately;
    public GameObject specialEffect;
    public AudioClip soundEffect;

    // keep reference of player here
    // protected playerStuff playerstuff;


    // keep track of what state the powerup is in
    protected enum PowerUpState
    {
        InAttractMode, IsCollected, IsExpiring
    }

    protected PowerUpState powerUpState;


    // When the powerup is initially spawned it is in an "attract" mode, making it easier for player
    // to spot it. IE: Visual effects, sounds etc.. sparklies.
    protected virtual void Start()
    {
        powerUpState = PowerUpState.InAttractMode;
    }

    // This is used for 3D object interaction
    protected virtual void OnTriggerEnter (Collider other)
    {
        PowerUpCollected(other.gameObject);
    }

    // Make sure the powerup is collected by the player then calls special effects and payload
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

        // collect effects
        PowerUpEffects();

        // payload
        PowerUpPayload();
    }

    // This is to instantiate visual effects and a sound
    protected virtual void PowerUpEffects()
    {
        if (specialEffect != null)
        {
            Instantiate (specialEffect, transform.position, transform.rotation, transform);
        }

        if (soundEffect != null)
        {
            // MainGameController.main.PlaySound (soundEffect);
        }
    }

    // This will apply the powerups payload to the player
    protected virtual void PowerUpPayload()
    {
        Debug.Log("Power Up collected, applying payload for: " + gameObject.name);

        // if instant despawn immmediately
        if (expiresImmediately)
        {
            PowerUpHasExpired();
        }
    }

    // When the powerup has expired a msg will be send and the object will be destroyed
    protected virtual void PowerUpHasExpired()
    {
        if (powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }

        // send message powerup has expired
        Debug.Log("Power Up has expired, removing after a delay for: " + gameObject.name);
        DestroySelfAfterDelay();
    }

    protected virtual void DestroySelfAfterDelay()
    {
        Destroy(gameObject, 10f);
    }
}
