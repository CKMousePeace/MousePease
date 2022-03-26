using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera : MonoBehaviour
{
    [SerializeField] private CPlayer m_Target;
    [SerializeField] private Vector3 m_Offset;

    [SerializeField] private float m_StartPosX;
    [SerializeField] private float m_EndPosX;
    [SerializeField] private float m_CameraLimitY;
    [SerializeField] private float m_CameraPosYSpeed;
    

    [SerializeField, Range(0.0f, 1.0f)] private float m_fSpeed;



    private void Start()
    {
        transform.position = m_Offset;
    }
    private void LateUpdate()
    {
        CameraMovement();
    }

    //카메라 기획에 맞게 수정 해야됨
    private void CameraMovement()
    {
        if (m_Target == null)
            Debug.LogError("Player컴포넌트를 넣어 주세요");

        var TargetPos = m_Target.transform.position;        

        //카메라 최대 X값과 최저 값을 뒤로는 이동하지 않게 만듦!
        if (m_StartPosX < TargetPos.x && m_EndPosX > TargetPos.x)
        {
            var CurrentPosX = Mathf.Lerp(transform.position.x, TargetPos.x, m_fSpeed);
            transform.position = new Vector3(CurrentPosX, transform.position.y, transform.position.z);
        }


        //카메라 y값 조정
        var PlimitPosY = ((transform.position.y) + m_CameraLimitY * 0.5f) ; 
        var MlimitPosY = ((transform.position.y) - m_CameraLimitY * 0.5f) ;

        
        // 가장 높은 위치 보다 플레이어가 높을 때
        if (MlimitPosY > TargetPos.y)
        {
            var PosY = TargetPos.y - MlimitPosY;
            transform.position += new Vector3(0.0f, PosY, 0.0f) * Time.deltaTime * 4 * m_fSpeed;
        }
        // 가장 아래 위치 보다 플레이어가 낮을 때
        else if (PlimitPosY < TargetPos.y)
        {
            var PosY = TargetPos.y - PlimitPosY;
            transform.position += new Vector3(0.0f, PosY, 0.0f) * Time.deltaTime * 4 * m_fSpeed;
        }


    }

}
