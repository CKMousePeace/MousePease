using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class CNavMeshGen : MonoBehaviour
{

    [SerializeField]private NavMeshSurface[] m_Surfaces;
    //NavMesh 새로 생성하는 스크립트 (현재는 사용 X)

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
