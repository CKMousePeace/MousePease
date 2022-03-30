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
    //�÷��̾� ������ WayPoint ��ũ��Ʈ���� boss ���ǵ� 0 ���� �ٲٰ� ó�� ��� ��� �� �÷��̾� ����
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
