using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//FMOD 볼륨 슬라이더 구현 부분 >>VCA 사용

public class CVAController : MonoBehaviour
{

    private FMOD.Studio.VCA vca_Master;
    private FMOD.Studio.VCA vca_BGM;
    private FMOD.Studio.VCA vca_SFX;


    public string VCANAME_Master;
    public string VCANAME_BGM;
    public string VCANAME_SFX;

    private Slider m_slider;


    void Start()
    {
        vca_Master = FMODUnity.RuntimeManager.GetVCA("vca:/" + VCANAME_Master);
        m_slider = GetComponent<Slider>();

        vca_BGM = FMODUnity.RuntimeManager.GetVCA("vca:/" + VCANAME_BGM);
        vca_BGM = FMODUnity.RuntimeManager.GetVCA("vca:/" + VCANAME_SFX);

    }
    public void SetVolume_BGM(float BGM_volume)
    {
        vca_BGM.setVolume(BGM_volume);
    }

    public void SetVolume_SFX(float SFX_volume)
    {
        vca_SFX.setVolume(SFX_volume);
    }

}
