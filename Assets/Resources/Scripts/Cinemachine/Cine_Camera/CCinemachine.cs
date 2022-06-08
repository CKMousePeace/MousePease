using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CCinemachine : MonoBehaviour
{
    public bool isZoomed = false;
    public bool isZoomOut = false;

    public CinemachineVirtualCamera g_PlayerFollower;
    public CinemachineFollowZoom g_PlayerZoom;

    [SerializeField] private int m_Zoom = 20;
    [SerializeField] public int m_Zoomout = 35;

    [SerializeField] private float m_Smooth = 3;

    [SerializeField] private int MinFov = 10;
    [SerializeField] private int MaxFov = 35;


    void FixedUpdate()
    {
        if (isZoomed)
        {
            g_PlayerZoom.m_MaxFOV = Mathf.Lerp(g_PlayerZoom.m_MaxFOV, m_Zoom, Time.deltaTime * m_Smooth);
        }

        if (!isZoomed)
        {
            g_PlayerZoom.m_MaxFOV = MaxFov;
        }

        if (isZoomOut)
        {

            g_PlayerZoom.m_MinFOV = Mathf.Lerp(g_PlayerZoom.m_MinFOV, m_Zoomout, Time.deltaTime * m_Smooth);

        }

        if (!isZoomOut)
        {
            //g_PlayerZoom.m_Damping = m_Smooth;

            g_PlayerZoom.m_MinFOV = MinFov;
            //g_PlayerFollower.m_Lens.FieldOfView = 
            //    Mathf.Lerp(g_PlayerFollower.m_Lens.FieldOfView, m_FOV, Time.deltaTime * m_Smooth);
        }
    }

}
