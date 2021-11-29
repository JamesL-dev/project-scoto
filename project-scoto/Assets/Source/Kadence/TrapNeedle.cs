using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Needle Trap Subclass
*/
public class TrapNeedle : TrapBase
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        m_damage = 25f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider collision) // OnTriggerEnter vs OnTriggerStay?
    {
        if(collision.CompareTag("Player")) //collision.gameObject.CompareTag("Player")
        {
            Debug.Log("Subclass: Player triggered trap.");
            PlayerData.Inst().TakeDamage(m_damage);
        }
    }
}
