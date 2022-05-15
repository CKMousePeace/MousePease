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
        if (!GameManager.g_isGameStart) return;
        if (g_IsDead)
        {
            if (!m_DeadAnim)
            {
                StartCoroutine(DeadCheak());
            }             
            return;
        }


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
    
    public void SetInCheese(bool State)
    {
        var Movemet = GetController("Movement") as CPlayerMovement;
        if (Movemet == null)
        {
            Debug.LogError("Movement controller를 못 찾았습니다.");
            return;
        }
        g_Rigid.velocity = new Vector3(0.0f , 0.0f , 0.0f);        
        Movemet.g_IsInCheese = State;
    }
    public bool CompareInCheese()
    {
        var Movemet = GetController("Movement") as CPlayerMovement;
        if (Movemet == null)
        {
            Debug.LogError("Movement controller를 못 찾았습니다.");
            return false;
        }
        return Movemet.g_IsInCheese;
    }
    public void SetColor()
    {
        m_MashRender.material.color = Color.red;
        m_MashRender.material.SetColor("_EmissionColor", Color.red);
        m_CurrentTime = 0.0f;

    }

}




