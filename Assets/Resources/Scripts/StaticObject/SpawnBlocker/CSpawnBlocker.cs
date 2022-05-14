using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpawnBlocker : MonoBehaviour
{


    [SerializeField] private Transform[] m_BlockerPoints;

    [SerializeField] private GameObject Blocker;
    [SerializeField] private bool m_CanSpawn = false;
    [SerializeField] private float m_SpawnTime = 0;
    private float m_Currtime = 0;


    void Update()
    {
        SpawnBlock();
    }

    private void Start()
    {

    }


    private void SpawnBlock()
    {

        m_Currtime += Time.deltaTime;

        if (m_CanSpawn == true)
        {
            if(m_Currtime > m_SpawnTime)
            {
                for (int i = 0; i < m_BlockerPoints.Length; i++)
                {
                    
                    CObjectPool.g_instance.ObjectPop("BoomBoom", new Vector3(m_BlockerPoints[i].position.x,
                        m_BlockerPoints[i].position.y, m_BlockerPoints[i].position.z), Quaternion.identity, new Vector3(1.0f, 1.0f, 1.0f));
                    CObjectPool.g_instance.ObjectPush();
                }             
                

                m_Currtime = 0;
            }

        }
    }
}
