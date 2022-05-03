using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//�ش� ��ũ��Ʈ�� ������ƮǮ�� ����Ǿ����� ������ �˸�. (�� ���� �ڵ�, �Ź��� ->DangerZone ~~ ForArtillery

public class CDangerZone : MonoBehaviour
{
    //���� �پ�� �� ������Ʈ
    [SerializeField]  private GameObject m_DamageZone;

    //������ ������ ǥ���� ������Ʈ
    [SerializeField] private GameObject m_Indicator;

    //����߸� ������Ʈ
    [SerializeField] private GameObject m_Cheese;

    //�پ��� �ð�
    [SerializeField]  private float m_timer;

    [Header("���� ������ ����")]
    [SerializeField] protected float m_Rockhigh = 0;


    //�÷��̾� ��ġ�� ���ִ��� Ȯ��
    private bool m_Checker = false;

    private void OnTriggerEnter(Collider col)
    {
        m_Checker = true;
        if (col.CompareTag("Player"))
        {
            //������ ũ�� ��      size value to change
            var scaleTo = new Vector3(1.0f, 1.0f, 2.5f);

            //���� ������Ʈ , ������ ũ�� ��, �ҿ�ð�  GameObject , size value to change, time
            StartCoroutine(DangerZoneChecker(m_DamageZone, m_Cheese,  scaleTo, m_timer));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_Checker = false;
    }

    IEnumerator DangerZoneChecker(GameObject objectToScale, GameObject Rock, Vector3 scaleTo, float seconds)
        //���� ũ�⸦ seconds �ð���ŭ ���� ���̴� �ڷ�ƾ
    {
        float elapsedTime = 0;
        Vector3 startingScale = objectToScale.transform.localScale;
        Rock.transform.position = new Vector3(gameObject.transform.position.x, m_Rockhigh, gameObject.transform.position.z);
        while (elapsedTime < seconds)
        {
            objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToScale.transform.position = scaleTo;

        if(m_Checker == true)
        {
            m_DamageZone.SetActive(false);
            m_Cheese.SetActive(true);
                
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
