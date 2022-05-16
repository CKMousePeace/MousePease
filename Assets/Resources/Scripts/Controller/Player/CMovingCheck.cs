using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMovingCheck : MonoBehaviour
{
    [SerializeField] private CPlayer m_Player;

    private void OnTriggerStay(Collider other)
    {
        m_Player.g_MoveCheck = true;
    }
    private void OnTriggerExit(Collider other)
    {
        m_Player.g_MoveCheck = false;
    }

}
