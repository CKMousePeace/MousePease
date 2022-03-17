using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CMagnetSkill : CSkillBase
{
    [SerializeField] private Image Magnetimage;
    [SerializeField] private CMagnet.MagnetType m_magnetType = CMagnet.MagnetType.Normal;
    private CMagnetManager m_magnetManager;

    public override void init(CDynamicObject actor)
    {
        
        base.init(actor);
        Magnetimage.color = Color.gray;
        var manager = GameObject.Find("MagnetManager");
        if (manager != null)
            m_magnetManager = manager.GetComponent<CMagnetManager>();
    }
    private void Update()
    {
        ButtonDown();
        Normal();
        Pole();
    }

    // 자석 상태를 변경하는 함수 입니다.
    private void ButtonDown()
    {
        
        if (Input.GetKeyDown(g_KeyCode))
        {
            m_magnetType++;
            m_magnetType = (CMagnet.MagnetType)((int)m_magnetType % 3);

            if (m_magnetType == CMagnet.MagnetType.Normal)
                Magnetimage.color = Color.gray;
            if (m_magnetType == CMagnet.MagnetType.NPole)
                Magnetimage.color = Color.red;
            if (m_magnetType == CMagnet.MagnetType.SPole)
                Magnetimage.color = Color.blue;
        }
    }

    
    private void Normal()
    {
        if (m_magnetType != CMagnet.MagnetType.Normal) return;
        if (!m_Actor.CompareController("Dash"))
            m_Actor.g_Rigid.useGravity = true;
        
    }


    private void Pole()
    {

        if (m_magnetType == CMagnet.MagnetType.Normal) return;

        foreach (var magnet in m_magnetManager.g_magnet)
        {
            if (magnet.g_Pole == CMagnet.MagnetType.Normal) continue;


            var Dist = Vector3.Distance(m_Actor.transform.position, magnet.transform.position);

            //자석의 force가 거리보다 작다면 리턴을 해줍니다.
            if (Dist > magnet.g_Force * 0.5f)
            {
                if (!m_Actor.CompareController("Dash"))                    
                    m_Actor.g_Rigid.useGravity = true;

                continue;
            }
            
            if (m_magnetType != magnet.g_Pole)
            {
                //주변에 자석이 있는지 검사합니다.
                if (Vector3.Distance(m_Actor.transform.position, magnet.transform.position) >= 0.1f)
                {
                    var Dir = (magnet.transform.position - m_Actor.transform.position).normalized;
                    m_Actor.transform.position += Dir * (magnet.g_Force - Dist) * Time.deltaTime;
                    m_Actor.g_Rigid.useGravity = false;
                    m_Actor.g_Rigid.velocity = Vector3.zero;
                }
            }
            else
            {
                var Dir = (m_Actor.transform.position - magnet.transform.position).normalized;
                m_Actor.g_Rigid.AddForce(Dir * (magnet.g_Force - Dist), ForceMode.Force);
                
            }

        }
    }
    
}
