using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CRock : CStaticObject
{
    //점점 줄어들 원 오브젝트
    [SerializeField]  private GameObject m_DamageZone;

    //떨어뜨릴 오브젝트
    [SerializeField] private GameObject m_Rock;

    //줄어드는 시간
    [SerializeField]  private float m_timer;


    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;

    //플레이어가 타이머가 끝날때 까지 해당 위치에 서있는지 체크
    private bool isCheck = false;

    protected override void Start()
    {
        base.Start();
        m_Rock.gameObject.SetActive(false);
        m_Buffinfo.g_Value_Vector3.Add(transform.position);
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            //변경할 크기 값      size value to change
            var scaleTo = new Vector3(1.0f, 1.0f, 2.5f);
            isCheck = true;
            //게임 오브젝트 , 변경할 크기 값, 소요시간  GameObject , size value to change, time
            StartCoroutine(DangerZoneChecker(m_DamageZone, m_Rock,  scaleTo, m_timer));
        }

    }

    private void OnTriggerExit(Collider col)
    {
        isCheck = false;
    }


    IEnumerator DangerZoneChecker(GameObject objectToScale, GameObject Rock, Vector3 scaleTo, float seconds)
        //원의 크기를 seconds 시간만큼 점점 줄이는 코루틴
    {
        m_Rock.gameObject.SetActive(true);
        float elapsedTime = 0;
        Vector3 startingScale = objectToScale.transform.localScale;
        Rock.transform.position = new Vector3(gameObject.gameObject.transform.position.x, seconds +1 , 0);
        while (elapsedTime < seconds)
        {
            Rock.transform.Translate(Vector3.down * Time.deltaTime);
            objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToScale.transform.position = scaleTo;



        if (isCheck == true)
        {
            var actor = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CDynamicObject>();
            gameObject.SetActive(false);
            actor.GenerateBuff("KnockBack", m_Buffinfo);

        }
        else
        {
            gameObject.SetActive(false);
        }
      
    }




    //protected static Vector3 RockFalling(Vector3 start, Vector3 end, float height, float t)
    //{
    //    Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

    //    var mid = Vector3.Lerp(start, end, t);

    //    return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    //    //x의 이동(직선) , y 이동을 이차곡선 형태로 형성     x movement (straight line) , y movement is formed in the form of a quadratic curve
    //}

    //protected IEnumerator BulletMove()
    //{
    //    timer = 0;
    //    while (transform.position.y >= startPos.y)
    //    {
    //        timer += Time.deltaTime;
    //        Vector3 tempPos = RockFalling(startPos, endPos, 5, timer);
    //        transform.position = tempPos;
    //        yield return new WaitForEndOfFrame();
    //    }
    //}

}
