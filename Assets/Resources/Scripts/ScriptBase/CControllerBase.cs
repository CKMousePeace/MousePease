using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CControllerBase : MonoBehaviour
{
    [SerializeField] protected KeyCode m_Key;
    [SerializeField] private string m_ControllerName;


    protected CDynamicObject m_Actor;    
    public KeyCode g_Key => m_Key;
    public string g_Name => m_ControllerName;
    virtual public void init(CDynamicObject actor) => m_Actor = actor;    
}
