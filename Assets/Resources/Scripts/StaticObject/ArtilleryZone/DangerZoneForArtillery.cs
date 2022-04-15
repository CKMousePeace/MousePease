using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//아 졸려

public class DangerZoneForArtillery : CArtilleryZone
{

    //점점 줄어들 원 오브젝트
    [SerializeField] protected GameObject m_DamageZone;

    //떨어질 구역을 표시할 오브젝트
    [SerializeField] protected GameObject m_Indicator;

    //떨어뜨릴 오브젝트
    [SerializeField] protected GameObject m_Rock;

    private void Start()
    {
        //변경할 크기 값      size value to change
        var scaleTo = new Vector3(1.0f, 1.0f, 2.5f);
        //게임 오브젝트 , 변경할 크기 값, 소요시간  GameObject , size value to change, time
        StartCoroutine(DangerZoneChecker(m_DamageZone, m_Rock, scaleTo, m_timer));

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

        if(elapsedTime > seconds)
        {
            m_DamageZone.SetActive(false);
            m_Rock.SetActive(true);   
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
