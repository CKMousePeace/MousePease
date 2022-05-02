using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CDynamicObject : CActor
{
    [SerializeField] protected List<CControllerBase> m_ControllerBases;
    [SerializeField] private Animator m_Animator;
    protected Dictionary<string, CControllerBase> m_ControllerDic = new Dictionary<string, CControllerBase>();
    protected bool m_DeadAnim = false;
    private Rigidbody m_Rigid;
   
    
    public Dictionary<string, CControllerBase> g_ControllerDic => m_ControllerDic;
    public Rigidbody g_Rigid  =>m_Rigid;   
    public Animator g_Animator => m_Animator;


    public bool g_IsDead { get; set; } = false;

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
    public bool GenerateController(string ControllerName)
    {
        if (m_ControllerDic.ContainsKey("BuffController"))
        {
            if (m_ControllerDic.ContainsKey(ControllerName))
            {
                m_ControllerDic[ControllerName].gameObject.SetActive(true);
                return true;
            }
            
        }
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

    //컨트롤러를 구하는 함수 입니다.
    public CControllerBase GetController(string ControllerName)
    {
        if (m_ControllerDic.ContainsKey(ControllerName))
        {
            return m_ControllerDic[ControllerName];
        }
        return null;
    }


    protected IEnumerator DeadCheak()
    {
        m_DeadAnim = true;
        m_Animator.SetTrigger("Dead");
        yield return new WaitForSeconds(5);
        g_IsDead = false;
        gameObject.SetActive(false);
        
    }  
  

}
