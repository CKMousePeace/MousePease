using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheese : CStaticObject
{
    // Start is called before the first frame update    
    [SerializeField] CBuffBase.BuffInfo m_BuffInfo;
    CPlayer m_Player;

    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerStay(Collider other)
    {
        var Target = other.gameObject.GetComponent<CPlayer>();
        if (Target == null)
            return;
        if (m_Player == null && other.CompareTag("Player"))
        {
            m_Player = other.gameObject.GetComponent<CPlayer>();
        }
        if (m_Player != null)
        {
            m_Player.SetInCheese(true);            
        }
        
        Target.GenerateBuff("Fast" , m_BuffInfo);
    }

    private void OnTriggerExit(Collider other)
    {
        var Target = other.gameObject.GetComponent<CPlayer>();
        if (Target == null)
            return;
        if (m_Player == null && other.CompareTag("Player"))
        {
            m_Player = other.gameObject.GetComponent<CPlayer>();
        }
        if (m_Player != null)
        {
            m_Player.SetInCheese(false);
        }

        Target.DestroyBuff("Fast");
    }
}
