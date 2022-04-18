using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameFadeIn : MonoBehaviour
{

    [SerializeField] private Image m_FadeOut;

    void Start()
    {
        StartCoroutine("FadeInScene");
    }


    void Update()
    {
        
    }


    IEnumerator FadeInScene()
    {

        float FadeInCount = 1;
        while (FadeInCount > 0)
        {
            FadeInCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            m_FadeOut.color = new Color(0, 0, 0, FadeInCount);
        }

        if (FadeInCount < 1.0f)
        {
            Destroy(gameObject);
        }
    }

}
