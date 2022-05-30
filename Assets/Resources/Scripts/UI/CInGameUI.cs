using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInGameUI : MonoBehaviour
{
    [SerializeField] private GameObject m_PauseUI;   
    public void Pause()
    {
        gameObject.SetActive(false);
        m_PauseUI.SetActive(true);
    }
}
