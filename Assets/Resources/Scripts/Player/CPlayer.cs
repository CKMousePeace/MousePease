using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : CDynamicObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected void Update()
    {
        if (CompareBuff("KnockBack")) return;

        foreach (var controller in m_ControllerBases)
        {           
            var controllerGameObj = controller.gameObject;
            if (Input.GetKeyDown(controller.g_Key))
            {
                if (controllerGameObj.activeInHierarchy) continue;
                controllerGameObj.SetActive(true);
            }
        }       
       
    }

    public bool MagnetSkillCheck()
    {
        if (m_ControllerDic.ContainsKey("SkillController"))
        {
            CSkillController SkillController = (CSkillController)m_ControllerDic["SkillController"];
            CMagnetSkill skill = SkillController.GetSkill("Magnet") as CMagnetSkill;
            if (skill == null) return false;
            return skill.g_MagnetCheck;
        }
        return false;
    }

    
}
