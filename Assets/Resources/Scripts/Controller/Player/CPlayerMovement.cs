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



    [SerializeField, Header("녹은 치즈 감소 스피드")] private float m_InMeltedDecreaseSpeed;
    [SerializeField] private float m_MeltedSpeedTime;

    private float m_currentSpeed;
    private float m_currentChaseTime;
    private float m_DirX , m_DirZ;
    private float m_CurrentInMeltedDecreaseSpeed;


    private Vector3 m_Dir = Vector3.zero;

    [SerializeField] private CColliderChecker m_Checker;
    [SerializeField] private bool m_InCheese = false;
    [SerializeField] private KeyCode m_DigginginKey;
    [SerializeField] private bool m_DigginginCheck;
    

    [SerializeField] private Vector2 m_ChaseTimeRange;
    public float g_currentSpeed => m_currentSpeed;
    public bool g_InCheese => m_InCheese;
        
    [SerializeField] private float m_ChaseTime;

    private void FixedUpdate()
    {        
        Movement();        
    }

    private void OnEnable()
    {
        m_Checker.m_ColliderStay += ColliderStay;
        m_Checker.m_TriggerStay += TriggerStay;
        m_Checker.m_TriggerExit += TriggerExit;

        m_ChaseTime = Random.Range(m_ChaseTimeRange.x , m_ChaseTimeRange.y);
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
        else
        {
            m_DigginginCheck = false;
        }
        if (!m_Actor.CompareController("Jump") && !m_Actor.CompareController("Dash"))
        {
            if (m_currentSpeed != 0)
                m_currentChaseTime += Time.deltaTime;
        }
        if (m_Actor.CompareController("Jump")  || m_Actor.CompareController("Dash"))
        {
            m_currentChaseTime = 0.0f;
        }
        if (!m_Actor.CompareController("Dash"))
            SetGravity();


    }

    // 달리는 함수입니다.
    private void Movement()
    {
        Walking();

        if (m_DirX == 0.0f && m_DirZ == 0.0f)
        {
            if (m_currentSpeed == 0.0f)
            {
                m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / m_fSpeed));                
                return;
            }
        }
        else
        {
            var Dir = Vector3.zero;
            if (!m_InCheese)
            {
                Dir = new Vector3(m_DirX, 0.0f, m_DirZ);
             
            }
            else
            {
                Dir = new Vector3(m_DirX, m_DirZ, 0.0f);                
            }
            m_Dir = Dir.normalized;
        }        
        
       TurnRot();
       if (m_Actor.CompareBuff("KnockBack")) return;

        
        if (!m_Actor.CompareController("Dash"))
        {
            PlayerMovement();
        }
    }

  
    //실질적으로 플레이어 움직이는 함수
    private void PlayerMovement()
    {
        m_Actor.g_Rigid.position = m_Actor.transform.position + m_Dir * m_currentSpeed * Time.fixedDeltaTime;
        if (m_currentChaseTime >= m_ChaseTime)
        {
            m_ChaseTime = Random.Range(m_ChaseTimeRange.x, m_ChaseTimeRange.y);
            m_currentChaseTime = 0.0f;

            if (!m_Actor.CompareController("Jump") && !m_Actor.CompareController("Dash"))
            {
                m_Actor.g_Animator.SetTrigger("Chase");
                m_Actor.g_Animator.SetLayerWeight(1, m_currentSpeed / m_fSpeed);
            }
        }
        else
            m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / m_fSpeed));
    }






    // y축 angle을 변경하는 함수 입니다.
    private void TurnRot()
    {
        if (m_DirX != 0.0f || m_DirZ != 0.0f)
        {
            var transEulerRot = m_Actor.transform.rotation;
            var ResultRot = Quaternion.LookRotation(m_Dir);

            if (m_InCheese)
            {
                if (m_Dir.y == 1.0f)
                {
                    ResultRot.eulerAngles = new Vector3(ResultRot.eulerAngles.x, 90.0f, ResultRot.eulerAngles.z);
                }
                else if (m_Dir.y == -1.0f)
                {
                    ResultRot.eulerAngles = new Vector3(ResultRot.eulerAngles.x, -90.0f, ResultRot.eulerAngles.z);
                }

            }                      

            m_Actor.transform.rotation = Quaternion.Lerp(transEulerRot, ResultRot, m_turnSpeed);
        }
    }


    
    //달리는 함수 입니다. 제거해야됨
    private void Walking()
    {
        var resultSpeed = 0.0f;
       
        if (m_DirZ != 0.0f || m_DirX != 0.0f)
            resultSpeed += m_fSpeed;

        if (m_InCheese)
            resultSpeed *= m_InCheeseSpeed;

        resultSpeed -= m_CurrentInMeltedDecreaseSpeed;

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

    private void SetGravity()
    {
        if (!m_Actor.g_Rigid.useGravity && !m_InCheese)
            m_Actor.g_Rigid.useGravity = true;
        if (m_Actor.g_Rigid.useGravity && m_InCheese)
            m_Actor.g_Rigid.useGravity = false;
    }
    

    //치즈 안에 들어가는 함수 입니다.


    private IEnumerator ExitCheese()
    {
        var time = 0.0f;
        var transEulerRot = m_Actor.transform.rotation;
        var ResultRot = Quaternion.Euler(new Vector3(0.0f, transEulerRot.eulerAngles.y, transEulerRot.eulerAngles.z));

        yield return new WaitUntil(() => {
            time += Time.deltaTime * 3.0f;
            if (time <= 1.0f)
            {
                m_Actor.transform.rotation = Quaternion.Lerp(transEulerRot, ResultRot, time);
                return false;
            }
            return true;
        });
    }



    private void TriggerStay(Collider collider)
    {
        if (collider.transform.CompareTag("Cheese"))
        {
            m_InCheese = true;
            collider.isTrigger = true;
        }
        //녹은 치즈
        else if (collider.transform.CompareTag("MeltedCheese"))
        {
            if (m_currentSpeed - m_InMeltedDecreaseSpeed <= 0.0f)
            {
                m_CurrentInMeltedDecreaseSpeed = m_currentSpeed;
            }
            else
            {
                m_CurrentInMeltedDecreaseSpeed = m_InMeltedDecreaseSpeed;
            }            
        }
    }


    private void TriggerExit(Collider collider)
    {
        if (collider.transform.CompareTag("Cheese"))
        {
            m_InCheese = false;
            collider.isTrigger = false;
            m_Actor.g_Rigid.velocity = Vector3.zero;
            StartCoroutine(ExitCheese());
        }
        //녹은 치즈
        else if (collider.transform.CompareTag("MeltedCheese"))
        {
            StartCoroutine(ExitMelted());
        }
    }

    private void ColliderStay(Collision collisiton)
    {
        if (collisiton.transform.CompareTag("Cheese") && m_DigginginCheck )
        {           
            collisiton.collider.isTrigger = true;
            StopCoroutine("ExitCheese");
        }
    }


    private IEnumerator ExitMelted()
    {
        float time = 0.0f;
        yield return new WaitUntil(() =>
        {
            time += Time.deltaTime;

            if (m_currentSpeed - m_InMeltedDecreaseSpeed <= 0.0f)
            {
                m_CurrentInMeltedDecreaseSpeed = m_currentSpeed;
            }

            if (time <= m_MeltedSpeedTime)
            {
                return false;
            }
            m_CurrentInMeltedDecreaseSpeed = 0.0f;
            return true;

        });
    }

}
