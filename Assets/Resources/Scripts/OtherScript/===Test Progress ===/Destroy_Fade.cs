using System.Collections;
using UnityEngine;

public class Destroy_Fade : MonoBehaviour
{
    private Renderer m_Renderer;

    void Start()
    {
        m_Renderer = gameObject.GetComponent<Renderer>();
        StartCoroutine(FadeOff_Dest());
    }

    IEnumerator FadeOff_Dest()
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("페이드 실행");
        int i = 10;
        while (i > 0)
        {
            Debug.Log( "나는 옅어진다 : " + i);
            i -= 1;
            float f = i / 10.0f;
            Color c = m_Renderer.material.color;
            c.a = f;
            m_Renderer.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }

        if (i < 0) 
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
