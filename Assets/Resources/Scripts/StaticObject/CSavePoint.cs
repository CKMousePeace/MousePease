using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSavePoint : MonoBehaviour
{
    [Header("SaveManager 넣어주세요")]
    [SerializeField] private GameObject m_Save;

    [Header("플레이어 넣어주세요")]
    [SerializeField] private GameObject m_Player;

    [Header("세이브 번호")]
    [SerializeField] private int SaveNum = 0;

    private void Start()
    {
        m_Save.GetComponent<CSaveController>().LoadGameData();

        switch (m_Save.GetComponent<CSaveController>().gameData.Checker)
        {
            case 1:
                m_Player.transform.position = m_Save.GetComponent<CSaveController>().g_Save[0].transform.position;
                m_Save.GetComponent<CSaveController>().SaveGameData();
                Destroy(m_Save.GetComponent<CSaveController>().g_Save[0]);
                
                break;

            case 2:
                m_Player.transform.position = m_Save.GetComponent<CSaveController>().g_Save[1].transform.position;
                m_Save.GetComponent<CSaveController>().SaveGameData();

                Destroy(m_Save.GetComponent<CSaveController>().g_Save[0]);
                Destroy(m_Save.GetComponent<CSaveController>().g_Save[1]);
                

                break;

            case 3:

                m_Player.transform.position = m_Save.GetComponent<CSaveController>().g_Save[2].transform.position;
                m_Save.GetComponent<CSaveController>().SaveGameData();

                Destroy(m_Save.GetComponent<CSaveController>().g_Save[0]);
                Destroy(m_Save.GetComponent<CSaveController>().g_Save[1]);
                Destroy(m_Save.GetComponent<CSaveController>().g_Save[2]);

                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {        
            switch (SaveNum){

                case 1:
                    m_Save.GetComponent<CSaveController>().gameData.Tutorial_1 = true;
                    m_Save.GetComponent<CSaveController>().gameData.Checker = 1;
                    m_Save.GetComponent<CSaveController>().SaveGameData();
                    Destroy(gameObject);
                    break;

                case 2:
                    m_Save.GetComponent<CSaveController>().gameData.Tutorial_2 = true;
                    m_Save.GetComponent<CSaveController>().gameData.Checker = 2;
                    m_Save.GetComponent<CSaveController>().SaveGameData();
                    Destroy(gameObject);
                    break;

                case 3:
                    m_Save.GetComponent<CSaveController>().gameData.Tutorial_3 = true;
                    m_Save.GetComponent<CSaveController>().gameData.Checker = 3;
                    m_Save.GetComponent<CSaveController>().SaveGameData();
                    Destroy(gameObject);
                    break;

            }
        }
    }
}
