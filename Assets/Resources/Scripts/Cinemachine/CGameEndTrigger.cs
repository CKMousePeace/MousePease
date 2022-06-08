using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CGameEndTrigger : MonoBehaviour
{
    [Header("게임 종료 스크립트")]

    //인게임에 적용 될 페이드 아웃
    [SerializeField] private Image m_FadeOut;
    [SerializeField] private GameObject FadeCover;

    [Header("어두워지는 속도")]
    [SerializeField] private float m_FadeSpeed = 0.01f;


    private void OnTriggerEnter(Collider col)
    {
        GameManager.GameStopEvent();
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

        if(FadeOutCount > 1.0f)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
