using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera : MonoBehaviour
{
    [SerializeField] private CPlayer m_Target;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField , Range(0.0f ,1.0f)] private float m_fSpeed;
    
    
    private void LateUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        if (m_Target == null)
            Debug.LogError("Player컴포넌트를 넣어 주세요");

        var resultPos = new Vector3(m_Target.transform.position.x , m_Target.g_OffsetCameraY, m_Target.transform.position.y);
        transform.position = Vector3.Lerp(transform.position , m_Target.transform.position + m_Offset, m_fSpeed);
    }

}
