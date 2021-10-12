
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject spawnToObject;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}