using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossTriggerManager : MonoBehaviour
{

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
