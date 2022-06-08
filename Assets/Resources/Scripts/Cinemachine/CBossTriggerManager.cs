using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossTriggerManager : MonoBehaviour
{
    [Header("보스 플레이어 킬 트리거 메니저")]
    //플레이어 즉사시킬 트리거 생성에 딜레이를 주는 파트

    [SerializeField] private float m_WaitTime = 4.0f;
    [SerializeField] private GameObject Col;

    void Start()
    {
        StartCoroutine(DelayCollider(m_WaitTime));
    }


    IEnumerator DelayCollider(float wait)
    {
        yield return new WaitForSeconds(wait);
        Col.SetActive(true);
    }

}
