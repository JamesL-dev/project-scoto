using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSuperclass : MonoBehaviour
{
    int Damage = 0;
    int timer = 0;
    int MAX_TIME = 10;
    public GameObject projectile = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnProjectile() {
        Instantiate(projectile);
    }

    public void SpawnProjectile(Vector3 position, Quaternion rotation) {
        Instantiate(projectile, position, rotation);
    }
}
