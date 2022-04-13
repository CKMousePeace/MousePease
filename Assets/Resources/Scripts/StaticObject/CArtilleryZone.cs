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

    [Tooltip("트리거 -> n초 후 발동")]
    [SerializeField] private float m_WaitToPlay = 0;

    [Tooltip("함정 지속될 시간")]      //시간 종료후 ArtilleryZone 비활성화
    [SerializeField] private float m_Running = 0;



    [Tooltip("범위에서 함정 스폰 주기")]
    [SerializeField] private float m_SpawnTime = 0;


    //스폰 주기 설정 ( DangerZone )
    private bool isCheck = false;
    private float m_Currtime = 0;


    private void Update()
    {
        Debug.Log("isCheck : " + isCheck);
        Debug.Log("m_Currtime : " + m_Currtime);
        if (isCheck)
        {
            SpawningZone();

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



    private void SpawningZone()
    {
        m_Currtime += Time.deltaTime;

        //몇초마다 실행할 것인지. m_Running 시간 만큼 실행
        if (m_Currtime > m_SpawnTime)
        {
            Vector3 spotPos = Player.gameObject.transform.position;     //플레이어 위치 받아옴
            Instantiate(m_DangerZoneCol, new Vector3(spotPos.x, 0.5f, spotPos.z), Quaternion.Euler(90, 0, 0));
            m_Currtime = 0;
        }
        
    }

}
