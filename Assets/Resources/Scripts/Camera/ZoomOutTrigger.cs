using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutTrigger : MonoBehaviour
{
    public GameObject ZoomOut_Trigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            GameObject.Find("Camera").GetComponent<CameraTrigger>().isZoomOut = true;
        }
     }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("Camera").GetComponent<CameraTrigger>().isZoomOut = false;
        }
    }
}
