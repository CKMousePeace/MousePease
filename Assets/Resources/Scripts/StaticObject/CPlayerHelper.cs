using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerHelper : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerStatic;

    private void OnTriggerEnter(Collider col)
    {
        m_PlayerStatic.SetActive(true);

        Destroy(gameObject);
    }



}


