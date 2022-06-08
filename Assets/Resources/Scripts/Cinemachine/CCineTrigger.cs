using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CCineTrigger : MonoBehaviour
{

    [SerializeField] private GameObject m_CineBoss;
    [SerializeField] private GameObject m_CineCamera;
    [SerializeField] private PlayableDirector m_CineTimeline;

    [SerializeField] private float m_PlayereditSpeed;
    [SerializeField] private CPlayer m_Player;
    private CPlayerMovement m_PlayerMovement;

    [Header("�����. �������")]
    [SerializeField]  private bool TestPlayer = false;
        //�ڿ� ���� �κ� 
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))   
        {
            if (m_PlayerMovement == null)
                m_PlayerMovement = m_Player.GetController("Movement") as CPlayerMovement;

            if (m_PlayerMovement == null)
            {
                Debug.LogError("Movement Error");
                return;
            }

            m_PlayerMovement.g_fMaxSpeed = m_PlayereditSpeed;



            m_CineBoss.SetActive(true);
            m_CineCamera.SetActive(false);
            m_CineTimeline.Play();
        }


    }


    private void Update()
    {
        if(TestPlayer == true)
        {
            m_CineBoss.SetActive(true);
            m_CineCamera.SetActive(false);
            m_CineTimeline.Play();
        }
    }
}
