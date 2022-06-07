using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BGMManager : MonoBehaviour
{

    private static FMOD.Studio.EventInstance Music;

    private void Start()
    {
        Music = RuntimeManager.CreateInstance("event:/Music/BGM_Level_02");
        Music.start();
        Music.release();
    }

    public void Progress(int BGM)
    {
        Music.setParameterByName("BGMSelect", BGM);
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


}