using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : CDynamicObject
{
    [SerializeField] private SkinnedMeshRenderer m_MashRender;
    private float m_CurrentTime = 0.0f;
    protected override void Start()
    {
        base.Start();
    }

    protected void Update()
    {
        if (CompareBuff("KnockBack")) return;

        foreach (var controller in m_ControllerBases)a
        {
            var controllerGameObj = controller.gameObject;
            if (Input.GetKeyDown(controller.g_Key))
            {
                if (controllerGameObj.activeInHierarchy) continue;
                controllerGameObj.SetActive(true);
            }
        }

        if (m_CurrentTime > 0.5f)
        {

            if (m_MashRender.material.color == Color.red)
            {
                m_MashRender.material.color = Color.white;
                m_MashRender.material.SetColor("_EmissionColor", Color.white);
            }
        }

        m_CurrentTime += Time.deltaTime;
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

    //public bool MagnetMagnetType(CMagnet.MagnetType type)
    //{
    //    if (m_ControllerDic.ContainsKey("SkillController"))
    //    {
    //        CSkillController SkillController = (CSkillController)m_ControllerDic["SkillController"];
    //        CMagnetSkill skill = SkillController.GetSkill("Magnet") as CMagnetSkill;
    //        if (skill == null) return false;
    //        return skill.g_MagnetType == type;
    //    }
    //    return false;
    //}


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


    public void SetColor()
    {
        
        m_MashRender.material.color = Color.red;
        m_MashRender.material.SetColor("_EmissionColor", Color.red);
        m_CurrentTime = 0.0f;
    }
    /*
     * 
     * 


     */

}




