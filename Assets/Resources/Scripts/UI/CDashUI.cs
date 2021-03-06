using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDashUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image m_DashImage;
    private CPlayer m_Player;
    private CDash m_Dash;
    private float m_Current;
    void Awake()
    {
        m_Player = GameObject.FindWithTag("Player").GetComponent<CPlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Dash == null)
        {
            m_Dash = m_Player.GetController("Dash") as CDash;            
            return;
        }
        m_Current = Mathf.Lerp(m_Current , m_Player.g_fStamina , 0.3f);
        m_DashImage.fillAmount = m_Current / m_Player.g_fMaxStamina;
    }
}
