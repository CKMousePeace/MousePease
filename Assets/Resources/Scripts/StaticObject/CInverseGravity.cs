using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInverseGravity : MonoBehaviour
{
    private Rigidbody m_PlayerRigid;
    private Transform m_Transform;
    private CPlayer m_Player;
    [SerializeField] private float m_Force;
    
    
    private void OnTriggerEnter(Collider collider)
    {
        

        if (m_PlayerRigid == null || m_Transform == null)
        {
            if (collider.transform.CompareTag("Player"))
            {                
                m_Transform = collider.transform;
                m_PlayerRigid = m_Transform.GetComponent<Rigidbody>();
                m_Player = m_Transform.GetComponent<CPlayer>();
            }
        }

        if (m_Transform.transform.CompareTag("Player"))
        {
            List<float> FloatValueList = new List<float>();
            FloatValueList.Add(m_Force);

            CBuffBase.BuffInfo info = new CBuffBase.BuffInfo
            {
                g_Value_Float = FloatValueList
            };
        
            m_Player.GenerateBuff("InverseGravity", info);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (m_Player == null) return;
        m_Player.DestroyBuff("InverseGravity");

    }
}
