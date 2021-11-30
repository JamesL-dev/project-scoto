/*
 * Filename: GreekProjectile.cs
 * Developer: Rodney McCoy
 * Purpose: Script attached to each grenade projectile
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Main Class
 *
 * Member Variables:
 * m_damage -- amount of damage grenade gives on inpact
 * m_fireAtExplosion -- determines if ring of fire is instantiate at AOE effect
 * m_explosion -- gameobject for explosion
 * m_fire -- gameobject for fire at AOE effect
 * m_radius -- radius of explosion
 * m_maxTime -- time tell explosion
 * m_timer -- time since projectile instantiated
 * m_objPool -- reference to object pool to obtain projectiles to fire
 */
public class GreekProjectile : MonoBehaviour
{
    [SerializeField] public float m_damage = 100F;
    public bool m_fireAtExplosion = true;
    public GameObject m_explosion, m_fire;

    float m_radius = 5F;
    int m_maxTime = 75, m_timer = 0;
    ProjectileObjectPool m_objPool;

    void Awake() 
    { 
        m_objPool = GameObject.Find("ProjectileObjectPool").GetComponent<ProjectileObjectPool>();
        m_timer = 0;
    }

    void FixedUpdate() 
    {
        m_timer ++;
        if(m_timer > m_maxTime) 
        {
            Instantiate(m_explosion, gameObject.transform.position, gameObject.transform.rotation); 
            if(m_fireAtExplosion)
            {
                // Creates ring of fire
                Instantiate(m_fire, new Vector3(gameObject.transform.position.x, .2F, gameObject.transform.position.z), 
                    Quaternion.LookRotation(Vector3.right, Vector3.up)); 
                m_fire.transform.localScale = new Vector3(3F, 1.5F, 3F);

                // Deals explosion damage to all enemies in m_radius range
                Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, m_radius);
                foreach (var hitCollider in hitColliders)
                {
                    BaseEnemy enemy = BaseEnemy.CheckIfEnemy(hitCollider);
                    if (enemy) { enemy.HitEnemy(BaseEnemy.WeaponType.Grenade, m_damage); }
                }
            }
            // Pass grenade back to reusable / object pool
            m_timer = 0;
            m_objPool.releaseReusable(ProjectileObjectPool.ProjectileType.Grenade, gameObject);
        }
    }
}

