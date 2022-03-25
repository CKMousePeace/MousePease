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

    //ī�޶� ��ȹ�� �°� ���� �ؾߵ�
    private void CameraMovement()
    {
        if (m_Target == null)
            Debug.LogError("Player������Ʈ�� �־� �ּ���");

        var TargetPos = m_Target.transform.position;        

        //ī�޶� �ִ� X���� ���� ���� �ڷδ� �̵����� �ʰ� ����!
        if (m_StartPosX < TargetPos.x && m_EndPosX > TargetPos.x)
        {
            var CurrentPosX = Mathf.Lerp(transform.position.x, TargetPos.x, m_fSpeed);
            transform.position = new Vector3(CurrentPosX, transform.position.y, transform.position.z);
        }


        //ī�޶� y�� ����
        var PlimitPosY = ((transform.position.y) + m_CameraLimitY * 0.5f) ; 
        var MlimitPosY = ((transform.position.y) - m_CameraLimitY * 0.5f) ;

        
        // ���� ���� ��ġ ���� �÷��̾ ���� ��
        if (MlimitPosY > TargetPos.y)
        {
            var PosY = TargetPos.y - MlimitPosY;
            transform.position += new Vector3(0.0f, PosY, 0.0f) * Time.deltaTime * 4 * m_fSpeed;
        }
        // ���� �Ʒ� ��ġ ���� �÷��̾ ���� ��
        else if (PlimitPosY < TargetPos.y)
        {
            var PosY = TargetPos.y - PlimitPosY;
            transform.position += new Vector3(0.0f, PosY, 0.0f) * Time.deltaTime * 4 * m_fSpeed;
        }


    }

}
