using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{

    public int zoom = 20;
    public int zoomout = -20;
    public int normal = 60;
    public float smooth = 5;
    public int FOV = 30;

    public bool isZoomed = false;
    public bool isZoomOut = false;
    public Camera zoomcamera;



    // Start is called before the first frame update
    void Start()
    {
        zoomcamera = GameObject.Find("Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if(isZoomed)
        {
            GameObject.Find("Camera").GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);

        }
        if(!isZoomed)
        {
            GameObject.Find("Camera").GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, FOV, Time.deltaTime * smooth);
        }

        if(isZoomOut)
        {
            GameObject.Find("Camera").GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoomout, Time.deltaTime * smooth);

        }
        if(!isZoomOut)
        {
            GameObject.Find("Camera").GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, FOV, Time.deltaTime * smooth);
        }

    }


}
