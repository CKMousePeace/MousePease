using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerHPBar : MonoBehaviour
{
    private CPlayer m_Player;
    private float m_MaxHP;
    [SerializeField] private GameObject m_HeartePrefabs;
    [SerializeField] private GameObject m_GameOver;
    [SerializeField] private float m_GameDelay;
    private List<CUIHeate> m_Heartes = new List<CUIHeate>();




    private void Awake()
    {
        m_Player = GameObject.FindWithTag("Player").GetComponent<CPlayer>();
        m_MaxHP = m_Player.g_fHP;
        for (int i = 0; i < m_Player.g_fHP; i++)
        {
            var Obj = Instantiate(m_HeartePrefabs, transform);
            var RectTransform = Obj.GetComponent<RectTransform>();
            RectTransform.localPosition = new Vector3(17 + (i * 60), 12);
            m_Heartes.Add(Obj.GetComponent<CUIHeate>());
        }
    }

    private void Update()
    {
        if (m_Player.g_fHP < 0.0f)
        {
            m_Player.g_fHP = 0.0f;
        }


        for (int i = (int)m_Player.g_fHP; i < m_MaxHP; i++)
        {
            m_Heartes[i].SetActiveHearte(false);
        }
        for (int i = 0; i < (int)m_Player.g_fHP; i++)
        {
            m_Heartes[i].SetActiveHearte(true);

        }

        if (m_Player.g_fHP <= 0.0f)
        {
            StartCoroutine(PlayerDead());
        }
    }

    private IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(m_GameDelay);
        m_GameOver.SetActive(true);

    }


}
