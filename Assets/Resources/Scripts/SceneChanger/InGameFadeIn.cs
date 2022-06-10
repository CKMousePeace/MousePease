using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameFadeIn : MonoBehaviour
{
    //�ΰ��ӿ� ���� �� ���̵� ��
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
