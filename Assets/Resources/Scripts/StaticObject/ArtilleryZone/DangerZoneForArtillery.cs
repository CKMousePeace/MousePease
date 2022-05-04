using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//아 졸려

public class DangerZoneForArtillery : MonoBehaviour
{

    //점점 줄어들 원 오브젝트
    [SerializeField] private GameObject m_DamageZone;
    [Header("줄어드는 시간")]
    [SerializeField] private float m_timer = 0;

    [Header("돌이 떨어질 높이")]
    [SerializeField] private float m_Rockhigh = 0;

    private void Start()
    {
        //변경할 크기 값      size value to change
        var scaleTo = new Vector3(1.0f, 1.0f, 2.5f);
        //게임 오브젝트 , 변경할 크기 값, 소요시간  GameObject , size value to change, time
        StartCoroutine(DangerZoneChecker(m_DamageZone, scaleTo, m_timer));

    }

    IEnumerator DangerZoneChecker(GameObject objectToScale , Vector3 scaleTo, float seconds)
        //원의 크기를 seconds 시간만큼 점점 줄이는 코루틴
    {
        float elapsedTime = 0;
        Vector3 startingScale = objectToScale.transform.localScale;
        while (elapsedTime < seconds)
        {
            objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToScale.transform.position = scaleTo;

        if(elapsedTime > seconds)
        {

            //오브젝트 풀에서 Rock 받아옴
            CObjectPool.g_instance.ObjectPop("Rock", new Vector3(gameObject.transform.position.x, m_Rockhigh, gameObject.transform.position.z),
            Quaternion.Euler(-90, 0, 0), new Vector3(1, 1, 1));

            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
