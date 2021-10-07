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
        yield return new WaitForSeconds(5);

        Inv_Flashlight flashlight = GameObject.Find("FlashlighContainer").GetComponent<Inv_Flashlight>();
        flashlight.NotFound();
        GameObject.Find("TridentContainer").GetComponent<Trident>().NotFound();
        GameObject.Find("BowContainer").GetComponent<Bow>().NotFound();
        GameObject.Find("GreekFireContainer").GetComponent<GreekFire>().NotFound();

        yield return null;
        
        Assert.IsTrue(flashlight.isFound());
    }

    [UnityTest]
    public IEnumerator InvBounds()
    {   
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(5);

        WeaponManager object = GameObject.Find("Inventory").GetComponent<WeaponManager>();
        
        object.CurrentWeapon = -1;
        yield return null;
        Assert.AreEqual(object.CurrentWeapon, object.InvSize - 1);

        object.CurrentWeapon = InvSize;
        yield return null;
        Assert.AreEqual(object.CurrentWeapon, 0);
    }
}
