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

    //[SerializeField] private int m_Zoom = 20;
    //[SerializeField] public int m_Zoomout = 35;

    [SerializeField] private float m_Smooth = 3;

    [Header("¡‹¿Œ Fov")]
    [SerializeField] private int ZoonInFov = 5;

    [Header("¡‹æ∆øÙ Fov")]
    [SerializeField] private int ZoonOutFov = 35;

    [Header("±‚∫ª Fov")]
    [SerializeField] private int NormalFov = 22;


    void FixedUpdate()
    {
        if (isZoomed)
        {
            g_PlayerZoom.m_MinFOV = Mathf.Lerp(g_PlayerZoom.m_MinFOV, ZoonInFov, Time.deltaTime * m_Smooth);
        }

        if (!isZoomed)
        {
            g_PlayerZoom.m_MinFOV = Mathf.Lerp(g_PlayerZoom.m_MinFOV, NormalFov, Time.deltaTime * m_Smooth);
        }

        if (isZoomOut)
        {

            g_PlayerZoom.m_MinFOV = Mathf.Lerp(g_PlayerZoom.m_MinFOV, ZoonOutFov, Time.deltaTime * m_Smooth);

        }

        if (!isZoomOut)
        {
            g_PlayerZoom.m_MinFOV = Mathf.Lerp(g_PlayerZoom.m_MinFOV, NormalFov, Time.deltaTime * m_Smooth);

        }
    }

}
