using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDash : CControllerBase
{
    [SerializeField] private float m_DashForce;
    [SerializeField] private float m_DashTime;   
    [SerializeField] private CColliderChecker m_ColliderChecker;
    [SerializeField] private float m_DelayTime;

    private Vector3 m_Dir;    
    private float m_CurrentTime;
    

    public override void init(CDynamicObject actor)
    {

        // ���� ���۽� ������Ʈ�� ���ݴϴ�.
        m_DelayTime = 0.0f;
        gameObject.SetActive(false);
        base.init(actor);
        
    }
    private void OnDisable()
    {       
        //���� m_Actor�� ���� ��� ������ ���ݴϴ�.
        if (m_Actor == null) return;
        m_Actor.g_Rigid.useGravity = true;        
    }

    private void OnEnable()
    {
        float m_DirX = Input.GetAxisRaw("Horizontal");

        //m_DirX == 0�� ��� �������� ���� �����̱� ������ return�� ���ݴϴ�. 
        // �Ǵ� �˹������ ���� ��� ���� 
        if (m_DirX == 0 || m_Actor.CompareBuff("KnockBack") || (m_DelayTime != 0 && Time.time - m_DelayTime <= 1.0f))
        {
            gameObject.SetActive(false);
            return;
        }
        m_DelayTime = Time.time;
        m_Dir = new Vector3(m_DirX, 0.0f, 0.0f);
        m_Dir.Normalize();
        m_CurrentTime = 0.0f;
        m_Actor.g_Rigid.useGravity = false;
        m_Actor.g_Rigid.velocity = Vector3.zero;
     
    }

    private void Update()
    {
        Dash();
    }

   
    private void Dash()
    {       

        // �ð� üũ
        float DashTime = Time.deltaTime / m_DashTime;        
        m_CurrentTime += DashTime;
        var MoveData = m_Dir * m_DashForce * DashTime;
                

        // ĸ�� �ɽ�Ʈ �� �̿��� �浹 üũ
        var capsuleCollider = (CapsuleCollider)m_ColliderChecker.g_Collider;

        var p1 = m_Actor.transform.position + capsuleCollider.center + (m_Actor.transform.up * capsuleCollider.height * 0.5f);
        var p2 = m_Actor.transform.position + capsuleCollider.center - (m_Actor.transform.up * capsuleCollider.height * 0.5f);


        // ĸ�� �ɽ�Ʈ ���
        RaycastHit hit;
        if (Physics.CapsuleCast(p1, p2 , capsuleCollider.radius , m_Actor.transform.forward, out hit, 0.3f))
        {
            // �浹 ��ġ���� m_Actor.transform.forward * 0.1f��ŭ �ڷ� �̵�
            m_Actor.transform.position = hit.point - m_Actor.transform.forward * 0.1f;
            gameObject.SetActive(false);
            return;
        }


        if (m_CurrentTime >= 1.0f)
            gameObject.SetActive(false);

        m_Actor.transform.position += MoveData;        
    }


    //����� �̿��� �浹üũ
    //private void OnDrawGizmosSelected()
    //{
    //
    //    var capsuleCollider = (CapsuleCollider)m_ColliderChecker.g_Collider;
    //    if (capsuleCollider == null || m_Actor == null)
    //    {
    //        return;
    //    }
    //
    //    var p1 = m_Actor.transform.position + capsuleCollider.center + (m_Actor.transform.up * capsuleCollider.height * 0.5f);
    //    var p2 = m_Actor.transform.position + capsuleCollider.center - (m_Actor.transform.up * capsuleCollider.height * 0.5f);
    //
    //
    //    Gizmos.DrawSphere(p1, capsuleCollider.radius);
    //    Gizmos.DrawSphere(p2, capsuleCollider.radius);
    //
    //}

}
