using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CSoundBankFor_ : MonoBehaviour
{

    public static CSoundBankFor_ instance;

    [SerializeField]
    [EventRef]
    public string g_SomeThingSound = null;

    public void YesIamTestBoi()
    {
        if (g_SomeThingSound != null)
        {

            RuntimeManager.PlayOneShot(g_SomeThingSound);
        }
    }

    //위와 같은 방법으로 사운드 뽑아오기

}
