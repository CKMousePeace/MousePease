using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CCineTrigger : MonoBehaviour
{

    [Header("보스 연출용 스크립트")]

    [SerializeField] private GameObject m_CineBoss;
    [SerializeField] private GameObject m_CineCamera;
    [SerializeField] private PlayableDirector m_CineTimeline;

    [SerializeField] private float m_PlayereditSpeed;
    [SerializeField] private CPlayer m_Player;
    private CPlayerMovement m_PlayerMovement;

    [SerializeField] private float m_StopPlayer = 4.0f;

    [Header("디버그. 연출재생")]
    [SerializeField]  private bool TestPlayer = false;


    private void Update()
    {
        if (TestPlayer == true)
        {
            m_CineBoss.SetActive(true);
            m_CineCamera.SetActive(false);
            m_CineTimeline.Play();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))   
        {
            StartCoroutine(StopPlayer(m_StopPlayer));

            if (m_PlayerMovement == null)
                m_PlayerMovement = m_Player.GetController("Movement") as CPlayerMovement;

            if (m_PlayerMovement == null)
            {
                Debug.LogError("Movement Error");
                return;
            }
            m_PlayerMovement.g_fMaxSpeed = m_PlayereditSpeed;


        }
    }

    IEnumerator StopPlayer(float WaitTime)
    {
        m_CineBoss.SetActive(true);
        m_CineCamera.SetActive(false);
        m_CineTimeline.Play();

        GameManager.GameStopEvent();
        yield return new WaitForSeconds(WaitTime);
        GameManager.GameStartEvent();
        Destroy(gameObject);
    }
}
