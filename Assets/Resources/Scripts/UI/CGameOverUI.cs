using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CGameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject m_InGameUI;
        
    private void OnEnable()
    {
        m_InGameUI.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }
    
    

    
}
