using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int CurrentWeapon = 0;
    public bool EnableAttack = 0;
    public bool BowFound = 0;
    public bool TridentFound = 0;
    public int FireAmount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void FireTrident()
    {
        Debug.Log("Fired Trident");
    }
}
