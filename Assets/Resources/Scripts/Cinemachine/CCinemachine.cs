using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CCinemachine : MonoBehaviour
{
    public bool isZoomed = false;
    public bool isZoomOut = false;

    public CinemachineVirtualCamera g_PlayerFollower;

    [SerializeField] private int zoom = 20;
    [SerializeField] private int zoomout = -20;

    [SerializeField] private float smooth = 5;

    [SerializeField] private float m_FOV = 30;

    void Update()
    {
        if (isZoomed)
        {
            g_PlayerFollower.m_Lens.FieldOfView = Mathf.Lerp(g_PlayerFollower.m_Lens.FieldOfView, zoom, Time.deltaTime * smooth);

        }
        if (!isZoomed)
        {
            g_PlayerFollower.m_Lens.FieldOfView = Mathf.Lerp(g_PlayerFollower.m_Lens.FieldOfView, m_FOV, Time.deltaTime * smooth);
        }

        if (isZoomOut)
        {
            g_PlayerFollower.m_Lens.FieldOfView = Mathf.Lerp(g_PlayerFollower.m_Lens.FieldOfView, zoomout, Time.deltaTime * smooth);

        }
        if (!isZoomOut)
        {
            g_PlayerFollower.m_Lens.FieldOfView = Mathf.Lerp(g_PlayerFollower.m_Lens.FieldOfView, m_FOV, Time.deltaTime * smooth);
        }

    }


}
