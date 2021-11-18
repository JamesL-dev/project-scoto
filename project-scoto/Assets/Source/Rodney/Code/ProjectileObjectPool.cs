/*
 * Filename: ProjectileObjectPool.cs
 * Developer: Rodney McCoy
 * Purpose: Implements object pool design pattern for grenades and arrows
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Main Class
 *
 * Member Variables:
 * m_poolAmt -- amount of object pools
 * m_key -- maps ProjectileType enum to the index projectile type's index in m_objects
 * m_serializeKeyValues -- user assigns enum - index pairs for m key array here, then this is assigned to m_key dictionary
 * m_queueSize -- used for debugging. shows size of queue
 * m_objects -- array of ObjectPool's
 */
public sealed class ProjectileObjectPool : MonoBehaviour 
{  
    public enum ProjectileType
    {
        Arrow = 0,
        Grenade
    }

    [Serializable] public class ObjectPool
    {
        public GameObject objectPrefab;
        public ConcurrentQueue<GameObject> objectPool = new ConcurrentQueue<GameObject>();
    }

    const int m_poolAmt = 2;
    [SerializeField] public int[] m_queueSize = new int[m_poolAmt];
    [SerializeField] public ConcurrentDictionary<ProjectileType, int> m_key = new ConcurrentDictionary<ProjectileType, int>(1, m_poolAmt);
    [SerializeField] public ProjectileType[] m_serializeKeyValues = new ProjectileType[m_poolAmt];
    [SerializeField] public ObjectPool[] m_objects = new ObjectPool[m_poolAmt]; 
    // Index a object Pool doing m_objects[m_key[(int)ProjectileType]].objectPool   

    void Start()
    {
        for(int i = 0; i < m_poolAmt; i++)
        {
            m_key[m_serializeKeyValues[i]] = i;
        }
    }

    void FixedUpdate()
    {
        for(int i = 0; i < m_poolAmt; i++)
        {
            m_queueSize[i] = m_objects[i].objectPool.Count;
        }
    }


    /*
     * Gives reusable to outside entity
     *
     * Parameter:
     * type -- enum of projectile type
     *
     * Returns:
     * GameObject -- instance of projectile for weapon subclass to use
     */
    public GameObject acquireReusable(ProjectileType type)
    {
        GameObject reusable;

        if(m_objects[m_key[type]].objectPool.IsEmpty)
        {
            reusable = Instantiate(m_objects[m_key[type]].objectPrefab, Vector3.zero, Quaternion.identity);
            reusable.SetActive(false);
        }
        else
        {
            if(!m_objects[m_key[type]].objectPool.TryDequeue (out reusable))
            {
                Debug.LogError("Rodney's ReusablePool for projectiles attemped to pop a projectile off and empty stack when it wasnt supposed to");
            }
        }

        reusable.transform.SetParent(null);
        return reusable;
    }

    /*
     * Returns reusable to object pool
     * 
     * Parameters:
     * type -- enum of projectile type
     * GameObject -- instance of projectile weapon subclass passed to object pool
     */
    public void releaseReusable(ProjectileType type, GameObject reusable)
    {
        reusable.SetActive(false);
        m_objects[m_key[type]].objectPool.Enqueue(reusable);
        reusable.transform.SetParent(null);
    }


    /* Members to created singleton design pattern for object pool */
    private static ProjectileObjectPool m_instance;
    public static ProjectileObjectPool Inst()
    {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("Inventory").GetComponent<ProjectileObjectPool>();
        }
        return m_instance;
    }
    private ProjectileObjectPool() {}
}

