using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CPause : MonoBehaviour
{
    [SerializeField] private Image m_ReSumeImage;
    [SerializeField] private Image m_ExitImage;
    [SerializeField] private Button m_ReSumeButton;
    [SerializeField] private Button m_ExitButton;

    [SerializeField] private Image m_LeftSelectImage;
    [SerializeField] private Image m_RightSelectImage;


    [SerializeField] private GameObject m_InGameUI;
    private Rect m_ResumeRect;
    private Rect m_ExitRect;



    // Start is called before the first frame update
    void Start()
    {
        m_ResumeRect = m_ReSumeImage.rectTransform.rect;
        m_ExitRect = m_ExitImage.rectTransform.rect;

        m_ResumeRect.center = m_ReSumeImage.rectTransform.position;
        m_ExitRect.center = m_ExitImage.rectTransform.position;

        m_LeftSelectImage.rectTransform.position = m_ReSumeImage.rectTransform.position;
        m_LeftSelectImage.rectTransform.position -= new Vector3(m_ReSumeImage.rectTransform.rect.width * 0.5f + 70, 0.0f, 0.0f);


        m_RightSelectImage.rectTransform.position = m_ReSumeImage.rectTransform.position;
        m_RightSelectImage.rectTransform.position += new Vector3(m_ReSumeImage.rectTransform.rect.width * 0.5f + 70, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ResumeRect.Contains(Input.mousePosition))
        {
            m_LeftSelectImage.rectTransform.position = m_ReSumeImage.rectTransform.position;
            m_LeftSelectImage.rectTransform.position -= new Vector3(m_ReSumeImage.rectTransform.rect.width * 0.5f + 70, 0.0f, 0.0f);

            m_RightSelectImage.rectTransform.position = m_ReSumeImage.rectTransform.position;
            m_RightSelectImage.rectTransform.position += new Vector3(m_ReSumeImage.rectTransform.rect.width * 0.5f + 70, 0.0f, 0.0f);

        }

        else if (m_ExitRect.Contains(Input.mousePosition))
        {

            m_LeftSelectImage.rectTransform.position = m_ExitImage.rectTransform.position;
            m_LeftSelectImage.rectTransform.position -= new Vector3(m_ExitImage.rectTransform.rect.width * 0.5f + 70, 0.0f, 0.0f);

            m_RightSelectImage.rectTransform.position = m_ExitImage.rectTransform.position;
            m_RightSelectImage.rectTransform.position += new Vector3(m_ExitImage.rectTransform.rect.width * 0.5f + 70, 0.0f, 0.0f);
        }
    }

    public void CancelPause()
    {
        m_InGameUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
