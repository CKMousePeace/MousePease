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
            m_Platform = Col.gameObject;                //���� -> ������Ʈ ��ġ ����  Contact -> Save object position
            m_PlatformPosition = m_Platform.transform.position;

            //������ ������ ������Ʈ ��ġ - ĳ���� ��ġ��distance�� ���� Object position at the moment of contact ~ save character position to distance
            m_Distance = m_PlatformPosition - m_Actor.transform.position;

            m_IsGrounded = true;

        }
        else m_IsGrounded = false;
    }



    private void MoveingPlatform()
    {
        if (m_Actor == null) return;
        if (m_Platform != null)             
            // The player is on the floor and dosent move.   �÷��̾ �ٴڿ� ������ �������� �ʴ´�. 
        {
            if (m_IsGrounded == true
                && Movement.GetComponent<CPlayerMovement>().g_currentSpeed == 0
                && !m_Actor.CompareController("Jump"))
            {

                m_Actor.transform.position = m_Platform.transform.position - m_Distance;
                //ĳ���� ��ġ =  ��� �ִ� �÷��� ~ distance ��ŭ ������ ��ġ Character position = the platform you are stepping on ~ the distance you are away from
            }
        }
    }
}
