using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDeadZone : CControllerBase
{
    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(true);
        base.init(actor);
    }


    private void OnTriggerEnter(Collider col)
    //When player touches, WayPoint script will change Boss speed to 0, play execution motion and delete player.
    //플레이어 닿으면 WayPoint 스크립트에서 boss 스피드 0 으로 바꾸고 처형 모션 재생 및 플레이어 삭제
    {
        if (col.CompareTag("Player"))
        {
            //CWayPoint cWayPoint = GameObject.Find("WayPointFollower").GetComponent<CWayPoint>();
            //cWayPoint.g_speed = 0;
            m_Actor.g_Animator.SetTrigger("AttackReady01");

            StartCoroutine(AttackDelay());
        }

    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(GameObject.FindGameObjectWithTag("Player"));

    }

}
