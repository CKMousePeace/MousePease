using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CSavePoint : MonoBehaviour
{
    [Header("SaveManager 넣을게~ ")]
    [SerializeField] private GameObject m_Save;

    [Header("플레이어 넣을게~ ")]
    [SerializeField] private GameObject m_Player;

    [Header("세이브 번호")]
    [SerializeField] private int SaveNum = 0;



    private FMOD.Studio.EventInstance SFX_SavePoint;

    private void Start()
    {
        m_Save.GetComponent<CSaveController>().LoadGameData();

        switch (m_Save.GetComponent<CSaveController>().gameData.Checker)
        {
            case 1:
                m_Player.transform.position = new Vector3(m_Save.GetComponent<CSaveController>().g_Save[0].transform.position.x,
                    m_Save.GetComponent<CSaveController>().g_Save[0].transform.position.y, -1.9f);
                m_Save.GetComponent<CSaveController>().SaveGameData();
                Destroy(m_Save.GetComponent<CSaveController>().g_Save[0].GetComponent<CSavePoint>());
                break;

            case 2:
                m_Player.transform.position = new Vector3(m_Save.GetComponent<CSaveController>().g_Save[1].transform.position.x,
                    m_Save.GetComponent<CSaveController>().g_Save[1].transform.position.y, -1.9f);
                m_Save.GetComponent<CSaveController>().SaveGameData();

                Destroy(m_Save.GetComponent<CSaveController>().g_Save[0].GetComponent<CSavePoint>());
                Destroy(m_Save.GetComponent<CSaveController>().g_Save[1].GetComponent<CSavePoint>());
                

                break;

            case 3:

                m_Player.transform.position = new Vector3(m_Save.GetComponent<CSaveController>().g_Save[2].transform.position.x,
                    m_Save.GetComponent<CSaveController>().g_Save[2].transform.position.y, -1.9f);
                m_Save.GetComponent<CSaveController>().SaveGameData();

                Destroy(m_Save.GetComponent<CSaveController>().g_Save[0].GetComponent<CSavePoint>());
                Destroy(m_Save.GetComponent<CSaveController>().g_Save[1].GetComponent<CSavePoint>());
                Destroy(m_Save.GetComponent<CSaveController>().g_Save[2].GetComponent<CSavePoint>());

                break;


            case 4:
                m_Player.transform.position = new Vector3(m_Save.GetComponent<CSaveController>().g_Save[3].transform.position.x,
    m_Save.GetComponent<CSaveController>().g_Save[3].transform.position.y, -1.9f);
                m_Save.GetComponent<CSaveController>().SaveGameData();

                Destroy(m_Save.GetComponent<CSaveController>().g_Save[0].GetComponent<CSavePoint>());
                Destroy(m_Save.GetComponent<CSaveController>().g_Save[1].GetComponent<CSavePoint>());
                Destroy(m_Save.GetComponent<CSaveController>().g_Save[2].GetComponent<CSavePoint>());
                Destroy(m_Save.GetComponent<CSaveController>().g_Save[3].GetComponent<CSavePoint>());

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

                    PlaySavePoint();
                    Destroy(this);
                    break;

                case 2:
                    m_Save.GetComponent<CSaveController>().gameData.Tutorial_2 = true;
                    m_Save.GetComponent<CSaveController>().gameData.Checker = 2;
                    m_Save.GetComponent<CSaveController>().SaveGameData();

                    PlaySavePoint();
                    Destroy(this);
                    break;

                case 3:
                    m_Save.GetComponent<CSaveController>().gameData.Tutorial_3 = true;
                    m_Save.GetComponent<CSaveController>().gameData.Checker = 3;
                    m_Save.GetComponent<CSaveController>().SaveGameData();
                    
                    PlaySavePoint();
                    Destroy(this);
                    break;

                case 4:
                    m_Save.GetComponent<CSaveController>().gameData.Tutorial_4 = true;
                    m_Save.GetComponent<CSaveController>().gameData.Checker = 4;
                    m_Save.GetComponent<CSaveController>().SaveGameData();

                    PlaySavePoint();
                    Destroy(this);
                    break;

            }
        }
    }



    private void PlaySavePoint()
    {

        CObjectPool.g_instance.ObjectPop("SaveBoi", new Vector3(transform.position.x,
                 transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0), new Vector3(1.0f, 1.0f, 1.0f));

        SFX_SavePoint = RuntimeManager.CreateInstance("event:/Interactables/SFX_SavePoint");
        SFX_SavePoint.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_SavePoint.start();
        SFX_SavePoint.release();
    }
}
