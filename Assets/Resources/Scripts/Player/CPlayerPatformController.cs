using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerPatformController : CControllerBase
{
    private bool m_IsGrounded = false;
    private GameObject m_Platform;
    [SerializeField] private CColliderChecker m_ColliderChecker;
    private Vector3 m_PlatformPosition;
    private Vector3 m_Distance;
    private GameObject Movement;

    private void Start()
    {
        m_Platform = GameObject.FindWithTag("Platform");
        Movement = GameObject.Find("Movement");
        m_ColliderChecker.m_ColliderStay += ColliderStay;
    }

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
    }

    void FixedUpdate()
    {
        MoveingPlatform();
    }

    private void ColliderStay(Collision Col)
    {
        if (Col.gameObject.tag == "Platform")
        {
            m_Platform = Col.gameObject;                //접촉 -> 오브젝트 위치 저장  Contact -> Save object position
            m_PlatformPosition = m_Platform.transform.position;

            //접촉한 순간의 오브젝트 위치 - 캐릭터 위치를distance에 저장 Object position at the moment of contact ~ save character position to distance
            m_Distance = m_PlatformPosition - m_Actor.transform.position;

            m_IsGrounded = true;

        }
        else m_IsGrounded = false;
    }



    private void MoveingPlatform()
    {
        if (m_Actor == null) return;
        if (m_Platform != null)             
            // The player is on the floor and dosent move.   플레이어가 바닥에 있으며 움직이지 않는다. 
        {
            if (m_IsGrounded == true
                && Movement.GetComponent<CPlayerMovement>().g_currentSpeed == 0
                && !m_Actor.CompareController("Jump"))
            {

                m_Actor.transform.position = m_Platform.transform.position - m_Distance;
                //캐릭터 위치 =  밟고 있는 플랫폼 ~ distance 만큼 떨어진 위치 Character position = the platform you are stepping on ~ the distance you are away from
            }
        }
    }
}
