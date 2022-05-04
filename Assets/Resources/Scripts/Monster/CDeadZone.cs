using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CDeadZone : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_nav;            //����
    [SerializeField] GameObject MonSmash;                    //���� �����ϴ� �κ�


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

        //�Ŀ� �̺κ� ��Ȱ��ȭ�� �ƴ� �÷��̾� ��� ��� �� ����.
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
    }
}
