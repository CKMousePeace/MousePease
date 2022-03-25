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


    // 자석 상태를 변경하는 함수 입니다.
    private void MagnetChange()
    {

        if (Input.GetKeyDown(g_KeyCode))
        {
            //자석 효과 바뀔때 마다 초기화
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


    //자석 효과 적용
    private void Pole()
    {
        if (m_magnetType == CMagnet.MagnetType.Normal)
        {
            // normal일때 대쉬 knockback이 없을 경우 중력값 활성화
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

    //범위에 있는 자석 수 카운트 하는 함수
    //첫 번째 리턴 값은 다른 자석을 리턴
    //두 번째 리턴 값은 같은 자석을 리턴
    private (List<CMagnet> , List<CMagnet>) MagnetCountCheck()
    {

        List<CMagnet> MagnetAnotherList = new List<CMagnet>();
        List<CMagnet> MagnetSameList = new List<CMagnet>();



        foreach (var magnet in m_magnetManager.g_magnet)
        {
            if (magnet.g_Pole == CMagnet.MagnetType.Normal) continue;

            var Dist = Vector3.Distance(m_Actor.transform.position, magnet.transform.position);

            //자석의 force가 거리보다 작다면 리턴을 해줍니다.
            if (Dist > magnet.g_Force * 0.5f) continue;

            //주변에 자석이 있는지 검사합니다.

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
    //자석 찾아서 인력 효과 적용
    private void Attraction(List<CMagnet> AnotherMagnet)
    {

        // 주변에 자석이 있을 경우
        if (AnotherMagnet.Count > 0)
        {
            if (m_ChoiceMagnet == null)
            {
                m_ForceDirection = Vector3.zero;
                m_ForceDirection = DirectionChoice();
            }

            foreach (var magnet in AnotherMagnet)
            {
                // 자석과 플레이어 방향 벡터
                var Dir = (magnet.transform.position - m_Actor.transform.position).normalized;
                // 하나만 있을 경우
                if (AnotherMagnet.Count == 1)
                {
                    m_ChoiceMagnet = magnet;
                }
                // 2개 이상이 있을 경우
                else if (m_ChoiceMagnet == null)
                {
                    if (m_ForceDirection == Vector3.zero)
                        return;
                    else
                    {
                        // 내적을 이용해서 코사인 값 받아오기
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



        // 자석이 선택이 되면 당기는 부분
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

    // 자석 찾아서 척력 효과 적용
    private void Repulsion(List<CMagnet> SameMagnet)
    {
        if (SameMagnet.Count > 0)
        {
            foreach (var magnet in SameMagnet)
            {
                var Dist = Vector3.Distance(m_Actor.transform.position, magnet.transform.position);

                //m_ForceDirection == Vector3.zero 일때 만 읽히게 하기 위해서
                //척력 발생시 중력값 적용
                if (Dist >= 0.1f && m_ForceDirection == Vector3.zero)
                {
                    var Dir = (m_Actor.transform.position - magnet.transform.position).normalized;
                    m_Actor.g_Rigid.AddForce(Dir * ((magnet.g_Force * 2.0f) - Dist), ForceMode.Force);
                    m_Actor.g_Rigid.useGravity = true;
                }
                else
                {
                    //방향설정을 위해 중력값 미적용
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
