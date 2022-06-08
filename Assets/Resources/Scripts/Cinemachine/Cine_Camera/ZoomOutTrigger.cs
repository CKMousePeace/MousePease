using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutTrigger : MonoBehaviour
{

    [SerializeField] private GameObject CCin;
    private void Start()
    {
        if(CCin == null)
            CCin = GameObject.Find("CMTuto vcam");
    }

    private void OnTriggerStay(Collider col)
    {

        if (col.CompareTag("Player"))
        {
            CCin.GetComponent<CCinemachine>().isZoomOut = true;
        }
     }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            CCin.GetComponent<CCinemachine>().isZoomOut = false;
        }
    }
}
