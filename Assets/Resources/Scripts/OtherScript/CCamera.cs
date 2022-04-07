using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
public class CCamera : MonoBehaviour
{
    [SerializeField] private CPlayer m_Target;
    [SerializeField] private Vector3 m_Offset;


    [SerializeField] private float m_CameraLimitY;    
    [Header("카메라는 이 사이에서만 움직입니다.")]
    [Header("첫번 째 변수는 작은 값 두번 째 변수는 큰 값입니다.")]
    [SerializeField] private Vector2 m_RestrictionX;

    private float m_BeforeResultX;




    [SerializeField, Range(0.0f, 1.0f)] private float m_fSpeed;

    private void LateUpdate()
    {
        CameraMovement();
    }

    //카메라 기획에 맞게 수정 해야됨
    private void CameraMovement()
    {
        if (m_Target == null)
        {
            Debug.LogError("Player컴포넌트를 넣어 주세요");
            return;
        }
        
        var TargetPos = m_Target.transform.position + m_Offset;
        
        float ResultY = TargetPos.y;
        if (TargetPos.x >= m_RestrictionX.x && TargetPos.x <= m_RestrictionX.y)
            m_BeforeResultX = TargetPos.x;

        Vector3 resultVec = new Vector3(m_BeforeResultX, ResultY , TargetPos.z);
        transform.position = resultVec;
    }

}
