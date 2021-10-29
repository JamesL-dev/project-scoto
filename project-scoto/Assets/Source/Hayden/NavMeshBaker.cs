using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* 
* navmeshbaker class that generates the whole level mesh at once
*/
public class NavMeshBaker
{
    /* CreateLevelMesh recreates the whole level navmesh
    *
    */
    public static void CreateLevelMesh()
    {
        GameObject levelGeneration = GameObject.Find("LevelGenerator");
        levelGeneration.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
