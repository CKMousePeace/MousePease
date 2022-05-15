using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CSceneChanger : MonoBehaviour
{

    public Image BLackImage;

    public void FadeOut()
    {
        StartCoroutine("FadeOutScene");
    }


    public void ChangeScene()
    {
        SceneManager.LoadScene("InGame_SetTrap");
    }


    IEnumerator FadeOutScene()
    {

        float FadeOutCount = 0;
        while (FadeOutCount < 1.0f)
        {
            FadeOutCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            BLackImage.color = new Color(0, 0, 0, FadeOutCount);
        }
    }
    



    public void SceneChange_1()
    {
        SceneManager.LoadScene("Mapping_dig_01");
    }

    public void SceneChange_2()
    {
        SceneManager.LoadScene("Mapping_dig_02");
    }

    public void SceneChange_3()
    {
        SceneManager.LoadScene("Mapping_gliding_01");
    }

    public void SceneChange_4()
    {
        SceneManager.LoadScene("Mapping_gliding_02");
    }


}
