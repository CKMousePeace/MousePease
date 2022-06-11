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
    private int m_SelectTitle;


    

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }


    // Start is called before the first frame update
    private void Start()
    {
        m_ResumeRect = m_ReSumeImage.rectTransform.rect;
        m_ExitRect = m_ExitImage.rectTransform.rect;

        m_ResumeRect.center = m_ReSumeImage.rectTransform.position;
        m_ExitRect.center = m_ExitImage.rectTransform.position;

        m_SelectTitle = 1;
    }

    // Update is called once per frame
    void Update()
    {

        var width = Screen.width / 1920.0f;
        
        var RectReSumeWidth = m_ReSumeImage.rectTransform.rect.width * 0.5f;
        var RectExitWidth = m_ExitImage.rectTransform.rect.width * 0.5f;

        if (m_ResumeRect.Contains(Input.mousePosition))
        {
            m_SelectTitle = 1;

        }

        else if (m_ExitRect.Contains(Input.mousePosition))
        {
            m_SelectTitle = 0;

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                m_SelectTitle--;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_SelectTitle++;
            }

            m_SelectTitle = Mathf.Clamp(m_SelectTitle, 0, 1);

        }


        if (m_SelectTitle == 1)
        {
            m_LeftSelectImage.rectTransform.position = m_ReSumeImage.rectTransform.position;
            m_LeftSelectImage.rectTransform.position -= new Vector3((RectReSumeWidth + 70.0f) * width, 0.0f, 0.0f);

            m_RightSelectImage.rectTransform.position = m_ReSumeImage.rectTransform.position;
            m_RightSelectImage.rectTransform.position += new Vector3((RectReSumeWidth + 70.0f) * width, 0.0f, 0.0f);
        }
        else if (m_SelectTitle == 0)
        {
            m_LeftSelectImage.rectTransform.position = m_ExitImage.rectTransform.position;
            m_LeftSelectImage.rectTransform.position -= new Vector3((RectExitWidth + 70.0f) * width, 0.0f, 0.0f);

            m_RightSelectImage.rectTransform.position = m_ExitImage.rectTransform.position;
            m_RightSelectImage.rectTransform.position += new Vector3((RectExitWidth + 70.0f) * width, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (m_SelectTitle == 1)
            {
                CancelPause();
            }
            else
            {
                Exit();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelPause(); 
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


