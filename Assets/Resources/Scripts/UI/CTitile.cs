using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CTitile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image m_PlayImage;
    [SerializeField] private Image m_ExitImage;
    [SerializeField] private Button m_PlayButton;
    [SerializeField] private Button m_ExitButton;

    [SerializeField] private Image m_LeftSelectImage;
    [SerializeField] private Image m_RightSelectImage;


    private Rect m_ImageRect;
    private Rect m_ExitRect;
    private int m_SelectTitle;


    private void Start()
    {
        m_ImageRect = m_PlayImage.rectTransform.rect;
        m_ExitRect = m_ExitImage.rectTransform.rect;

        m_ImageRect.center = m_PlayImage.rectTransform.position;
        m_ExitRect.center = m_ExitImage.rectTransform.position;

        m_SelectTitle = 1;

    }
    // Update is called once per frame
    void Update()
    {
        if (m_ImageRect.Contains(Input.mousePosition))
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
            m_LeftSelectImage.rectTransform.position = m_PlayImage.rectTransform.position;
            m_LeftSelectImage.rectTransform.position -= new Vector3(m_PlayImage.rectTransform.rect.width * 0.5f + 70, 0.0f, 0.0f);

            m_RightSelectImage.rectTransform.position = m_PlayImage.rectTransform.position;
            m_RightSelectImage.rectTransform.position += new Vector3(m_PlayImage.rectTransform.rect.width * 0.5f + 70, 0.0f, 0.0f);
        }
        else if (m_SelectTitle == 0)
        {
            m_LeftSelectImage.rectTransform.position = m_ExitImage.rectTransform.position;
            m_LeftSelectImage.rectTransform.position -= new Vector3(m_ExitImage.rectTransform.rect.width * 0.5f + 70, 0.0f, 0.0f);

            m_RightSelectImage.rectTransform.position = m_ExitImage.rectTransform.position;
            m_RightSelectImage.rectTransform.position += new Vector3(m_ExitImage.rectTransform.rect.width * 0.5f + 70, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (m_SelectTitle == 1)
            {
                GameStartEvent();
            }
            else
            {
                GameExitEvent();
            }
        }
    }

    public void GameStartEvent()
    {
        SceneManager.LoadScene(1);
    }

    public void GameExitEvent()
    {
        Application.Quit();
    }
}
