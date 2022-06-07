using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CBossTrigger : MonoBehaviour
{

    [SerializeField] private Image m_FadeOut;
    [SerializeField] private GameObject FadeCover;

    private bool isCheck = false;


    private void Update()
    {
        StartCoroutine(DelayStart());
    }


    private void OnTriggerEnter(Collider col)
    {
        if (isCheck == true)
        {
            FadeCover.SetActive(true);
            StartCoroutine(FadeOutBoss());
        }
        else
            return;
    }


    IEnumerator FadeOutBoss()
    {
        FadeCover.SetActive(true);
        float FadeOutCount = 0;
        while (FadeOutCount < 1.0f)
        {
            FadeOutCount += 0.03f;
            yield return new WaitForSeconds(0.01f);
            m_FadeOut.color = new Color(0, 0, 0, FadeOutCount);
        }
        SceneManager.LoadScene("Tutorial");

    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(4.0f);
        isCheck = true;
    }

}
