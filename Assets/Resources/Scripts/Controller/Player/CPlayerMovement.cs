using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMovement : CControllerBase
{
        
    [SerializeField] private float m_fSpeed;
    [SerializeField , Range(0.1f ,1.0f)] private float m_turnSpeed;
    [SerializeField] private CColliderChecker m_checker;    
    [SerializeField] private float m_DecreaseSpeed;
    [SerializeField] private float m_InCreaseSpeed;
    [SerializeField, Header("치즈안에 있을 경우")] private float m_InCheeseSpeed;

    private float m_currentSpeed;
    
    private float m_DirX , m_DirZ;
    private Vector3 m_Dir = Vector3.zero;

    [SerializeField] private CColliderChecker m_Checker;
    [SerializeField] private bool m_InCheese = false;
    [SerializeField] private KeyCode m_DigginginKey;
    [SerializeField] private bool m_DigginginCheck;
    public float g_currentSpeed => m_currentSpeed;
    public bool g_InCheese => m_InCheese;

    private void FixedUpdate()
    {        
        Movement();        
    }

    private void OnEnable()
    {
        m_Checker.m_ColliderStay += ColliderStay;
        m_Checker.m_TriggerStay += TriggerStay;
        m_Checker.m_TriggerExit += TriggerExit;
    }

    private void OnDisable()
    {
        m_currentSpeed = 0.0f;
        m_Actor.g_Animator.SetFloat("Walking", 0.0f);

        m_Checker.m_ColliderStay -= ColliderStay;
        m_Checker.m_TriggerStay -= TriggerStay;
        m_Checker.m_TriggerExit -= TriggerExit;
    }

    private void Update()
    {
        m_DigginginCheck = false;
        m_DirX = Input.GetAxisRaw("Horizontal");
        m_DirZ = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(m_DigginginKey))
        {
            m_DigginginCheck = true;
        }
        
        
    }

    // 달리는 함수입니다.
    public void Movement()
    {
        Running();

        if (m_DirX == 0.0f && m_DirZ == 0.0f)
        {
            if (m_currentSpeed == 0.0f)
            {
                m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / m_fSpeed));                
                return;
            }
        }
        else
        {//* Mathf.Abs(m_DirX)
            var Dir = new Vector3(m_DirX, 0.0f , m_DirZ);
            m_Dir = Dir.normalized;
        }
        
        
       TurnRot();
       if (m_Actor.CompareBuff("KnockBack")) return;

        // 3d 게임이지만 게임상 2d로 움직이기 때문에 x값만 사용                     

        if (!m_Actor.CompareController("Dash"))
        {

            if (!m_InCheese)
            {
                m_Actor.g_Rigid.position = m_Actor.transform.position + m_Dir * m_currentSpeed * Time.fixedDeltaTime;
                m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / m_fSpeed));
            }
            else if (m_DigginginCheck)
            {
                m_Actor.g_Rigid.position = m_Actor.transform.position + m_Dir * m_currentSpeed * Time.fixedDeltaTime;
                m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / m_fSpeed));

            }
            else
            {
                m_Actor.g_Animator.SetFloat("Walking", 0.0f);
            }

        }
    }

    // y축 angle을 변경하는 함수 입니다.
    private void TurnRot()
    {
        if (m_DirX != 0.0f || m_DirZ != 0.0f)
        {
            var transEulerRot = m_Actor.transform.rotation;
            var ResultRot = Quaternion.LookRotation(m_Dir);
            m_Actor.transform.rotation = Quaternion.Lerp(transEulerRot , ResultRot ,m_turnSpeed);
        }       
    } 
   
    
    //달리는 함수 입니다. 제거해야됨
    private void Running()
    {
        var resultSpeed = 0.0f;
       
        if (m_DirZ != 0.0f || m_DirX != 0.0f)
            resultSpeed += m_fSpeed;

        if (m_InCheese)
            resultSpeed *= m_InCheeseSpeed;

        if (m_currentSpeed < resultSpeed)
        {
            m_currentSpeed += resultSpeed * Time.fixedDeltaTime * m_InCreaseSpeed;
        }
        if (m_currentSpeed >= resultSpeed)
        {
            if (m_DecreaseSpeed == 0.0f) m_DecreaseSpeed = 1.0f;            

            m_currentSpeed -= Time.fixedDeltaTime * m_DecreaseSpeed;
            
            if (m_currentSpeed <= resultSpeed)
                m_currentSpeed = resultSpeed;           
        }
    }
    
    private void TriggerStay(Collider collider)
    {
        if (collider.transform.CompareTag("Cheese"))
        {
            m_InCheese = true;
            collider.isTrigger = true;
        }
    }

    private void TriggerExit(Collider collider)
    {
        if (collider.transform.CompareTag("Cheese"))
        {
            m_InCheese = false;
            collider.isTrigger = false;
        }
    }

    private void ColliderStay(Collision collisiton)
    {
        if (collisiton.transform.CompareTag("Cheese") && m_DigginginCheck)
        {           
            collisiton.collider.isTrigger = true;
        }
    }
}
