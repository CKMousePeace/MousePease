using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera : MonoBehaviour
{
    [SerializeField] private CCameraTriggerBase m_CameraBase;
    public CCameraTriggerBase g_CameraBase{get => m_CameraBase; set { m_CameraBase = value; } }

    
    private void LateUpdate()
    {
        m_CameraBase.Execute();
    }
}
