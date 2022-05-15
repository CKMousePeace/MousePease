using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CUIText : MonoBehaviour
{
    [SerializeField] private Text m_Slow;
    [SerializeField] private Text m_Dash;

    private CPlayer m_CPlayer;
    private CDash m_DashConrol;
    private CSlow m_SlowConrol;

    private void Start()
    {
        m_CPlayer = GameObject.FindWithTag("Player").GetComponent<CPlayer>();
        
        if (m_CPlayer == null)
            Debug.LogError("CPlayer가 없습니다.");        

    }



    private void Update()
    {
        if (m_DashConrol == null)
        {
            m_DashConrol = (CDash)m_CPlayer.GetController("Dash");
        }
        if (m_SlowConrol == null)
        {
            m_SlowConrol = (CSlow)m_CPlayer.GetBuff("Slow");
        }


        if (m_CPlayer.CompareBuff("Slow"))
        {            
            m_Slow.text = "Slow Time : " + Mathf.CeilToInt(m_SlowConrol.g_SlowTime);
        }
        else
        {
            m_Slow.text = "Slow Time : 0";
        }


        
        m_Dash.text = "Dash Cool Time : " + Mathf.CeilToInt(m_DashConrol.g_DashCool);        
        
    }



}
