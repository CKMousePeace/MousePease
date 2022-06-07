using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CCinemachine : MonoBehaviour
{
    public bool isZoomed = false;
    public bool isZoomOut = false;

    public CinemachineVirtualCamera g_PlayerFollower;

    [SerializeField] private int m_Zoom = 20;
    [SerializeField] private int m_Zoomout = -20;

    [SerializeField] private float m_Smooth = 5;

    [SerializeField] private float m_FOV = 30;

    void FixedUpdate()
    {
        if (isZoomed)
        {
            g_PlayerFollower.m_Lens.FieldOfView = 
                Mathf.Lerp(g_PlayerFollower.m_Lens.FieldOfView, m_Zoom, Time.deltaTime * m_Smooth);

        }
        if (!isZoomed)
        {
            g_PlayerFollower.m_Lens.FieldOfView = 
                Mathf.Lerp(g_PlayerFollower.m_Lens.FieldOfView, m_FOV, Time.deltaTime * m_Smooth);
        }

        if (isZoomOut)
        {
            g_PlayerFollower.m_Lens.FieldOfView = 
                Mathf.Lerp(g_PlayerFollower.m_Lens.FieldOfView, m_Zoomout, Time.deltaTime * m_Smooth);

        }
        if (!isZoomOut)
        {
            g_PlayerFollower.m_Lens.FieldOfView = 
                Mathf.Lerp(g_PlayerFollower.m_Lens.FieldOfView, m_FOV, Time.deltaTime * m_Smooth);
        }

    }


}
