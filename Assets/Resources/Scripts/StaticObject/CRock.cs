using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CRock : CStaticObject
{

    [SerializeField]  private Vector3 startPos, endPos;
    //땅에 닫기까지 걸리는 시간
    [SerializeField] protected float timer;

    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;

    protected override void Start()
    {
        base.Start();
        m_Buffinfo.g_Value_Vector3.Add(transform.position);

    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine("Start");
        }
    }

    IEnumerator EStart()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(5, 0, 0);
        StartCoroutine("BulletMove");

        yield return new WaitForEndOfFrame();
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    var actor = other.gameObject.GetComponent<CDynamicObject>();
    //    if (actor == null) return;
    //    // 맞은 오브젝트에게 상태를 전달 해줍니다.
    //    actor.GenerateBuff("KnockBack", m_Buffinfo);
    //}


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            Debug.Log("충돌");

        }

    }



    protected static Vector3 RockFalling(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
        //x의 이동(직선) , y 이동을 이차곡선 형태로 형성     x movement (straight line) , y movement is formed in the form of a quadratic curve
    }

    protected IEnumerator BulletMove()
    {
        timer = 0;
        while (transform.position.y >= startPos.y)
        {
            timer += Time.deltaTime;
            Vector3 tempPos = RockFalling(startPos, endPos, 5, timer);
            transform.position = tempPos;
            yield return new WaitForEndOfFrame();
        }
    }

}
