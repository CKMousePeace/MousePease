using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrackPad : CStaticObject
{
    [SerializeField] private GameObject m_gameObject;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_CoolTime;
    private float m_CurrnetTime = 0;
    private float m_StartPosition = 0;
    
    protected void Awake()
    {
        m_StartPosition = m_gameObject.transform.localPosition.y;
    }
    private void Update()
    {
        m_CurrnetTime += Time.deltaTime * m_CoolTime;

        var pos = m_gameObject.transform.localPosition;
        pos.y = Mathf.Abs (m_StartPosition * Mathf.Pow(Mathf.Cos(m_CurrnetTime), m_Speed));        
        m_gameObject.transform.localPosition = pos;
    }
}
