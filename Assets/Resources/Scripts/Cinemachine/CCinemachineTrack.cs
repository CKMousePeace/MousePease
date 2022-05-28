using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CCinemachineTrack : MonoBehaviour
{


    [SerializeField] private CinemachineSmoothPath smmothPath;
    private CinemachineVirtualCamera VirtualCamera;
    private CinemachineTrackedDolly TrackDolly;
    private float m_Length = 0.0f;   
    
    private void Awake()
    {

        
        VirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        TrackDolly = VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        for (int i = 0; i < smmothPath.m_Waypoints.Length; i++)
        {
            CinemachineSmoothPath.Waypoint point = (CinemachineSmoothPath.Waypoint)smmothPath.m_Waypoints.GetValue(i);
            if (m_Length < smmothPath.m_Waypoints[i].position.x)
            {
                m_Length = smmothPath.m_Waypoints[i].position.x;
            }
        }
        
    }

    private void Update()
    {
        float PointLen = smmothPath.m_Waypoints.Length;
        var followTrans = VirtualCamera.Follow;
        float m_Data = (followTrans.position.x / m_Length) * (PointLen - 1.0f);
        TrackDolly.m_PathPosition = m_Data ;
    }




}
