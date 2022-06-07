using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollinSpawn : MonoBehaviour
{ 
    [SerializeField] private Transform m_RollingSpawnLocation;
    [SerializeField] private float m_WaitTime = 0;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(SpawnRolling());
        }
    }


    IEnumerator SpawnRolling()
    {

        yield return new WaitForSeconds(m_WaitTime);

       CObjectPool.g_instance.ObjectPop("RollingBoi", new Vector3(m_RollingSpawnLocation.position.x,
                        m_RollingSpawnLocation.position.y, m_RollingSpawnLocation.position.z), Quaternion.Euler(0, 0, 0), new Vector3(1.0f, 1.0f, 1.0f));
        //CObjectPool.g_instance.ObjectPush();

        gameObject.SetActive(false);
    }

}
