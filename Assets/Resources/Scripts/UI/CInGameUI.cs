using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInGameUI : MonoBehaviour
{
    [SerializeField] private GameObject m_PauseUI;
    private void Update()
    {
        if (m_PauseUI == null) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
        gameObject.SetActive(false);
        m_PauseUI.SetActive(true);
    }
}
