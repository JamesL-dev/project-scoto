/*
 * Filename: TerrainSpawner.cs
 * Developer: Zachariah Preston
 * Purpose: Creates terrain obstacles within a room.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Creates terrain obstacles within a room.
 *
 * Member variables:
 * m_boulder -- GameObject for the boulder prefab.
 * m_pillar -- GameObject for the pillar prefab.
 * m_roomSize -- Float for the size of the room, for determining how many boulders to spawn.
 * m_boulderList -- List of GameObjects for the instantiated boulders.
 */
public class TerrainSpawner : MonoBehaviour
{
    public GameObject m_boulder, m_pillar;
    public float m_roomSize;

    private List<GameObject> m_boulderList = new List<GameObject>();

    /* Randomly creates boulders based on the room's size.
     */
    void Start()
    {
        if (m_roomSize == 1f)
        {
            // Small: Make 0 or 1 boulder.
            NewBoulderChance(0, 4, 2);
        }
        else if (m_roomSize == 1.5f)
        {
            // Medium: Make 0 to 3 boulders.
            NewBoulderChance(0, -8, -4);
            NewBoulderChance(1, 9, -10);
            NewBoulderChance(2, 5, 11);
        }
        else if (m_roomSize == 2f)
        {
            // Large: Make 0 to 5 boulders.
            NewBoulderChance(0, 1, 1);
            NewBoulderChance(1, -16, -5);
            NewBoulderChance(2, 0, 11);
            NewBoulderChance(3, -3, -13);
            NewBoulderChance(4, 9, -4);
        }
        else
        {
            // Unknown: Make 0 or 1 boulder.
            Debug.LogWarning("Warning: Unexpected room size, defaulting to one boulder.");
            NewBoulderChance(0, 4, 2);
        }
    }

    /* 50/50 chance of generating a boulder at the given position, with random y position, rotation, and scale.
     *
     * Parameters:
     * mazeWidth -- Integer for the width of the maze.
     */
    private void NewBoulderChance(int i, float x, float z)
    {
        if (Random.Range(0, 2) == 0) // coin flip
        {
            GameObject tempBoulder = Instantiate(m_boulder, transform);
            tempBoulder.transform.position += new Vector3(x, Random.Range(-0.5f, 0.5f), z);
            tempBoulder.transform.eulerAngles += new Vector3(0f, Random.Range(0f, 360f), 0f);
            tempBoulder.transform.localScale *= Random.Range(0.8f, 1.2f);
            m_boulderList.Add(tempBoulder);
        }
    }
}

