using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CSoundBankForPlayer : MonoBehaviour
{

    public static CSoundBankForPlayer instance;

    [SerializeField]
    [EventRef]
    public string g_PlayerFootStep = null;


    private void Awake()
    {
        instance = this;
    }


    public void PlayerStep()
    {
        if (g_PlayerFootStep != null)
        {
            RuntimeManager.PlayOneShot(g_PlayerFootStep);
        }
    }

    //���� ���� ������� ���� �̾ƿ���

}
