using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInverseGravityBuff : CBuffBase
{

    float m_Force;
    protected override void OnBuffInit(BuffInfo buff)
    {        
        m_Force = buff.g_Value_Float[0];
    }

    private void FixedUpdate()
    {
        var Velocity = g_DynamicObject.g_Rigid.velocity;
        Velocity.y = m_Force;
        g_DynamicObject.g_Rigid.velocity = Velocity;
    }
}
