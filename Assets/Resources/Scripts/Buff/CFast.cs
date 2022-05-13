using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFast : CBuffBase
{
    private float m_FastSpeed;

    public float g_FastSpeed => m_FastSpeed;

    protected override void OnBuffInit(BuffInfo buff)
    {
        try
        {
            m_FastSpeed = buff.g_Value_Float[0];
        }
        catch 
        {
            Debug.LogError("인덱스가 맞지 않습니다.");
        }

        
    }
}
