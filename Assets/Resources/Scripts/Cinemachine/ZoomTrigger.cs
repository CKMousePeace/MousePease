using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTrigger : CCinemachine
{

    private void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            isZoomed = true;
        }
      }

    private void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            isZoomed = false;
        }

    }
}
