using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public PowerUp m_ThisLoot;
    public int m_LootChance;
}


[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public PowerUp LootPowerup()
    {
        int m_cumulattiveProb = 0;
        int m_currentProb = Random.Range(0, 100);

        for(int i = 0; i < loots.Length; i++)
        {
            m_cumulattiveProb += loots[i].m_LootChance;
            if(m_currentProb <= m_cumulattiveProb)
            {
                return loots[i].m_ThisLoot;
            }
        }
        return null;
    }
}
