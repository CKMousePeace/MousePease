using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//해당 스크립트는 오브젝트풀이 적용되어있지 않음을 알림. (구 버전 코드, 신버전 ->DangerZone ~~ ForArtillery

public class CDangerZone : MonoBehaviour
{
    //점점 줄어들 원 오브젝트
    [SerializeField]  private GameObject m_DamageZone;

    //떨어질 구역을 표시할 오브젝트
    [SerializeField] private GameObject m_Indicator;

    //떨어뜨릴 오브젝트
    [SerializeField] private GameObject m_Cheese;

    //줄어드는 시간
    [SerializeField]  private float m_timer;

    [Header("돌이 떨어질 높이")]
    [SerializeField] protected float m_Rockhigh = 0;


    //플레이어 위치에 서있는지 확인
    private bool m_Checker = false;

    private void OnTriggerEnter(Collider col)
    {
        m_Checker = true;
        if (col.CompareTag("Player"))
        {
            //변경할 크기 값      size value to change
            var scaleTo = new Vector3(1.0f, 1.0f, 2.5f);

            //게임 오브젝트 , 변경할 크기 값, 소요시간  GameObject , size value to change, time
            StartCoroutine(DangerZoneChecker(m_DamageZone, m_Cheese,  scaleTo, m_timer));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_Checker = false;
    }

    IEnumerator DangerZoneChecker(GameObject objectToScale, GameObject Rock, Vector3 scaleTo, float seconds)
        //원의 크기를 seconds 시간만큼 점점 줄이는 코루틴
    {
        float elapsedTime = 0;
        Vector3 startingScale = objectToScale.transform.localScale;
        Rock.transform.position = new Vector3(gameObject.transform.position.x, m_Rockhigh, gameObject.transform.position.z);
        while (elapsedTime < seconds)
        {
            objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToScale.transform.position = scaleTo;

        if(m_Checker == true)
        {
            m_DamageZone.SetActive(false);
            m_Cheese.SetActive(true);
                
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
