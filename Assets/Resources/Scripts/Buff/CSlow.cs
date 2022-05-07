using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSlow : CBuffBase
{
    private float m_SlowTime;
    private float m_SlowSpeed;
    [SerializeField] private float m_CurrnetTime;

    public float g_SlowSpeed => m_SlowSpeed;

    protected override void OnBuffInit(BuffInfo buff)
    {
        try
        {
            m_CurrnetTime = 0.0f;
            m_SlowSpeed = buff.g_Value_Float[0];
            m_SlowTime = buff.g_Value_Float[1];
        }
        catch 
        {
            Debug.LogError("인덱스가 맞지 않습니다.");
        }
    }
    private void Update()
    {
        m_CurrnetTime += Time.deltaTime;
        if (m_CurrnetTime >= m_SlowTime)
        {
            gameObject.SetActive(false);
        }
    }
}
