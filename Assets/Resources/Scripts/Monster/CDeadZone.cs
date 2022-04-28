using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CDeadZone : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_nav;            //보스
    [SerializeField] GameObject MonSmash;                    //몬스터 점프하는 부분


    private void Start()
    {
        m_nav = GameObject.Find("Boss").GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            m_nav.velocity = Vector3.zero;
            MonSmash.SetActive(true);        //Boss Kill Motion Enable
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.0f);

        //후에 이부분 비활성화가 아닌 플레이어 사망 모션 및 정지.
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
    }
}
