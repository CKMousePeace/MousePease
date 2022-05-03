using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CCameraTriggerBase : MonoBehaviour
{
    static protected Transform m_Focus;
    protected Camera m_MainCamera;

    protected virtual void Start()
    {
        m_MainCamera = Camera.main;
    }    
    public abstract void Execute();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_MainCamera.GetComponent<CCamera>().g_CameraBase = this;
        }
    }
}
