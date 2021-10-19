/*
 * Filename: RodneyBoundaryTest.cs
 * Developer: Rodney McCoy
 * Purpose: Boundary Test
 */
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

/*
 * Main Boundary Test Class
 */
public class RodneyBoundaryTest : MonoBehaviour
{
    
    [UnityTest]
    public IEnumerator NoDiscoveredWeapons()
    {   
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2.5F);

        Trident trident = GameObject.Find("TridentContainer").GetComponent<Trident>(); 
        trident.NotFound();
        GameObject.Find("BowContainer").GetComponent<Bow>().NotFound();
        GameObject.Find("GreekFireContainer").GetComponent<GreekFire>().NotFound();

        yield return new WaitForSeconds(.1F);
        Assert.IsTrue(trident.isFound());
        yield return null;
    }

    [UnityTest]
    public IEnumerator InvBounds()
    {   
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2);

        WeaponManager obj_ = GameObject.Find("Inventory").GetComponent<WeaponManager>();
        
        int previous_weapon = obj_.CurrentWeapon();
        yield return new WaitForSeconds(.05F);
        obj_.SetCurrentWeapon(-1);
        yield return new WaitForSeconds(.05F);        
        Assert.AreEqual(obj_.CurrentWeapon(), previous_weapon);

        previous_weapon = obj_.CurrentWeapon();
        yield return new WaitForSeconds(.05F);
        obj_.SetCurrentWeapon(-1000);
        yield return new WaitForSeconds(.05F);
        Assert.AreEqual(obj_.CurrentWeapon(), previous_weapon);

        previous_weapon = obj_.CurrentWeapon();
        yield return new WaitForSeconds(.05F);
        obj_.SetCurrentWeapon(WeaponManager.m_invSize);
        yield return new WaitForSeconds(.05F);
        Assert.AreEqual(obj_.CurrentWeapon(), previous_weapon);

        previous_weapon = obj_.CurrentWeapon();
        yield return new WaitForSeconds(.05F);
        obj_.SetCurrentWeapon(WeaponManager.m_invSize + 1000);
        yield return new WaitForSeconds(.05F);
        Assert.AreEqual(obj_.CurrentWeapon(), previous_weapon);
        yield return null;
    }
}
