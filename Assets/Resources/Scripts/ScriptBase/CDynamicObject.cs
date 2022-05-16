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
    public bool g_MoveCheck{ get; set; }


    public Dictionary<string, CControllerBase> g_ControllerDic => m_ControllerDic;
    public Rigidbody g_Rigid  =>m_Rigid;   
    public Animator g_Animator => m_Animator;



    public bool g_IsDead { get; set; } = false;
 


    [SerializeField] private float m_fHP;
    public float g_fHP { get => m_fHP; set { m_fHP = value; } }
    

    protected override void Start()
    {
        
        m_Rigid = GetComponent<Rigidbody>();
        g_IsDead = false;
        foreach (var controller in m_ControllerBases)
        {
            controller.init(this);
            m_ControllerDic.Add(controller.g_Name, controller);
        }
    }

    //controller�� �˻��ϴ� �Լ� �Դϴ�.
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
                if (!m_ControllerDic[ControllerName].gameObject.activeInHierarchy)
                    m_ControllerDic[ControllerName].gameObject.SetActive(true);
                return true;
            }
            
        }
        return false;
    }

    //buff�� �˻��ϴ� �Լ��Դϴ�.
    public bool CompareBuff(string BuffName)
    {
        if (m_ControllerDic.ContainsKey("BuffController"))
        {
            CBuffController BuffCotnroller = (CBuffController)m_ControllerDic["BuffController"];
            return BuffCotnroller.ComparerBuff(BuffName);
        }
        return false;
    }
    // ������Ʈ�� �˻��ϴ� �Լ� �Դϴ�.
    public bool CompareSkill(string SkillName)
    {
        if (m_ControllerDic.ContainsKey("SkillController"))
        {
            CSkillController SkillController =  (CSkillController)m_ControllerDic["SkillController"];
            return SkillController.ComparerSkill(SkillName);
        }
        return false;
    }

    // ��ų �����ϴ� �Լ� �Դϴ�.
    public bool GenerateSkill(string SkillName)
    {
        if (m_ControllerDic.ContainsKey("SkillController"))
        {
            CSkillController SkillController = (CSkillController)m_ControllerDic["SkillController"];
            return SkillController.GenerateSkill(SkillName);
        }
        return false;
    }


    // ������Ʈ�� Ȱ��ȭ ���ִ� �Լ��Դϴ�.
    public bool GenerateBuff(string BuffName, CBuffBase.BuffInfo buffinfo)
    {
        if (m_ControllerDic.ContainsKey("BuffController"))
        {
            CBuffController BuffCotnroller = (CBuffController)m_ControllerDic["BuffController"];
            return BuffCotnroller.GenerateBuff(BuffName, buffinfo);
        }
        return false;
    }

    // ������Ʈ�� ��Ȱ��ȭ ���ִ� �Լ��Դϴ�.
    public void DestroyController(string ControllerName)
    {
        if (m_ControllerDic.ContainsKey(ControllerName))
        {
            if (m_ControllerDic[ControllerName].gameObject.activeInHierarchy)
                m_ControllerDic[ControllerName].gameObject.SetActive(false);
        }
    }

    //��Ʈ�ѷ��� ���ϴ� �Լ� �Դϴ�.
    public CControllerBase GetController(string ControllerName)
    {
        if (m_ControllerDic.ContainsKey(ControllerName))
        {
            return m_ControllerDic[ControllerName];
        }
        return null;
    }


    public CBuffBase GetBuff(string name)
    {
        if (m_ControllerDic.ContainsKey("BuffController"))
        {
            var BuffController = m_ControllerDic["BuffController"] as CBuffController;
            if (BuffController == null)
                return null;

            if (BuffController.ComparerBuff(name))
            {
                return BuffController.g_DicBuffs[name];
            }
        }
        return null;
    }
    public void DestroyBuff(string name)
    {
        if (m_ControllerDic.ContainsKey("BuffController"))
        {
            var BuffController = m_ControllerDic["BuffController"] as CBuffController;
            if (BuffController == null)
            {
                Debug.LogError("BuffController�� �����ϴ�.");
                return;
            }

            if (!BuffController.DestoryBuff(name))
            {
                Debug.LogError("Buff ������ ���� �߽��ϴ�.");
            }            

        }
        else
        {
            Debug.LogError("BuffController�� �����ϴ�.");
        }
    }
    protected IEnumerator DeadCheak()
    {
        m_DeadAnim = true;
        foreach (var item in m_ControllerBases)
        {
            if (item.gameObject.activeInHierarchy)
                item.gameObject.SetActive(false);
        }

        m_Animator.SetTrigger("Dead");
        yield return new WaitForSeconds(5);        
        gameObject.SetActive(false);        
    }  
  

}
