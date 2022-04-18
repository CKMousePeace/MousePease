using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArtilleryZone : MonoBehaviour
{
    //플레이어 충돌 받아올 오브젝트
    [SerializeField] private GameObject m_PlayerCheckZone;

    //플레이어의 탈출을 막을 오브젝트
    [SerializeField] private GameObject[] m_Blocker;

    //플레이어에게 데미지를 줄 돌 오브젝트(프리팹)
    [SerializeField] private GameObject m_DangerZoneCol;

    //플레이어의 경로를 방해할 치즈 오브젝트(프리팹)
    [SerializeField] private GameObject m_CheeseZone;


    //플레이어 함정 위에 있는지 확인
    [SerializeField] private GameObject m_ColCheckPlace;

    [Header("범위에서 함정 스폰 주기")]
    [SerializeField] private float m_SpawnTime = 0;


    //=======================================================================//

    [Header("트리거 -> n초 후 발동")]
    [SerializeField] protected float m_WaitToPlay = 0;

    [Header("함정 지속될 시간")]
    [SerializeField] protected float m_Running = 0;

    [Header("줄어드는 시간")]
    [SerializeField] protected float m_timer = 0;

    [Header("돌이 떨어질 높이")]
    [SerializeField] protected float m_Rockhigh = 0;
    //=======================================================================//


    //스폰 주기 설정 ( DangerZone )
    private bool m_isCheck = false;
    private float m_CurrRuntime = 0;
    private float m_Currtime = 0;


    //============실험===============//

    [Header("플레이어 안에 없을시 false: 비활성화 true: 삭제")]
    [SerializeField] private bool m_isDebug = false;

    [Header(" false: 돌 공격 true: 치즈공격 ")]
    [SerializeField] private bool m_isDebugTrap = false;

    //===============================//

    private void Update()
    {
 
        if (m_isCheck == true && m_ColCheckPlace.GetComponent<CPCheckForArtil>().g_isOnArtill == true)
        {
            RunningTime();
            Destroy(m_ColCheckPlace.GetComponent<BoxCollider>());       //점프시 함정이 지워지는것 방지
        }
        else if(m_isCheck == true && m_ColCheckPlace.GetComponent<CPCheckForArtil>().g_isOnArtill == false)
        {
            switch (m_isDebug)
            {
                case false:
                    gameObject.SetActive(false);
                    break;

                case true:
                    Destroy(gameObject);
                    break;
            }          
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
        for (int i = 0; i < m_Blocker.Length; i++)
        {
            m_Blocker[i].SetActive(true);
            m_isCheck = true;
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
            var player = GameObject.FindGameObjectWithTag("Player");    //플레이어 Find
            Vector3 spotPos = player.gameObject.transform.position;     //플레이어 위치 받아옴

            switch (m_isDebugTrap)
            {

                case false:
                    Instantiate(m_DangerZoneCol, new Vector3(spotPos.x, 0.5f, spotPos.z), Quaternion.identity);
                    break;

                case true:
                    Instantiate(m_CheeseZone, new Vector3(spotPos.x, 2.25f, spotPos.z), Quaternion.identity);
                    break;

            }         
            m_Currtime = 0;
        }
    }//m_DangerZoneCol
}
