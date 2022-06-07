using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CGameEndTrigger : MonoBehaviour
{
    //인게임에 적용 될 페이드 아웃
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
