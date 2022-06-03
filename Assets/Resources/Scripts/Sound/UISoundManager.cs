using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class UISoundManager : MonoBehaviour
{

    private FMOD.Studio.EventInstance UITest;



    public void PlayPressUI()
    {
        UITest = RuntimeManager.CreateInstance("event:/UI");
        UITest.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        UITest.start();
        UITest.release();
    }


}
