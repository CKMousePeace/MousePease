using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이동 혹은 Destroy 가능한 플랫폼(발판)

public class CPlatform : MonoBehaviour
{
    public enum PlatformType
    {
        MovementLnR = 0,        //Movement Left and Right.                                                  좌 우 이동 플랫폼
        MovementUnD = 1,        //Movement Up and Down.                                                     위 아래 이동 플랫폼
        SelfDestroy = 2,        //If the player goes up, it will be destroyed after a set period of time.   플레이어가 올라갈 경우 일정 시간 후에 파괴
    }
    [Header("플랫폼 타입 결정")]
    [SerializeField] private PlatformType m_plattype;      //m_plattype << This part is determines the platform type.                           플랫폼 유형 결정. 왼쪽 - 오른쪽 / 위-아래 / 스스로 파괴
    [Header("플랫폼 이동 경로 표시")]
    [SerializeField] private bool m_DebugLine = true;      //m_DebugLine << This part is debugs where the platform will go.                     어디로 이동할지 살짝 보여주는 디버그라인
    [SerializeField] private Vector3[] m_MovePoint;        //m_MovePoint << Relative coordinates to move relative to the previous coordinates.  이전 좌표를 기준으로 움직일 상대 좌표


    [SerializeField] private float m_MoveSpeedLnR = 1.5f;  //m_MoveSpeedLnR << This part is show and edit Left and Right movement of the platform speed.  좌 우 플랫폼 속도 
    [SerializeField] private float m_MoveSpeedUnD = 1.5f;  //m_MoveSpeedUnD << This part is show and edit Up and Down movement of the platform speed.     위 아래 플랫폼 속도 
    [SerializeField] private float m_WaitTime = 0.5f;      //m_WaitTime << This part is ecide how long will wait after arrival.                           플랫폼이 목적지에 도착 후 얼마만큼의 대기 시간을 가질 것 인지 시간 정하기

    [SerializeField] private float m_DestroyTime = 2.0f;   //m_DestroyTime << This part is measures time to selfdestroy.                         스스로 파괴하는 플랫폼 언제 Destroy할지 시간 정하기
    [SerializeField] private float m_Regeneration = 2.0f;  //m_Regeneration << This part is how long after a destroyed platform will respawn.    플랫폼이 파괴된 후 얼마 만큼의 시간 후에 재생성 될 것인지.

    [Header("게임 시작시 바로 발판이 움직일것인지. true면 바로 움직임")]
    public bool awakeStart = true;
    [Header("게임 시작 직후 처음 움직이기까지 대기 시간")]
    public float awakeWaitTime = 0;


    private int m_cur = 1;                //This part is Path number for the platform to go.                                        현재 가야할 경로 번호
    private bool m_Moveback = false;      //This part is Make sure you are on your way back after arriving at your destination.     현재 목적지를 찍고 되돌아 가는 중인지
    private bool m_MovingOn = false;      //m_MovingOn << Make sure the platform is moving.                                         플랫폼이 이동중인지
    private bool m_playerCheck = false;   //playerCheck << This part is make sure the player is on top.                             플레이어가 올라와 있는지 확인

    private Vector3[] m_Pos;                      //m_MovePoint값을 토대로 변환한 실제 월드좌표를 가지고 있는 배열
    private Vector3 m_firstPos = Vector3.zero;    //OnDrawGizmos에서 사용. 게임 시작 상태를 파악하고 초기 좌표를 저장

    private void Awake()
    {
        if(m_MovePoint.Length <= 0)
        {
            //If m_MovePoint << this thing is noting. just destroy. 이동 경로 없다면 스크립트를 삭제
            Destroy(this);
            return;

        }
    }









    private void Respawn()  //재생성 초기화. 모든 값을 초기화 시키고 다시 활성화 시킨다.
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
