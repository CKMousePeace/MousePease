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

    //���� ���� ������� ���� �̾ƿ���

}
