using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static bool m_isGameStart = true;
    public static bool g_isGameStart => m_isGameStart;

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

    }


    public void ButtonMovemnt(bool isOn )
    {
        //�÷��̾� ������
        GameStartEvent();

    }
}
