using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerPatformController : CControllerBase
{
    private bool m_IsGrounded = false;
    [SerializeField] private GameObject m_Platform;
    [SerializeField] private CColliderChecker m_ColliderChecker;
    private Vector3 m_PlatformPosition;
    private Vector3 m_Distance;
    [SerializeField] private GameObject Movement;

    private void Start()
    {
        m_Platform = GameObject.FindWithTag("Platform");
        Movement = GameObject.Find("Movement");
        m_ColliderChecker.m_ColliderStay += OnCollisionStay;
    }

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
    }

    void FixedUpdate()
    {
        MoveingPlatform();
    }

    private void OnCollisionStay(Collision Col)
    {
        if (Col.gameObject.tag == "Platform")
        {
            m_Platform = Col.gameObject;
            m_PlatformPosition = m_Platform.transform.position;

            m_Distance = m_PlatformPosition - m_Actor.transform.position;       //위치 저장

            m_IsGrounded = true;
        }
    }

    private void MoveingPlatform()
    {
        if (m_Actor == null) return;
        if (m_Platform != null)
        {
            if (m_IsGrounded == true
                && Movement.GetComponent<CPlayerMovement>().g_currentSpeed == 0
                && !m_Actor.CompareController("Jump")
                && !m_Actor.CompareController("Dash")
                && !m_Actor.CompareController("WallJump"))
            {
                //m_Actor.transform.position = m_Platform.transform.position - m_Distance;
                m_Actor.transform.position = new Vector3(m_Platform.transform.position.x - m_Distance.x,
                    m_Actor.transform.position.y, m_Platform.transform.position.z - m_Distance.z);
                //캐릭터 위치 =  밟고 있는 플랫폼 ~ distance 만큼 떨어진 위치 Character position = the platform you are stepping on ~ the distance you are away from
            }
            else
            {
                m_Platform = null;
                return;
            }


        }
    }
}
