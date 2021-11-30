using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class powerUpTests : MonoBehaviour
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("PowerUpTests");
    }
    public HealthOrb script;

    [UnityTest]
    public IEnumerator playerOverHeal()
    {
        int expectedValue = 100;
        GameObject go = GameObject.Find("healthHolder");
        HealthOrb healthOrb = go.GetComponent<HealthOrb>();

        Debug.Log("healthBonus value: " + healthOrb.m_healthBonus);
        Debug.Log("Applying health to player...");
        PlayerData.Inst().TakeHealth(healthOrb.m_healthBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, PlayerData.Inst().GetHealth());
        Debug.Log("Player health is: " + PlayerData.Inst().GetHealth() + " Expected: " + expectedValue);
        Debug.Log("---------------------------------------------");

        Debug.Log("Damaging Player for: 1");
        PlayerData.Inst().TakeDamage(1);
        Debug.Log("Player health is: " + PlayerData.Inst().GetHealth());
        Debug.Log("healthBonus value: " + healthOrb.m_healthBonus);
        Debug.Log("Applying health to player...");
        PlayerData.Inst().TakeHealth(healthOrb.m_healthBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, PlayerData.Inst().GetHealth());
        Debug.Log("Player health is: " + PlayerData.Inst().GetHealth() + " Expected: " + expectedValue);

        Debug.Log("---------------------------------------------");
        healthOrb.m_healthBonus = 100;
        Debug.Log("Setting m_healthBonus to: " + healthOrb.m_healthBonus);
        Debug.Log("Applying health to player...");
        PlayerData.Inst().TakeHealth(healthOrb.m_healthBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, PlayerData.Inst().GetHealth());
        Debug.Log("Player health is: " + PlayerData.Inst().GetHealth() + " Expected: " + expectedValue);
        Debug.Log("---------------------------------------------");

        healthOrb.m_healthBonus = 1000;
        Debug.Log("Setting m_healthBonus to: " + healthOrb.m_healthBonus);
        PlayerData.Inst().TakeHealth(healthOrb.m_healthBonus);
        Debug.Log("Applying health to player...");
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, PlayerData.Inst().GetHealth());
        Debug.Log("Player health is: " + PlayerData.Inst().GetHealth() + " Expected: " + expectedValue);

        yield return null;
    }

    [UnityTest]
    public IEnumerator healthBonusLow()
    {
        GameObject go = GameObject.Find("healthHolder");
        HealthOrb healthOrb = go.GetComponent<HealthOrb>();
        int expectedValue = healthOrb.m_minbonus;

        healthOrb.setHealthBonus(-2);
        Debug.Log("Setting m_healthBonus to: " + healthOrb.m_healthBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, healthOrb.m_healthBonus);

        healthOrb.setHealthBonus(-50);
        Debug.Log("Setting m_healthBonus to: " + healthOrb.m_healthBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, healthOrb.m_healthBonus);

        healthOrb.setHealthBonus(-100);
        Debug.Log("Setting m_healthBonus to: " + healthOrb.m_healthBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, healthOrb.m_healthBonus);

        healthOrb.setHealthBonus(-1000);
        Debug.Log("Setting m_healthBonus to: " + healthOrb.m_healthBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, healthOrb.m_healthBonus);

        yield return null;
    }
    [UnityTest]    
    public IEnumerator healthBonusHigh()
    {
        GameObject go = GameObject.Find("healthHolder");
        HealthOrb healthOrb = go.GetComponent<HealthOrb>();
        int expectedValue = healthOrb.m_maxBonus;

        healthOrb.setHealthBonus(102);
        Debug.Log("Setting m_healthBonus to: " + healthOrb.m_healthBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, healthOrb.m_healthBonus);

        healthOrb.setHealthBonus(200);
        Debug.Log("Setting m_healthBonus to: " + healthOrb.m_healthBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, healthOrb.m_healthBonus);

        healthOrb.setHealthBonus(500);
        Debug.Log("Setting m_healthBonus to: " + healthOrb.m_healthBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, healthOrb.m_healthBonus);

        healthOrb.setHealthBonus(1000);
        Debug.Log("Setting m_healthBonus to: " + healthOrb.m_healthBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, healthOrb.m_healthBonus);

        yield return null;
    }

    [UnityTest]    
    public IEnumerator EnergyBonusLow()
    {
        GameObject go2 = GameObject.Find("energyHolder");
        EnergyOrb energyOrb = go2.GetComponent<EnergyOrb>();
        int expectedValue = energyOrb.m_minbonus;

        energyOrb.setEnergyBonus(-2);
        Debug.Log("Setting m_energyBonus to: " + energyOrb.m_energyBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, energyOrb.m_energyBonus);

        energyOrb.setEnergyBonus(-200);
        Debug.Log("Setting m_energyBonus to: " + energyOrb.m_energyBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, energyOrb.m_energyBonus);

        energyOrb.setEnergyBonus(-500);
        Debug.Log("Setting m_energyBonus to: " + energyOrb.m_energyBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, energyOrb.m_energyBonus);

        energyOrb.setEnergyBonus(-1000);
        Debug.Log("Setting m_energyBonus to: " + energyOrb.m_energyBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, energyOrb.m_energyBonus);

        yield return null;
    }

    [UnityTest]    
    public IEnumerator EnergyBonusHigh()
    {
        GameObject go2 = GameObject.Find("energyHolder");
        EnergyOrb energyOrb = go2.GetComponent<EnergyOrb>();
        int expectedValue = energyOrb.m_maxBonus;

        energyOrb.setEnergyBonus(102);
        Debug.Log("Setting m_energyBonus to: " + energyOrb.m_energyBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, energyOrb.m_energyBonus);

        energyOrb.setEnergyBonus(200);
        Debug.Log("Setting m_energyBonus to: " + energyOrb.m_energyBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, energyOrb.m_energyBonus);

        energyOrb.setEnergyBonus(500);
        Debug.Log("Setting m_energyBonus to: " + energyOrb.m_energyBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, energyOrb.m_energyBonus);

        energyOrb.setEnergyBonus(1000);
        Debug.Log("Setting m_energyBonus to: " + energyOrb.m_energyBonus);
        Debug.Log("Testing...");
        Assert.AreEqual(expectedValue, energyOrb.m_energyBonus);

        yield return null;
    }
    
}
