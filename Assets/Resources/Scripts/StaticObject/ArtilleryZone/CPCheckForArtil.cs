using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPCheckForArtil : MonoBehaviour
{
    //플레이어 "함정" 위에 서있는지 확인
    public bool g_isOnArtill;

    private void OnTriggerStay(Collider other)
    {
        g_isOnArtill = true;

    }

    private void OnTriggerExit(Collider other)
    {
        g_isOnArtill = false;
    }



}
