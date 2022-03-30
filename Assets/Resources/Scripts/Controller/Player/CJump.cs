using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJump : CControllerBase
{
    [SerializeField] private float m_fForce;
    [SerializeField] private CColliderChecker m_ColliderChecker;
    [SerializeField] private float m_JumpTime;

    private bool m_isDoubleJump = false;                //플랫폼에서 점프하는지 아닌지 확인하기에 public 으로 변경
    private bool m_isJump = false;       //체크 플랫폼에서 점프하는지 아닌지 확인하기에 public 으로 변경

    public bool g_isDoubleJump => m_isDoubleJump; //플랫폼에서 점프하는지 아닌지 확인하기에 public 으로 변경
    public bool g_isJump => m_isJump;       //체크 플랫폼에서 점프하는지 아닌지 확인하기에 public 으로 변경


    private float m_CurrentJumpTime;
    private float m_beforeJumpTime;
    
    public override void init(CDynamicObject actor)
    {
        // 시작하자 마자 오브젝트를 꺼줍니다.
        gameObject.SetActive(false);
        base.init(actor);
    }

    

    private void OnEnable()
    {
        if (m_Actor == null) return;
        if (m_Actor.CompareController("Dash"))
        {
            gameObject.SetActive(false);
            return;
        }
        m_Actor.g_Animator.SetTrigger("Jump");
        m_Actor.g_Animator.SetBool("isGround" , false);
        StartCoroutine(JumpStart());

    }

    private void OnDisable()
    {
        if (m_Actor == null) return;

        m_Actor.g_Animator.SetBool("isGround", true);        
        m_isJump = false;
        m_isDoubleJump = false;
        m_ColliderChecker.m_ColliderStay -= ColliderStay;
        m_ColliderChecker.m_TriggerEnter -= TriggerEnter;
    }

    private void Update()
    {        
        if (m_Actor.CompareController("Dash"))
        {
            gameObject.SetActive(false);
            return;
        }
        Jump();
        TriggerCheck();
    }


    private IEnumerator JumpStart()
    {
        yield return new WaitUntil(() => {

            if (m_Actor.g_Animator.GetCurrentAnimatorStateInfo(0).IsName("JumpStart") && m_Actor.g_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                m_isJump = true;
                m_ColliderChecker.g_Collider.isTrigger = true;
                m_CurrentJumpTime = 0.0f;
                m_beforeJumpTime = 0.0f;

                m_Actor.g_Rigid.AddForce(Vector3.up * 3.0f, ForceMode.Impulse);
                m_ColliderChecker.m_ColliderStay += ColliderStay;
                m_ColliderChecker.m_TriggerEnter += TriggerEnter;
                return true;
            }
            return false;   
        });

        


    }
    

    //아래에 오브젝트와 충돌 할 경우 오브젝트를 종료 합니다.
    private void ColliderStay(Collision collder)
    {
        if (!gameObject.activeInHierarchy) return;
        var extentsy = new Vector3(0.0f, m_ColliderChecker.g_Collider.bounds.extents.y, 0.0f);
        var extentsx = new Vector3(m_ColliderChecker.g_Collider.bounds.extents.x, 0.0f, 0.0f);

        Debug.DrawRay(m_Actor.transform.position - (extentsy * 0.9f), Vector3.down * 0.5f);
        Debug.DrawRay(m_Actor.transform.position - (extentsy * 0.9f + extentsx), Vector3.down * 0.5f); 
        Debug.DrawRay(m_Actor.transform.position - (extentsy * 0.9f - extentsx), Vector3.down * 0.5f);

        if (Physics.Raycast(m_Actor.transform.position - (extentsy * 0.9f), Vector3.down, 0.5f) || 
            Physics.Raycast(m_Actor.transform.position - (extentsy * 0.9f + extentsx), Vector3.down, 0.5f) ||
            Physics.Raycast(m_Actor.transform.position - (extentsy * 0.9f - extentsx), Vector3.down, 0.5f))
        {
            gameObject.SetActive(false);
        }

    }
    //Floor를 제외한 다른 오브젝트와 충돌을 할 경우 trigger를 제거 해줍니다.
    private void TriggerEnter(Collider other)
    {
        if (!other.CompareTag("Floor") && !other.CompareTag("FirstFloor"))
        {
            m_ColliderChecker.g_Collider.isTrigger = false;
        }
    }


    //점프 적용하는 함수 입니다.
    private void Jump()
    {
        if (Input.GetKey(m_Key) && m_CurrentJumpTime <= 1.0f && m_isJump)
        {
            m_CurrentJumpTime += Time.deltaTime / m_JumpTime;
            var deltaTime = m_CurrentJumpTime - m_beforeJumpTime;
            if (m_CurrentJumpTime >= 1.0f)
                deltaTime -= (m_CurrentJumpTime - 1.0f);


            m_Actor.g_Rigid.AddForce(Vector3.up * (m_fForce * deltaTime) * 3.0f, ForceMode.Impulse);
            m_beforeJumpTime = m_CurrentJumpTime;
        }
        else
        {           
            m_isJump = false;
            DoubleJump();
        }        
    }

    // 더블 점프를 사용 할 수 있는지 판별합니다.
    private void DoubleJump()
    {        
        if (Input.GetKeyDown(m_Key))
        {
            if (!m_isDoubleJump)
            {
                m_isDoubleJump = true;
                m_Actor.g_Rigid.AddForce(Vector3.up * (m_fForce) * 1.5f, ForceMode.Impulse);
            }
        }
    }

    // trigger을 킬지 말지 정하는 함수 입니다.
    private void TriggerCheck()
    {
        if ((!m_isJump) && m_ColliderChecker.g_Collider.isTrigger == true && m_Actor.g_Rigid.velocity.y <= -0.0f)
        {
            if (!m_Actor.CompareController("DownJump"))
                m_ColliderChecker.g_Collider.isTrigger = false;
        }
    }

}
