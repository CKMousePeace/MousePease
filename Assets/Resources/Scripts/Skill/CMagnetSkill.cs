using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class CMagnetSkill : CSkillBase
{
    [SerializeField] private Image Magnetimage;
    [SerializeField] private CMagnet.MagnetType m_magnetType = CMagnet.MagnetType.Normal;
    private CMagnetManager m_magnetManager;
    private Vector3 m_ForceDirection;
    private float m_MagnetCosData;
    CMagnet m_ChoiceMagnet = null;
    public override void init(CDynamicObject actor)
    {
        
        base.init(actor);
        Magnetimage.color = Color.gray;
        var manager = GameObject.Find("MagnetManager");
        if (manager != null)
            m_magnetManager = manager.GetComponent<CMagnetManager>();

        float m_MagnetHalfCin = 45.0f * 0.5f;
        m_MagnetCosData = Mathf.Cos(m_MagnetHalfCin * Mathf.Deg2Rad);


    }

    private void Update()
    {
        Pole();
        MagnetChange();        
    }


    // �ڼ� ���¸� �����ϴ� �Լ� �Դϴ�.
    private void MagnetChange()
    {

        if (Input.GetKeyDown(g_KeyCode))
        {
            //�ڼ� ȿ�� �ٲ� ���� �ʱ�ȭ
            m_magnetType++;
            m_ChoiceMagnet = null;
            m_ForceDirection = Vector3.zero;
            m_magnetType = (CMagnet.MagnetType)((int)m_magnetType % 2);
                       

            if (m_magnetType == CMagnet.MagnetType.Normal)
            {
                Magnetimage.color = Color.gray;
                m_Actor.g_Rigid.useGravity = true;
            }
            
            if (m_magnetType == CMagnet.MagnetType.NPole)             
                Magnetimage.color = Color.red;
            if (m_magnetType == CMagnet.MagnetType.SPole)
                Magnetimage.color = Color.blue;
        }
    }    


    //�ڼ� ȿ�� ����
    private void Pole()
    {
        if (m_magnetType == CMagnet.MagnetType.Normal)
        {
            // normal�϶� �뽬 knockback�� ���� ��� �߷°� Ȱ��ȭ
            if (!m_Actor.CompareController("Dash") || !m_Actor.CompareBuff("KnockBack"))
            {
                m_Actor.g_Rigid.useGravity = true;
            }
            return;
        }

        var MagnetList = MagnetCountCheck();
        var AnotherMagnet = MagnetList.Item1;
        var SameMagnet = MagnetList.Item2;



        Attraction(AnotherMagnet);
        Repulsion(SameMagnet);

    }

    //������ �ִ� �ڼ� �� ī��Ʈ �ϴ� �Լ�
    //ù ��° ���� ���� �ٸ� �ڼ��� ����
    //�� ��° ���� ���� ���� �ڼ��� ����
    private (List<CMagnet> , List<CMagnet>) MagnetCountCheck()
    {

        List<CMagnet> MagnetAnotherList = new List<CMagnet>();
        List<CMagnet> MagnetSameList = new List<CMagnet>();



        foreach (var magnet in m_magnetManager.g_magnet)
        {
            if (magnet.g_Pole == CMagnet.MagnetType.Normal) continue;

            var Dist = Vector3.Distance(m_Actor.transform.position, magnet.transform.position);

            //�ڼ��� force�� �Ÿ����� �۴ٸ� ������ ���ݴϴ�.
            if (Dist > magnet.g_Force * 0.5f) continue;

            //�ֺ��� �ڼ��� �ִ��� �˻��մϴ�.

            if (m_magnetType != magnet.g_Pole)
            {
                if (Vector3.Distance(m_Actor.transform.position, magnet.transform.position) >= 0.1f)
                    MagnetAnotherList.Add(magnet);                
            }
            else
                MagnetSameList.Add(magnet);
        }  
        return (MagnetAnotherList, MagnetSameList);    
    }


    private Vector3 DirectionChoice()
    {
        var DirX = Input.GetAxisRaw("Horizontal");
        var DirY = Input.GetAxisRaw("Vertical");

        

        if (DirX == 0.0f && DirY == 0.0f)
        {
            return Vector3.zero;
        }
        return Vector3.Normalize(new Vector3(DirX, DirY, 0.0f));
    }
    //�ڼ� ã�Ƽ� �η� ȿ�� ����
    private void Attraction(List<CMagnet> AnotherMagnet)
    {

        // �ֺ��� �ڼ��� ���� ���
        if (AnotherMagnet.Count > 0)
        {
            if (m_ChoiceMagnet == null)
            {
                m_ForceDirection = Vector3.zero;
                m_ForceDirection = DirectionChoice();
            }

            foreach (var magnet in AnotherMagnet)
            {
                // �ڼ��� �÷��̾� ���� ����
                var Dir = (magnet.transform.position - m_Actor.transform.position).normalized;
                // �ϳ��� ���� ���
                if (AnotherMagnet.Count == 1)
                {
                    m_ChoiceMagnet = magnet;
                }
                // 2�� �̻��� ���� ���
                else if (m_ChoiceMagnet == null)
                {
                    if (m_ForceDirection == Vector3.zero)
                        return;
                    else
                    {
                        // ������ �̿��ؼ� �ڻ��� �� �޾ƿ���
                        float Cosin = Vector3.Dot(m_ForceDirection, Dir);
                        if (Cosin >= 0)
                        {
                            if (Cosin > m_MagnetCosData)
                                m_ChoiceMagnet = magnet;
                        }
                    }
                }
            }
        }



        // �ڼ��� ������ �Ǹ� ���� �κ�
        if (m_ChoiceMagnet)
        {
            var Dir = (m_ChoiceMagnet.transform.position - m_Actor.transform.position).normalized;
            var Dist = Vector3.Distance(m_Actor.transform.position, m_ChoiceMagnet.transform.position);
            m_Actor.transform.position += Dir * (m_ChoiceMagnet.g_Force - Dist) * Time.deltaTime;
            m_Actor.g_Rigid.useGravity = false;
            m_Actor.g_Rigid.velocity = Vector3.zero;
            if (Dist > m_ChoiceMagnet.g_Force * 0.5f )
            {
                m_ChoiceMagnet = null;
            }
        }
    }

    // �ڼ� ã�Ƽ� ô�� ȿ�� ����
    private void Repulsion(List<CMagnet> SameMagnet)
    {
        if (SameMagnet.Count > 0)
        {
            foreach (var magnet in SameMagnet)
            {
                var Dist = Vector3.Distance(m_Actor.transform.position, magnet.transform.position);

                //m_ForceDirection == Vector3.zero �϶� �� ������ �ϱ� ���ؼ�
                //ô�� �߻��� �߷°� ����
                if (Dist >= 0.1f && m_ForceDirection == Vector3.zero)
                {
                    var Dir = (m_Actor.transform.position - magnet.transform.position).normalized;
                    m_Actor.g_Rigid.AddForce(Dir * ((magnet.g_Force * 2.0f) - Dist), ForceMode.Force);
                    m_Actor.g_Rigid.useGravity = true;
                }
                else
                {
                    //���⼳���� ���� �߷°� ������
                    if (m_ForceDirection == Vector3.zero)
                    {
                        m_ForceDirection = DirectionChoice();
                        m_Actor.g_Rigid.useGravity = false;
                    }
                    else if (m_ForceDirection != Vector3.zero)
                    {
                        m_Actor.g_Rigid.AddForce(m_ForceDirection * ((magnet.g_Force * 2.0f) - Dist), ForceMode.Force);
                        m_Actor.g_Rigid.useGravity = true;
                    }
                }
            }
        }
    }




}
