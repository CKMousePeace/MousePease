using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BossSoundManager_Test : MonoBehaviour
{

    public static BossSoundManager instance;

    [SerializeField] EventReference g_BossWalk;



    public void BossWalking()
    {

        RuntimeManager.PlayOneShot(g_BossWalk);
    }


}
