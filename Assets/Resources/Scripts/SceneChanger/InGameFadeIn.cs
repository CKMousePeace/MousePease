using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameFadeIn : MonoBehaviour
{
    //인게임에 적용 될 페이드 인
    [SerializeField] private Image m_FadeOut;
    [SerializeField] private GameObject canvers;

    void Start()
    {
        StartCoroutine("FadeInScene");
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
            canvers.SetActive(false);
        }
    }

}
