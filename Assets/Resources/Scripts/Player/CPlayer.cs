using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : CDynamicObject
{
    [SerializeField] private SkinnedMeshRenderer m_MashRender;
    [SerializeField] private float m_fMaxStamina;
    [SerializeField] private float m_fIncreaseStamina;
    private float m_fStamina;
    

    public float g_fStamina { get { return m_fStamina; } set { m_fStamina = value; } }
    public float g_fMaxStamina => m_fMaxStamina;
    protected override void Start()
    {
        base.Start();
        m_fStamina = m_fMaxStamina;
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
        else
        {
            if (g_fHP <= 0.0f)
            {
                g_IsDead = true;
            }

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

        if (IncreaseCheck())
        {
            m_fStamina += m_fIncreaseStamina * Time.deltaTime;
            if (m_fStamina >= m_fMaxStamina)
            {
                m_fStamina = m_fMaxStamina;
            }
        }

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
    private bool IncreaseCheck()
    {
        if (CompareController("Jump") || CompareController("WallJump") || CompareController("Dash") || CompareSkill("DownHill"))
        {
            return false;
        }
        return true;
    }
}






