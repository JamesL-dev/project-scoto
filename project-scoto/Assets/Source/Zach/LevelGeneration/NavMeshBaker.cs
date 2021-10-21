using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshBaker
{
    // Start is called before the first frame update
    public List<NavMeshSurface> m_surfaces = new List<NavMeshSurface>();
    void Start()
    {
    }

    public void CreateLevelMesh()
    {
        for (int i=0; i < m_surfaces.Count; i++)
        {
            m_surfaces[i].BuildNavMesh();
        }
    }

    public void AddSurface(GameObject room)
    {
        Component[] components = room.GetComponents(typeof(Component));

        m_surfaces.Add(room.GetComponent<NavMeshSurface>());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
