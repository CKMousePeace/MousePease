using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
public class CCamera : MonoBehaviour
{
    [SerializeField] private CPlayer m_Target;
    [SerializeField] private Vector3 m_Offset;


    [SerializeField] private float m_CameraLimitY;    
    [Header("ī�޶�� �� ���̿����� �����Դϴ�.")]
    [Header("ù�� ° ������ ���� �� �ι� ° ������ ū ���Դϴ�.")]
    [SerializeField] private Vector2 m_RestrictionX;

    private float m_BeforeResultX;




    [SerializeField, Range(0.0f, 1.0f)] private float m_fSpeed;

    private void LateUpdate()
    {
        CameraMovement();
    }

    //ī�޶� ��ȹ�� �°� ���� �ؾߵ�
    private void CameraMovement()
    {
        if (m_Target == null)
        {
            Debug.LogError("Player������Ʈ�� �־� �ּ���");
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
