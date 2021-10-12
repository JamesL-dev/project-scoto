using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class RodneyBoundaryTest : MonoBehaviour
{
    [UnityTest]
    public IEnumerator NoDiscoveredWeapons()
    {   
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(2.5F);

        Inv_Flashlight flashlight = GameObject.Find("FlashlightContainer").GetComponent<Inv_Flashlight>();
        flashlight.NotFound();
        GameObject.Find("TridentContainer").GetComponent<Trident>().NotFound();
        GameObject.Find("BowContainer").GetComponent<Bow>().NotFound();
        GameObject.Find("GreekFireContainer").GetComponent<GreekFire>().NotFound();

        yield return new WaitForSeconds(.1F);
        Assert.IsTrue(flashlight.isFound());
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
        obj_.SetCurrentWeapon(WeaponManager.InvSize);
        yield return new WaitForSeconds(.05F);
        Assert.AreEqual(obj_.CurrentWeapon(), previous_weapon);

        previous_weapon = obj_.CurrentWeapon();
        yield return new WaitForSeconds(.05F);
        obj_.SetCurrentWeapon(WeaponManager.InvSize + 1000);
        yield return new WaitForSeconds(.05F);
        Assert.AreEqual(obj_.CurrentWeapon(), previous_weapon);
        yield return null;
    }
}
