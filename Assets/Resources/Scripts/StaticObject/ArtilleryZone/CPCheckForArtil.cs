using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPCheckForArtil : MonoBehaviour
{
    //�÷��̾� "����" ���� ���ִ��� Ȯ��
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
