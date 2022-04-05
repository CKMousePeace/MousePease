using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CRock : CStaticObject
{

   //[SerializeField]  private Vector3 startPos, endPos;
   //���� �ݱ���� �ɸ��� �ð�

    //�پ��� �ð�
    [SerializeField] protected float m_timer;


    //[SerializeField] protected float Size;

    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;


    private bool isCheck = false;

    protected override void Start()
    {
        base.Start();
        m_Buffinfo.g_Value_Vector3.Add(transform.position);


    }

    private void Update()
    {


    }




    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            //������ ũ�� ��      size value to change
            var scaleTo = new Vector3(0.0f, 0.0f, 0.1f);

            //���� ������Ʈ , ������ ũ�� ��, �ҿ�ð�  GameObject , size value to change, time
            StartCoroutine(DangerZoneChecker(gameObject, scaleTo, m_timer));


        }

    }


    IEnumerator DangerZoneChecker(GameObject objectToScale, Vector3 scaleTo, float seconds)
    {
        var actor = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CDynamicObject>();

        float elapsedTime = 0;
        Vector3 startingScale = objectToScale.transform.localScale;
        while (elapsedTime < seconds)
        {
            objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToScale.transform.position = scaleTo;
        isCheck = true;


        if (isCheck == true)
        {
            actor.GenerateBuff("KnockBack", m_Buffinfo);
            gameObject.SetActive(false);
        }
        
    }



    //protected static Vector3 RockFalling(Vector3 start, Vector3 end, float height, float t)
    //{
    //    Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

    //    var mid = Vector3.Lerp(start, end, t);

    //    return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    //    //x�� �̵�(����) , y �̵��� ����� ���·� ����     x movement (straight line) , y movement is formed in the form of a quadratic curve
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
