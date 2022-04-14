using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArtilleryZone : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    //플레이어 충돌 받아올 오브젝트
    [SerializeField] private GameObject m_PlayerCheckZone;

    //플레이어의 탈출을 막을 오브젝트
    [SerializeField] private GameObject[] m_Blocker;

    //플레이어에게 데미지를 줄 오브젝트(프리팹)
    [SerializeField] private GameObject m_DangerZoneCol;

    //플레이어 함정 위에 있는지 확인
    [SerializeField] private GameObject m_ColCheckPlace;

    [Header("범위에서 함정 스폰 주기")]
    [SerializeField] private float m_SpawnTime = 0;


    //=======================================================================//
    //
    //점점 줄어들 원 오브젝트
    [SerializeField] protected GameObject m_DamageZone;

    //떨어질 구역을 표시할 오브젝트
    [SerializeField] protected GameObject m_Indicator;

    //떨어뜨릴 오브젝트
    [SerializeField] protected GameObject m_Rock;

    [Header("트리거 -> n초 후 발동")]
    [SerializeField] protected float m_WaitToPlay = 0;

    //시간 종료후 ArtilleryZone 비활성화
    [Header("함정 지속될 시간")]
    [SerializeField] protected float m_Running = 0;

    [Header("줄어드는 시간")]
    [SerializeField] protected float m_timer = 0;

    [Header("돌이 떨어질 높이")]
    [SerializeField] protected float m_Rockhigh = 0;
    //=======================================================================//


    //스폰 주기 설정 ( DangerZone )
    private bool isCheck = false;
    private float m_CurrRuntime = 0;
    private float m_Currtime = 0;


    private void Update()
    {
 
        if (isCheck == true && m_ColCheckPlace.GetComponent<CPCheckForArtil>().g_isOnArtill == true)
        {
            RunningTime();
        }
        else if(isCheck == true && m_ColCheckPlace.GetComponent<CPCheckForArtil>().g_isOnArtill == false)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine("WaitPlayerZone");
        }
    }

    IEnumerator WaitPlayerZone()
    {
        yield return new WaitForSeconds(m_WaitToPlay);

        //배열 전체 실행(함정이 몇개 있을 지 모르므로,)
        for (int i = 0; i < m_Blocker.Length; i++)
        {
            m_Blocker[i].SetActive(true);
            isCheck = true;
        }

    }


    private void RunningTime()
    {

            m_CurrRuntime += Time.deltaTime;

            //함정이 지속될 시간 (m_Running)

            if (m_CurrRuntime < m_Running)
            {
                SpawningZone();

            }
            else if (m_CurrRuntime > m_Running)
            {
                //isCheck = false;
                Destroy(gameObject);
            }
    }


    private void SpawningZone()
    {
        m_Currtime += Time.deltaTime;

        //스폰주기. m_SpawnTime 시간마다 스폰
        if (m_Currtime > m_SpawnTime)
        {
            Vector3 spotPos = Player.gameObject.transform.position;     //플레이어 위치 받아옴
            Instantiate(m_DangerZoneCol, new Vector3(spotPos.x, 0.5f, spotPos.z), Quaternion.identity);
            m_Currtime = 0;
        }
    }
}
