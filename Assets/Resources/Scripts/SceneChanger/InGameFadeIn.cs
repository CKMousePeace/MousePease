using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameFadeIn : MonoBehaviour
{
    //인게임에 적용 될 페이드 인
    [SerializeField] private Image m_FadeOut;

    void Start()
    {
        StartCoroutine(FadeInScene(1.0f));
    }

    IEnumerator FadeInScene(float time)
    {
        Color color = m_FadeOut.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / time;
            m_FadeOut.color = color;
            yield return null;

        }
    }
}
