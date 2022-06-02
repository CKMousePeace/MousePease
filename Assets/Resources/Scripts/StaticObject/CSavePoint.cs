using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSavePoint : MonoBehaviour
{
    [Header("SaveManager �־��ּ���")]
    [SerializeField] private GameObject m_Save;

    [Header("�÷��̾� �־��ּ���")]
    [SerializeField] private GameObject m_Player;


    private void Start()
    {
        if (m_Save.GetComponent<CSaveController>().gameData.Tutorial_Middle == true)
        {
            m_Player.transform.position = gameObject.transform.position;
            m_Save.GetComponent<CSaveController>().SaveGameData();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.CompareTag("Player"))
        {        
            m_Save.GetComponent<CSaveController>().gameData.Tutorial_Middle = true;
            m_Save.GetComponent<CSaveController>().SaveGameData();

            Destroy(gameObject);
        }

    }


}
