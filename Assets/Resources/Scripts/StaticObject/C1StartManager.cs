using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;

public class C1StartManager : MonoBehaviour
{
    [SerializeField] private GameObject BossControll;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            BossControll.GetComponent<CBossController>().BossIntroStart();
            Destroy(gameObject);
        }
    }
}
