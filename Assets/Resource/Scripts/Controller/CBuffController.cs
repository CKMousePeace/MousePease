using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBuffController : CControllerBase
{
    [SerializeField] protected List<CBuffBase> m_Buffs;
    private Dictionary<string, CBuffBase> m_DicBuffs = new Dictionary<string, CBuffBase>();

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
        foreach (var buff in m_Buffs)
        {
            buff.init(actor);
            m_DicBuffs.Add(buff.g_BuffName , buff);
        }
    }

    public bool ComparerBuff(string BuffName)
    {
        if (m_DicBuffs.ContainsKey(BuffName))
        {
            if (m_DicBuffs[BuffName].gameObject.activeInHierarchy)
                return true;            
        }
        return false;
    }
    public bool GenerateBuff(string BuffName , CBuffBase.BuffInfo buffinfo)
    {
        if (m_DicBuffs.ContainsKey(BuffName))
        {
            m_DicBuffs[BuffName].GenerateBuff(buffinfo);

            if (!m_DicBuffs[BuffName].gameObject.activeInHierarchy)
                m_DicBuffs[BuffName].gameObject.SetActive(true);
            
            return true;
        }
        return false;
    }
}
