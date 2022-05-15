using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private static bool m_isGameStart = false;

    public static bool g_isGameStart => m_isGameStart;

    [Header("����")]
    [SerializeField] private GameObject m_Boss;

    [Header("���� ���ǵ� ����")]
    [SerializeField] private float m_BossSPeed = 6.0f;

    [Header("�÷��̾�")]
    [SerializeField] private GameObject m_Player;

    [Header("��Ȱ��ȭ = �÷��̾� ��")]
    [SerializeField] private bool m_PlayerHold = false;




    public static void GameStartEvent()
    {
        m_isGameStart = true;
    }

    public static void GameStopEvent()
    {
        m_isGameStart = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }


        PlayerHoldMode();
    }


    private void PlayerHoldMode()
    {
        if (m_PlayerHold == true)
        {
            GameManager.GameStartEvent();
        }
        if (m_PlayerHold == false)
        {
            GameManager.GameStopEvent();
        }



    }



}
