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

    //위와 같은 방법으로 사운드 뽑아오기

}
