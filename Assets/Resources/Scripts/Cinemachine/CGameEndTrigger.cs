using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CGameEndTrigger : MonoBehaviour
{
    [Header("���� ���� ��ũ��Ʈ")]

    //�ΰ��ӿ� ���� �� ���̵� �ƿ�
    [SerializeField] private Image m_FadeOut;


    [Header("����� ��ο����� �ӵ�")]
    [SerializeField] private float m_FadeSpeed = 1.0f;

    [Header("================")]
    //���� ���� �� �����ִ� To be continued
    [SerializeField] private Image m_BG;
    [SerializeField] private Image m_ToBeContinu;
    [SerializeField] private GameObject m_ToBeCon;


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.GameStopEvent();
            StartCoroutine(FadeOutScene(m_FadeSpeed));
        }
    }

    IEnumerator FadeOutScene(float time)
    {
        Color color = m_FadeOut.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / time;
            m_FadeOut.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        StartCoroutine(FadeOutToBeContinued(1.0f));

    }


    IEnumerator FadeOutToBeContinued(float time)
    {

        m_ToBeCon.SetActive(true);
        Color color = m_BG.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / time;
            m_BG.color = color;
            m_ToBeContinu.color = color;
            yield return null;
        }


        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeInToBeContinued(1.0f));
    }



    IEnumerator FadeInToBeContinued(float time)
    {
        Color color = m_BG.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / time;
            m_BG.color = color;
            m_ToBeContinu.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("TitleScene");
    }

}
