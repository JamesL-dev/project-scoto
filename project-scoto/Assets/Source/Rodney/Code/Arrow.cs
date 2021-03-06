/*
 * Filename: Arrow.cs
 * Developer: Rodney McCoy
 * Purpose: This script attaches to each arrow projectile and determines its movement and interactions with other gameobjects
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Main Class
 * 
 * Member Variables:
 * m_damage -- amount of damage given to enemies
 * m_maxTime -- time arrow stays in scene
 * m_velocityScalar -- scalar for velocity vector
 * m_accelerationScalar -- scalar for acceleration vector
 * m_timer -- time since instantiated
 * m_inAir -- determines if arrow has collided with an enemy / ground gameobject
 * m_acceleration -- acceleration vector
 * m_velocity -- velocity vector
 * m_light -- light object attached to arrow
 * m_objPool -- accessor to object pool to get projectiles
 */
public class Arrow : MonoBehaviour
{
    [SerializeField] public float m_damage = 10F, m_maxTime = 120F;

    float m_velocityScalar = .5F, m_accelerationScalar = 1F;
    int m_timer = 0;
    bool m_inAir = true;
    Vector3 m_acceleration = new Vector3(0.0F,-0.001F,0.0F), m_velocity = new Vector3(0,0,0);
    Light m_light;
    ProjectileObjectPool m_objPool;

    void Awake() 
    {
        m_objPool = GameObject.Find("ProjectileObjectPool").GetComponent<ProjectileObjectPool>();
        m_velocity = -(gameObject.transform.rotation * Vector3.up * m_velocityScalar);
        m_acceleration = new Vector3(0.0F,-0.001F,0.0F) * m_accelerationScalar;
        m_light = gameObject.AddComponent<Light>();
        m_light.color = Color.white;
        m_light.range = 20;
        m_light.intensity = .25F;
    }

    void OnEnable()
    {
        m_velocity = -(gameObject.transform.rotation * Vector3.up * m_velocityScalar);
        m_acceleration = new Vector3(0.0F,-0.001F,0.0F) * m_accelerationScalar;
        m_timer = 0;
        m_light.intensity = .25F;
        m_inAir = true;
    }

    void FixedUpdate() 
    {
        m_timer++;
        if(m_inAir) 
        {
            gameObject.transform.position += m_velocity;
            m_velocity += m_acceleration;
        }
        else
        {
            m_timer ++;
            m_light.intensity -= .003F;
        }

        if(m_timer > m_maxTime) 
        {   
            m_objPool.releaseReusable(ProjectileObjectPool.ProjectileType.Arrow, gameObject);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(m_inAir)
        {
            bool ignore = false;

            BaseEnemy enemy = BaseEnemy.CheckIfEnemy(other);
            if (enemy)
            {
                enemy.HitEnemy(BaseEnemy.WeaponType.Arrow, m_damage);
                m_objPool.releaseReusable(ProjectileObjectPool.ProjectileType.Arrow, gameObject);
            }
            else if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) 
            {
                // Do Nothing
            } 
            else
            {
                ignore = true;
            }

            if(!ignore)
            {
                m_inAir = false;
                gameObject.transform.position += .5F*m_velocity ;
            }
        }
    }

    static bool m_testCalled = false;
    float m_velocityScalar2 = 3F;

    /*
     * Function to change private parameters of arrow, for testing only
     */
    public void Test()
    {
        if(!m_testCalled) 
        {
            Debug.LogWarning("Function Arrow.Test() only to be used for testing & debugging.");
            m_testCalled = true;
        }
        m_velocity *= m_velocityScalar2/m_velocityScalar;
        m_maxTime = 1000000;
    }
}

