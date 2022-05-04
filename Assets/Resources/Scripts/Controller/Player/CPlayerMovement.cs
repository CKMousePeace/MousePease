using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMovement : CControllerBase
{
        
    [SerializeField] private float m_fMaxSpeed;
    [SerializeField , Range(0.1f ,1.0f)] private float m_turnSpeed;
    //[SerializeField] private float m_DecreaseSpeed = 0.0f;
    //[SerializeField] private float m_InCreaseSpeed = 0.0f;
    [SerializeField] private float m_MeltedSpeedTime;

    private float m_currentSpeed;    
    private float m_DirX;
    private float m_Yaw = 90.0f;
       


    [SerializeField] private CColliderChecker m_Checker;    
    [SerializeField] private KeyCode m_DigginginKey;        
    [SerializeField] private Vector2 m_ChaseTimeRange;
    

    public float g_currentSpeed => m_currentSpeed;
    public KeyCode g_DigginginKey => m_DigginginKey;
    

    

    private void OnEnable()
    {            
    }

    private void OnDisable()
    {
        m_currentSpeed = 0.0f;             
        m_Actor.g_Animator.SetFloat("Walking", 0.0f);
    }

    private void Update()
    {
        PlayerMoveKey();
        m_Actor.g_Animator.SetFloat("Walking", m_currentSpeed / m_fMaxSpeed);
    }
    private void FixedUpdate()
    {
        if (PlayerMoveState()) return;
        Movement();
        
    }

    private void PlayerMoveKey()
    {
        m_DirX = Input.GetAxisRaw("Horizontal");        
    }



    // �޸��� �Լ��Դϴ�.
    private void Movement()
    {
        PlayerMove();
        TurnRot();
    }
    
    //���������� �÷��̾� �����̴� �Լ�
    private void PlayerMove()
    {
        var AbsDir = Mathf.Abs(m_DirX);
        var Dir = new Vector3(m_Actor.transform.forward.x * AbsDir, 0.0f, 0.0f);

        

        if (m_DirX == 0.0f)
        {
            m_currentSpeed = 0.0f;
            return;
        }
        m_currentSpeed = m_fMaxSpeed;
        var Displacement = Dir * m_fMaxSpeed * Time.fixedDeltaTime;               
        m_Actor.g_Rigid.MovePosition(m_Actor.g_Rigid.position + Displacement);

    }

    // y�� angle�� �����ϴ� �Լ� �Դϴ�.
    private void TurnRot()
    {
        var PlayerEulerAngles = m_Actor.transform.eulerAngles;
        float CurrentAngle = Mathf.LerpAngle(PlayerEulerAngles.y, m_Yaw, m_turnSpeed);
        m_Actor.transform.eulerAngles = new Vector3(PlayerEulerAngles.x, CurrentAngle, PlayerEulerAngles.z);

        if (m_DirX == 0.0f){
            
            return;
        }
        m_Yaw = m_DirX * 90.0f;
    }

    

    private bool PlayerMoveState()
    {
        if (m_Actor.CompareBuff("KnockBack"))
        {
            return true;
        }
        if (m_Actor.CompareController("Dash"))
        {
            return true;
        }
        return false;
    }


    /// <summary>
    ///ġ�� ���� �κ� 
    /// </summary>


    private void InCheeseInit()
    {
        
        StopCoroutine("ExitCheese");
    }
    private void ExitCheese()
    {
        
        m_Actor.g_Rigid.velocity = Vector3.zero;
        StartCoroutine(ExitCheeseCoroutine());
    }

    // ġ�� ��ó��
    private IEnumerator ExitCheeseCoroutine()
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
}
