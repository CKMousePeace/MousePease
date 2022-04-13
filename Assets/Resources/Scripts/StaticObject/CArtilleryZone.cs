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

    [Tooltip("Ʈ���� -> n�� �� �ߵ�")]
    [SerializeField] private float m_WaitToPlay = 0;

    [Tooltip("���� ���ӵ� �ð�")]      //�ð� ������ ArtilleryZone ��Ȱ��ȭ
    [SerializeField] private float m_Running = 0;



    [Tooltip("�������� ���� ���� �ֱ�")]
    [SerializeField] private float m_SpawnTime = 0;


    //���� �ֱ� ���� ( DangerZone )
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

        //�迭 ��ü ����(������ � ���� �� �𸣹Ƿ�,)
        for (int i = 0; i < m_Blocker.Length; i++)
        {
            m_Blocker[i].SetActive(true);
            isCheck = true;
        }

    }



    private void SpawningZone()
    {
        m_Currtime += Time.deltaTime;

        //���ʸ��� ������ ������. m_Running �ð� ��ŭ ����
        if (m_Currtime > m_SpawnTime)
        {
            Vector3 spotPos = Player.gameObject.transform.position;     //�÷��̾� ��ġ �޾ƿ�
            Instantiate(m_DangerZoneCol, new Vector3(spotPos.x, 0.5f, spotPos.z), Quaternion.Euler(90, 0, 0));
            m_Currtime = 0;
        }
        
    }

}
