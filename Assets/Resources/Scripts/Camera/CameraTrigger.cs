using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{

    [SerializeField] private int zoom = 20;
    [SerializeField] private int zoomout = -20;
    [SerializeField] private int normal = 60;
    [SerializeField] private float smooth = 5;
    [SerializeField] private int FOV = 30;

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
