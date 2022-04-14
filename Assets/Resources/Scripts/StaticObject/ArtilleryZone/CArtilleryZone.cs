using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArtilleryZone : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    //�÷��̾� �浹 �޾ƿ� ������Ʈ
    [SerializeField] private GameObject m_PlayerCheckZone;

    //�÷��̾��� Ż���� ���� ������Ʈ
    [SerializeField] private GameObject[] m_Blocker;

    //�÷��̾�� �������� �� ������Ʈ(������)
    [SerializeField] private GameObject m_DangerZoneCol;

    //�÷��̾� ���� ���� �ִ��� Ȯ��
    [SerializeField] private GameObject m_ColCheckPlace;

    [Header("�������� ���� ���� �ֱ�")]
    [SerializeField] private float m_SpawnTime = 0;


    //=======================================================================//
    //
    //���� �پ�� �� ������Ʈ
    [SerializeField] protected GameObject m_DamageZone;

    //������ ������ ǥ���� ������Ʈ
    [SerializeField] protected GameObject m_Indicator;

    //����߸� ������Ʈ
    [SerializeField] protected GameObject m_Rock;

    [Header("Ʈ���� -> n�� �� �ߵ�")]
    [SerializeField] protected float m_WaitToPlay = 0;

    //�ð� ������ ArtilleryZone ��Ȱ��ȭ
    [Header("���� ���ӵ� �ð�")]
    [SerializeField] protected float m_Running = 0;

    [Header("�پ��� �ð�")]
    [SerializeField] protected float m_timer = 0;

    [Header("���� ������ ����")]
    [SerializeField] protected float m_Rockhigh = 0;
    //=======================================================================//


    //���� �ֱ� ���� ( DangerZone )
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

        //�迭 ��ü ����(������ � ���� �� �𸣹Ƿ�,)
        for (int i = 0; i < m_Blocker.Length; i++)
        {
            m_Blocker[i].SetActive(true);
            isCheck = true;
        }

    }


    private void RunningTime()
    {

            m_CurrRuntime += Time.deltaTime;

            //������ ���ӵ� �ð� (m_Running)

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

        //�����ֱ�. m_SpawnTime �ð����� ����
        if (m_Currtime > m_SpawnTime)
        {
            Vector3 spotPos = Player.gameObject.transform.position;     //�÷��̾� ��ġ �޾ƿ�
            Instantiate(m_DangerZoneCol, new Vector3(spotPos.x, 0.5f, spotPos.z), Quaternion.identity);
            m_Currtime = 0;
        }
    }
}
