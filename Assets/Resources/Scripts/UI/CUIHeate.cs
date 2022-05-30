using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIHeate : MonoBehaviour
{
    [SerializeField] private GameObject m_Hearte;
    [SerializeField] private GameObject m_GrayHearte;

    public void SetActiveHearte(bool Active)
    {
        if (!Active)
        {
            if (m_Hearte.activeInHierarchy)
            {
                m_Hearte.SetActive(false);
            }
        }
        else
        {
            if (!m_Hearte.activeInHierarchy)
            {
                m_Hearte.SetActive(true);

            }
        }
    }


    

}
