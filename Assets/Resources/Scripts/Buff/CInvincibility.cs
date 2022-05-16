using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInvincibility : CBuffBase
{


    private SkinnedMeshRenderer m_MashRender;
    private float m_CurrentTime = 0.0f;
    private CPlayer m_Play;

    // Start is called before the first frame update


    public override void init(CDynamicObject dynamicObject)
    {
        base.init(dynamicObject);

        if (m_Play == null)
        {
            m_Play = (CPlayer)g_DynamicObject;
            m_MashRender = m_Play.GetComponentInChildren<SkinnedMeshRenderer>();
        }
    }
    
    protected override void OnBuffInit(BuffInfo buff)
    {
        SetColor();
        m_CurrentTime = 0.0f;
    }

    private void Update()
    {
        if (m_CurrentTime > 2.0f)
        {
            
            if (m_MashRender.material.color == Color.red)
            {
                m_MashRender.material.color = Color.white;
                m_MashRender.material.SetColor("_EmissionColor", Color.white);
            }


            gameObject.SetActive(false);
        }
        m_CurrentTime += Time.deltaTime;
    }


    public void SetColor()
    {
        m_MashRender.material.color = Color.red;
        m_MashRender.material.SetColor("_EmissionColor", Color.red);
        m_CurrentTime = 0.0f;

    }

}
