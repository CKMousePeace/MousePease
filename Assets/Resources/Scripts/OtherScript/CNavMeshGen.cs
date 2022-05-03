using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class CNavMeshGen : MonoBehaviour
{

    [SerializeField]private NavMeshSurface[] m_Surfaces;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UpdateNavMesh();
        }
    }


    private void UpdateNavMesh()
    {
        for (int i = 0; i < m_Surfaces.Length; i++)
        {
            m_Surfaces[i].BuildNavMesh();
        }

    }
}
