using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CCamera : MonoBehaviour
{
    [SerializeField] private Transform m_Focus;    
    [SerializeField , Min(0.0f)] private float m_Radius;    

    private Vector3 m_FocusPoint;
    private Vector3 m_StartPoint;
    private float m_FocusCentering = 0.5f;
    private Vector3 m_Offset;


    private float m_ShakeTime = 0.0f;
    private float m_currentShakeTime = 0.0f;
    private float m_Amount = 0.0f;
    

    private void Awake()
    {
        m_Offset = transform.position;
        m_FocusPoint = m_Focus.position;
        m_StartPoint = m_Focus.position;
    }
    

    private void LateUpdate()
    {
        UpdateFocusPoint();


        var ShakeVector = Vector3.zero;
        if (m_currentShakeTime < m_ShakeTime)
            ShakeVector = Random.insideUnitSphere;


        if (Input.GetKeyDown(KeyCode.V))
        {
            SetShakeInfo(0.2f, 0.2f);
        }

        m_currentShakeTime += Time.deltaTime;

        transform.position = (ShakeVector * m_Amount) + m_FocusPoint + m_Offset - m_StartPoint;
    }


    //카메라 기획에 맞게 수정 해야됨
    private void UpdateFocusPoint()
    {
        var TargetPos = m_Focus.position;



        if (m_Radius > 0.0f)
        {
            float distance = Vector3.Distance(TargetPos, m_FocusPoint);
            float t = 0.1f;
            if (distance > 0.01f && m_FocusCentering > 0.0f)
            {
                t = Mathf.Pow(1.0f - m_FocusCentering, Time.unscaledDeltaTime);               
            }
            if (distance > m_Radius)
            {
                t = Mathf.Min(t, m_Radius / distance);
            }           

            m_FocusPoint = Vector3.Lerp(TargetPos, m_FocusPoint, t);
        }
        else
        {
            m_FocusPoint = TargetPos;
        }


    }

    public void SetShakeInfo(float ShakeTime, float Amount)
    {
        m_ShakeTime = ShakeTime;
        m_currentShakeTime = 0.0f;
        m_Amount = Amount;
    }

}
