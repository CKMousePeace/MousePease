using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CGameEndTrigger : MonoBehaviour
{
    //�ΰ��ӿ� ���� �� ���̵� �ƿ�
    [SerializeField] private Image m_FadeOut;
    [SerializeField] private GameObject FadeCover;

    [Header("��ο����� �ӵ�")]
    [SerializeField] private float m_FadeSpeed = 0.01f;


    private void OnTriggerEnter(Collider col)
    {
        
        StartCoroutine(FadeOutScene(m_FadeSpeed));
    }

    IEnumerator FadeOutScene(float speed)
    {
        FadeCover.SetActive(true);
        float FadeOutCount = 0;
        while (FadeOutCount < 1.0f)
        {
            FadeOutCount += speed;
            yield return new WaitForSeconds(0.01f);
            m_FadeOut.color = new Color(0, 0, 0, FadeOutCount);
        }
        SceneManager.LoadScene("TitleScene");
    }

}
