using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkillBase : MonoBehaviour
{
    [SerializeField] private string m_SkillName;
    [SerializeField] private KeyCode m_KeyCode;
    public KeyCode g_KeyCode => m_KeyCode;
    public string g_SkillName => m_SkillName;
    protected CDynamicObject m_Actor;
      

    virtual public void init(CDynamicObject actor) => m_Actor = actor;

}
