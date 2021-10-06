using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdysseyPuzzle : BasePuzzle
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChancePuzzle", m_delay, m_delay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Fixed Update is called 50 times per second
    void FixedUpdate()
    {

    }

    // Initilizes a puzzle if a random chance occurs
    void ChancePuzzle()
    {
        if (!m_isActive && m_frequency != 0.0f && m_frequency <= Random.Range(0.0f, 1.0f))
        {
            Initialize();
        }
    }

    // Initialize a new puzzle
    void Initialize()
    {
        m_isActive = true;
    }
}
