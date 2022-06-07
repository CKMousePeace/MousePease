using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutTrigger : CCinemachine
{

    private void OnTriggerStay(Collider col)
    {

        if (col.CompareTag("Player"))
        {
            isZoomOut = true;
        }
     }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isZoomOut = false;
        }
    }
}
