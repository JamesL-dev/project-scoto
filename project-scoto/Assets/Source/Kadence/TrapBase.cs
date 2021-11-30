using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Trap Superclass

*/
public class TrapBase : MonoBehaviour
{
    protected float m_damage = 10f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void OnTriggerEnter(Collider collision) // OnTriggerEnter vs OnTriggerStay?
    {
        if(collision.CompareTag("Player")) //collision.gameObject.CompareTag("Player")
        {
            Debug.Log("Superclass: Player triggered trap.");
            PlayerData.Inst().TakeDamage(m_damage);
        }
    }

/*
    protected virtual void TrapDamage(){
        PlayerData.Inst().TakeDamage(m_damage);
    }
*/

}
