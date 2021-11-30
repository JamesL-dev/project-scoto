using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Obstacle Spawner Class

Factory Method Design Pattern:
Define an interface for creating an object, but let subclasses decide which class to instantiate. Factory Method lets a class defer instantiation to subclasses.
Defining a "virtual" constructor.

Other design patterns require new classes, whereas Factory Method only requires a new operation.
Factory Method requires subclassing, but doesn't require Initialize.

Why factory method vs. prototype? 
Factory Method: creation through inheritance. Prototype: creation through delegation.
This class has an inheritance hierarchy that exercises polymorphism. 

Class Diagram: 
ObstacleSpawner
^
SpawnTrap
^
SpawnNeedleTrap-----SpawnFireTrap

Static Binding occurs during compile time.
Dynamic Binding occurs during run time. 

Member variables:
m_needleTrap:
m_fireTrap:
m_trapList:
m_roomSize:
m_room
*/

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_needleTrap;
    //private GameObject m_fireTrap; 
    private List<GameObject> m_trapList = new List<GameObject>();
    public float m_roomSize;
    private GameObject m_room;
    
    // Start is called before the first frame update
    
    void Start()
    {
        m_room = gameObject.transform.parent.gameObject;

        if(m_roomSize == 1f){ //Small room (0 - 1 traps)
            SpawnTrap();
        }
        else if(m_roomSize == 1.5f){ //Medium room (0 - 2 traps)
            SpawnTrap();
            SpawnTrap();
        }
        else if(m_roomSize == 2f){ //Large room (0 - 3 traps)
            SpawnTrap();
            SpawnTrap();
            SpawnTrap();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // A superclass specifies all standard and generic behavior (using pure virtual "placeholders" for creation steps). 
    //Factory method is: a static method of a class that returns an object of that class' type.
    public TrapBase SpawnTrap(){
        Vector3 roomSize = m_room.transform.Find("Floor").GetComponent<Collider>().bounds.size; //Hayden's enemy spawner
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z); //enemy factory
        position.x += Random.Range(-roomSize.x/3, roomSize.x/3);
        position.z += Random.Range(-roomSize.z/3, roomSize.z/3);
        //how to make sure that something else isn't already there?
        
        //int magic = Random.Range(0,2); //possible values of 0, 1, 2 for 2 traps
        //if(magic == 1){
            return SpawnTrapNeedle(position);
        //}
/*
        else if(magic == 2){
            return SpawnTrapFire(position);
        }
*/
    }

    // Delegates creation details to subclasses that are supplied by the client.
    private TrapNeedle SpawnTrapNeedle(Vector3 position){
        GameObject tempTrap = Instantiate(m_needleTrap, position, Quaternion.identity);
        m_trapList.Add(tempTrap);
        return tempTrap.GetComponent<TrapNeedle>();
    }

/*
    private TrapFire SpawnTrapFire(Vector3 position){
        GameObject tempTrap = Instantiate(m_fireTrap, position, Quaternion.identity);
        m_trapList.Add(tempTrap);
        return tempTrap.GetComponent<TrapFire>();
    }

*/

}
