using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CDynamicObject : CActor
{
    [SerializeField] protected List<CControllerBase> m_ControllerBases;
    [SerializeField] private Animator m_Animator;
    private Dictionary<string, CControllerBase> m_ControllerDic = new Dictionary<string, CControllerBase>();
    private Rigidbody m_Rigid;
    
    public Dictionary<string, CControllerBase> g_ControllerDic => m_ControllerDic;
    public Rigidbody g_Rigid => m_Rigid;
    public Animator g_Animator => m_Animator;
    

    [SerializeField] private float m_fHP;
    public float g_fHP { get => m_fHP; set { m_fHP = value; } }
    

    protected override void Start()
    {
        
        m_Rigid = GetComponent<Rigidbody>();
        
        foreach (var controller in m_ControllerBases)
        {
            controller.init(this);
            m_ControllerDic.Add(controller.g_Name, controller);
        }
    }

    //controller를 검사하는 함수 입니다.
    public bool CompareController(string ControllerName)
    {
        if (m_ControllerDic.ContainsKey(ControllerName))
            return m_ControllerDic[ControllerName].gameObject.activeInHierarchy;
        return false;
    }

    //buff를 검사하는 함수입니다.
    public bool CompareBuff(string BuffName)
    {
        if (m_ControllerDic.ContainsKey("BuffController"))
        {
            CBuffController BuffCotnroller = (CBuffController)m_ControllerDic["BuffController"];
            return BuffCotnroller.ComparerBuff(BuffName);
        }
        return false;
    }
    // 오브젝트를 검사하는 함수 입니다.
    public bool CompareSkill(string SkillName)
    {
        if (m_ControllerDic.ContainsKey("SkillController"))
        {
            CSkillController SkillController =  (CSkillController)m_ControllerDic["SkillController"];
            return SkillController.ComparerSkill(SkillName);
        }
        return false;
    }

    // 오브젝트를 활성화 해주는 함수입니다.
    public bool GenerateBuff(string BuffName, CBuffBase.BuffInfo buffinfo)
    {
        if (m_ControllerDic.ContainsKey("BuffController"))
        {
            CBuffController BuffCotnroller = (CBuffController)m_ControllerDic["BuffController"];
            return BuffCotnroller.GenerateBuff(BuffName, buffinfo);
        }
        return false;
    }

    // 오브젝트를 비활성화 해주는 함수입니다.
    public void DestroyController(string ControllerName)
    {
        if (m_ControllerDic.ContainsKey(ControllerName))
        {
            if (m_ControllerDic[ControllerName].gameObject.activeInHierarchy)
                m_ControllerDic[ControllerName].gameObject.SetActive(false);
        }
    }
}
