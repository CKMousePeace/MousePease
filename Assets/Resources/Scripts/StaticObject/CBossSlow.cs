using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CBossSlow : MonoBehaviour
{

    [SerializeField] private NavMeshAgent m_NavBoss;
    [SerializeField] private float m_BossWaitTime = 0.0f;


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("º¸½º Æø·Â ¸ØÃç!!");
            StartCoroutine(OnStopBoss(m_BossWaitTime));
        }
    }


    private IEnumerator OnStopBoss(float WaitTIme)
    {
        m_NavBoss.speed = 0;
        m_NavBoss.velocity = Vector3.zero;

        yield return new WaitForSeconds(WaitTIme);

        m_NavBoss.speed = 6;
        Destroy(gameObject);

        yield break;

    }


}
