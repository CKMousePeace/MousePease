using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class UISoundManager : MonoBehaviour
{

    private FMOD.Studio.EventInstance UI_Button_1;
    private FMOD.Studio.EventInstance UI_Button_2;
    private FMOD.Studio.EventInstance UI_Button_3;


    public void PlayPressUI_1()
    {
        UI_Button_1 = RuntimeManager.CreateInstance("event:/UI/UI_Button_1");
        UI_Button_1.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        UI_Button_1.start();
        UI_Button_1.release();
    }

    public void PlayPressUI_2()
    {
        UI_Button_2 = RuntimeManager.CreateInstance("event:/UI/UI_Button_2");
        UI_Button_2.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        UI_Button_2.start();
        UI_Button_2.release();
    }

    public void PlayPressUI_3()
    {
        UI_Button_3 = RuntimeManager.CreateInstance("event:/UI/UI_Button_3");
        UI_Button_3.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        UI_Button_3.start();
        UI_Button_3.release();
    }

}
