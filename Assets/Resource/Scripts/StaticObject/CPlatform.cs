using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�̵� Ȥ�� Destroy ������ �÷���(����)

public class CPlatform : MonoBehaviour
{
    public enum PlatformType
    {
        MovementLnR = 0,        //Movement Left and Right.                                                  �� �� �̵� �÷���
        MovementUnD = 1,        //Movement Up and Down.                                                     �� �Ʒ� �̵� �÷���
        SelfDestroy = 2,        //If the player goes up, it will be destroyed after a set period of time.   �÷��̾ �ö� ��� ���� �ð� �Ŀ� �ı�
    }
    [Header("�÷��� Ÿ�� ����")]
    [SerializeField] private PlatformType m_plattype;      //m_plattype << This part is determines the platform type.                           �÷��� ���� ����. ���� - ������ / ��-�Ʒ� / ������ �ı�
    [Header("�÷��� �̵� ��� ǥ��")]
    [SerializeField] private bool m_DebugLine = true;      //m_DebugLine << This part is debugs where the platform will go.                     ���� �̵����� ��¦ �����ִ� ����׶���
    [SerializeField] private Vector3[] m_MovePoint;        //m_MovePoint << Relative coordinates to move relative to the previous coordinates.  ���� ��ǥ�� �������� ������ ��� ��ǥ


    [SerializeField] private float m_MoveSpeedLnR = 1.5f;  //m_MoveSpeedLnR << This part is show and edit Left and Right movement of the platform speed.  �� �� �÷��� �ӵ� 
    [SerializeField] private float m_MoveSpeedUnD = 1.5f;  //m_MoveSpeedUnD << This part is show and edit Up and Down movement of the platform speed.     �� �Ʒ� �÷��� �ӵ� 
    [SerializeField] private float m_WaitTime = 0.5f;      //m_WaitTime << This part is ecide how long will wait after arrival.                           �÷����� �������� ���� �� �󸶸�ŭ�� ��� �ð��� ���� �� ���� �ð� ���ϱ�

    [SerializeField] private float m_DestroyTime = 2.0f;   //m_DestroyTime << This part is measures time to selfdestroy.                         ������ �ı��ϴ� �÷��� ���� Destroy���� �ð� ���ϱ�
    [SerializeField] private float m_Regeneration = 2.0f;  //m_Regeneration << This part is how long after a destroyed platform will respawn.    �÷����� �ı��� �� �� ��ŭ�� �ð� �Ŀ� ����� �� ������.

    [Header("���� ���۽� �ٷ� ������ �����ϰ�����. true�� �ٷ� ������")]
    public bool awakeStart = true;
    [Header("���� ���� ���� ó�� �����̱���� ��� �ð�")]
    public float awakeWaitTime = 0;


    private int m_cur = 1;                //This part is Path number for the platform to go.                                        ���� ������ ��� ��ȣ
    private bool m_Moveback = false;      //This part is Make sure you are on your way back after arriving at your destination.     ���� �������� ��� �ǵ��� ���� ������
    private bool m_MovingOn = false;      //m_MovingOn << Make sure the platform is moving.                                         �÷����� �̵�������
    private bool m_playerCheck = false;   //playerCheck << This part is make sure the player is on top.                             �÷��̾ �ö�� �ִ��� Ȯ��

    private Vector3[] m_Pos;                      //m_MovePoint���� ���� ��ȯ�� ���� ������ǥ�� ������ �ִ� �迭
    private Vector3 m_firstPos = Vector3.zero;    //OnDrawGizmos���� ���. ���� ���� ���¸� �ľ��ϰ� �ʱ� ��ǥ�� ����

    private void Awake()
    {
        if(m_MovePoint.Length <= 0)
        {
            //If m_MovePoint << this thing is noting. just destroy. �̵� ��� ���ٸ� ��ũ��Ʈ�� ����
            Destroy(this);
            return;

        }
    }









    private void Respawn()  //����� �ʱ�ȭ. ��� ���� �ʱ�ȭ ��Ű�� �ٽ� Ȱ��ȭ ��Ų��.
    {
        m_cur = 1;
        transform.position = m_firstPos;
        m_Moveback = false;
        m_MovingOn = false;
        m_playerCheck = false;
        this.enabled = true;
        this.gameObject.SetActive(true);
    }



    protected void Start()
    {

    }


    protected void Update()
    {
        
    }


}
