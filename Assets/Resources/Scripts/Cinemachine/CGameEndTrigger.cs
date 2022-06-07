using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CGameEndTrigger : MonoBehaviour
{
    //�ΰ��ӿ� ���� �� ���̵� �ƿ�
    [SerializeField] private Image m_FadeOut;
    [SerializeField] private GameObject FadeCover;


    private void OnTriggerEnter(Collider col)
    {
        FadeCover.SetActive(true);
        StartCoroutine(FadeOutScene());
    }

    IEnumerator FadeOutScene()
    {
        float FadeOutCount = 0;
        while (FadeOutCount < 1.0f)
        {
            FadeOutCount += 0.03f;
            yield return new WaitForSeconds(0.01f);
            m_FadeOut.color = new Color(0, 0, 0, FadeOutCount);
        }
        SceneManager.LoadScene("TitleScene");
    }

}
