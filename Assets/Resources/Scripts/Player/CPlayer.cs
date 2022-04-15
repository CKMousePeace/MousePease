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

    public bool MagnetMagnetType(CMagnet.MagnetType type)
    {
        if (m_ControllerDic.ContainsKey("SkillController"))
        {
            CSkillController SkillController = (CSkillController)m_ControllerDic["SkillController"];
            CMagnetSkill skill = SkillController.GetSkill("Magnet") as CMagnetSkill;
            if (skill == null) return false;
            return skill.g_MagnetType == type;
        }
        return false;
    }


    public bool InCheeseCheck()
    {
        if (m_ControllerDic.ContainsKey("Movement"))
        {
            CPlayerMovement Controller = (CPlayerMovement)m_ControllerDic["Movement"];
            if (Controller == null) return false;
            return Controller.g_InCheese;
        }
        return true;
    }


}




