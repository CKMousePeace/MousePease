using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerBoi;
    [SerializeField] private NavMeshAgent BossBoi;


    // Update is called once per frame

    private static bool m_isGameStart = false;

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
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }


    private void Start()
    {
        
    }
}
