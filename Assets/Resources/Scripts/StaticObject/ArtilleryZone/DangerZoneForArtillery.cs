using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//�� ����

public class DangerZoneForArtillery : CArtilleryZone
{

    //���� �پ�� �� ������Ʈ
    [SerializeField] protected GameObject m_DamageZone;

    //������ ������ ǥ���� ������Ʈ
    [SerializeField] protected GameObject m_Indicator;

    //����߸� ������Ʈ
    [SerializeField] protected GameObject m_Rock;

    private void Start()
    {
        //������ ũ�� ��      size value to change
        var scaleTo = new Vector3(1.0f, 1.0f, 2.5f);
        //���� ������Ʈ , ������ ũ�� ��, �ҿ�ð�  GameObject , size value to change, time
        StartCoroutine(DangerZoneChecker(m_DamageZone, m_Rock, scaleTo, m_timer));

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

        if(elapsedTime > seconds)
        {
            m_DamageZone.SetActive(false);
            m_Rock.SetActive(true);   
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
