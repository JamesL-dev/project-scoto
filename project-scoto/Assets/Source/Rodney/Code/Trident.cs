using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : Weapon 
{
    void Start() 
    {
        MAX_TIME = 20; 
        discovered = true;
    }

    public override void Fire(Vector3 position, Quaternion rotation) 
    { 
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100);

        BaseEnemy enemy = BaseEnemy.CheckIfEnemy(hit.collider);
        if (enemy) { enemy.TakeDamage(Damage); }
    }
}