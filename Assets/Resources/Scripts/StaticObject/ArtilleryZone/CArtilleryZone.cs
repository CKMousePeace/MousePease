using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArtilleryZone : MonoBehaviour
{
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

    [Header("Ʈ���� -> n�� �� �ߵ�")]
    [SerializeField] protected float m_WaitToPlay = 0;

    [Header("���� ���ӵ� �ð�")]
    [SerializeField] protected float m_Running = 0;

    [Header("�پ��� �ð�")]
    [SerializeField] protected float m_timer = 0;

    [Header("���� ������ ����")]
    [SerializeField] protected float m_Rockhigh = 0;
    //=======================================================================//


    //���� �ֱ� ���� ( DangerZone )
    private bool m_isCheck = false;
    private float m_CurrRuntime = 0;
    private float m_Currtime = 0;


    //============����===============//

    [Header("�÷��̾� �ȿ� ������ 0: ��Ȱ��ȭ 1: ����")]
    [SerializeField] private int m_isDebug = 0;

    //===============================//

    private void Update()
    {
 
        if (m_isCheck == true && m_ColCheckPlace.GetComponent<CPCheckForArtil>().g_isOnArtill == true)
        {
            RunningTime();
        }
        else if(m_isCheck == true && m_ColCheckPlace.GetComponent<CPCheckForArtil>().g_isOnArtill == false)
        {
            switch (m_isDebug)
            {
                case 0:
                    gameObject.SetActive(false);
                    break;

                case 1:
                    Destroy(gameObject);
                    break;

                default:
                    Debug.Log("0 or 1 �̶��!!");
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
            var player = GameObject.FindGameObjectWithTag("Player");    //�÷��̾� Find
            Vector3 spotPos = player.gameObject.transform.position;     //�÷��̾� ��ġ �޾ƿ�
            Instantiate(m_DangerZoneCol, new Vector3(spotPos.x, 0.5f, spotPos.z), Quaternion.identity);
            m_Currtime = 0;
        }
    }
}
