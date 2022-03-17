using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera : MonoBehaviour
{
    [SerializeField] private CPlayer m_Target;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private float m_StartPosX;
    [SerializeField] private float m_EndPosX;
    [SerializeField, Range(0.0f, 1.0f)] private float m_fSpeed;



    private void LateUpdate()
    {
        CameraMovement();
    }

    //카메라 기획에 맞게 수정 해야됨
    private void CameraMovement()
    {
        if (m_Target == null)
            Debug.LogError("Player컴포넌트를 넣어 주세요");

        if (m_StartPosX < m_Target.transform.position.x && m_Target.transform.position.x < m_EndPosX)
        {
            var resultPos = new Vector3(m_Target.transform.position.x, m_Target.g_OffsetCameraY, m_Target.transform.position.y);
            transform.position = Vector3.Lerp(transform.position, m_Target.transform.position + m_Offset, m_fSpeed);
        }

    }

}
