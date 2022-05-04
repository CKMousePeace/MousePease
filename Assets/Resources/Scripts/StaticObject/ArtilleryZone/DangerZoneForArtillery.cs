using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//�� ����

public class DangerZoneForArtillery : MonoBehaviour
{

    //���� �پ�� �� ������Ʈ
    [SerializeField] private GameObject m_DamageZone;
    [Header("�پ��� �ð�")]
    [SerializeField] private float m_timer = 0;

    [Header("���� ������ ����")]
    [SerializeField] private float m_Rockhigh = 0;

    private void Start()
    {
        //������ ũ�� ��      size value to change
        var scaleTo = new Vector3(1.0f, 1.0f, 2.5f);
        //���� ������Ʈ , ������ ũ�� ��, �ҿ�ð�  GameObject , size value to change, time
        StartCoroutine(DangerZoneChecker(m_DamageZone, scaleTo, m_timer));

    }

    IEnumerator DangerZoneChecker(GameObject objectToScale , Vector3 scaleTo, float seconds)
        //���� ũ�⸦ seconds �ð���ŭ ���� ���̴� �ڷ�ƾ
    {
        float elapsedTime = 0;
        Vector3 startingScale = objectToScale.transform.localScale;
        while (elapsedTime < seconds)
        {
            objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToScale.transform.position = scaleTo;

        if(elapsedTime > seconds)
        {

            //������Ʈ Ǯ���� Rock �޾ƿ�
            CObjectPool.g_instance.ObjectPop("Rock", new Vector3(gameObject.transform.position.x, m_Rockhigh, gameObject.transform.position.z),
            Quaternion.Euler(-90, 0, 0), new Vector3(1, 1, 1));

            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
