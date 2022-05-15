using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSkillController : CControllerBase
{
    [SerializeField] private List<CSkillBase> m_Skills;
    [SerializeField] private Dictionary<string ,CSkillBase> m_DicSkills = new Dictionary<string, CSkillBase>();

    
    public override void init(CDynamicObject actor)
    {
        base.init(actor);
        foreach (CSkillBase skill in m_Skills)
            skill.init(actor);
        foreach (var Skill in m_Skills)
        {
            m_DicSkills.Add(Skill.g_SkillName, Skill);
        }
    }

    // 스킬 사용중인지 판별 하는 함수
    public bool ComparerSkill(string SkillName)
    {
        if (m_DicSkills.ContainsKey(SkillName))
        {
            if (m_DicSkills[SkillName].gameObject.activeInHierarchy)
                return true;
        }
        return false;
    }

    public CSkillBase GetSkill(string SkillName)
    {
        if (m_DicSkills.ContainsKey(SkillName))
        {
            return m_DicSkills[SkillName];
        }
        return null;
    }

    public bool GenerateSkill(string SkillName)
    {
        if (!GameManager.g_isGameStart) return false;

        if (m_DicSkills.ContainsKey(SkillName))
        {

            if (!m_DicSkills[SkillName].gameObject.activeInHierarchy)
            {
                m_DicSkills[SkillName].gameObject.SetActive(true);
                return true;
            }
            return false;
            
        }
        return false;
    }



}
