using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBuffController : CControllerBase
{
    [SerializeField] protected List<CBuffBase> m_Buffs;
    [SerializeField] private Transform m_Bucket;
    private Dictionary<string, CBuffBase> m_DicBuffs = new Dictionary<string, CBuffBase>();


    public Dictionary<string, CBuffBase> g_DicBuffs => m_DicBuffs;
    public override void init(CDynamicObject actor)
    {
        base.init(actor);
        foreach (var buff in m_Buffs)
        {
            buff.init(actor);
            m_DicBuffs.Add(buff.g_BuffName , buff);
        }
    }

    
    //������ �ִ��� �˻��ϴ� �Լ� �Դϴ�.
    public bool ComparerBuff(string BuffName)
    {
        if (m_DicBuffs.ContainsKey(BuffName))
        {
            if (m_DicBuffs[BuffName].gameObject.activeInHierarchy)
            {
                
                return true;            
            }
        }
        return false;
    }
    public bool DestoryBuff(string name)
    {
        if (m_DicBuffs.ContainsKey(name))
        {
            if (m_DicBuffs[name].gameObject.activeInHierarchy)
            {
                m_DicBuffs[name].gameObject.SetActive(false);
                return true;
            }

        }
        return false;

    }
    //������ �����ϴ� �����Դϴ�.


    public bool GenerateBuff(string BuffName , CBuffBase.BuffInfo buffinfo)
    {
        if (m_DicBuffs.ContainsKey(BuffName))
        {
            m_DicBuffs[BuffName].GenerateBuff(buffinfo);

            if (!m_DicBuffs[BuffName].gameObject.activeInHierarchy)
                m_DicBuffs[BuffName].gameObject.SetActive(true);
            return true;
        }
        else
        {
            GameObject buffPrefab = Resources.Load<GameObject>("Prefabs/Buffs/" + BuffName);
            if (buffPrefab == null)
            {
                Debug.LogError("���� �ȿ� �������� ���� ���� �ʽ��ϴ�.");
            }
            if (m_Bucket != null)
            {
                var buffObj = Instantiate(buffPrefab, m_Bucket).GetComponent<CBuffBase>();
                m_DicBuffs.Add(BuffName, buffObj);
                m_DicBuffs[BuffName].GenerateBuff(buffinfo);
            }
            else
            {
                var buffObj = Instantiate(buffPrefab, transform).GetComponent<CBuffBase>();
                m_DicBuffs.Add(BuffName, buffObj);
                m_DicBuffs[BuffName].GenerateBuff(buffinfo);
            }
        }
        return false;
    }    
}
