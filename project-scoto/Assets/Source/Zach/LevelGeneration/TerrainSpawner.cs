using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public GameObject m_boulder, m_pillar;
    public float m_roomSize;

    void Start()
    {
        m_boulder = Instantiate(m_boulder, transform);
        m_boulder.transform.position += new Vector3(5, 0, 5);
        m_pillar = Instantiate(m_pillar, transform);
        m_pillar.transform.position += new Vector3(-5, 0, 5);
    }

    void Update()
    {
        
    }
}
