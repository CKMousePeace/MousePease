using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFootCol : MonoBehaviour
{

    [SerializeField] private GameObject m_SoundM;       //���� üĿ

    //�ٸ� �ݶ��̴� Ȯ�� (�ٴڰ� �浹 üũ)
    private void ColliderEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Carpat") ||
           col.gameObject.layer == LayerMask.NameToLayer("Grass") ||
           col.gameObject.layer == LayerMask.NameToLayer("WoodFloor"))
        {
            m_SoundM.gameObject.GetComponent<BossSoundManager>().DetermineTerrain();
            Debug.Log("���� �Ѿ�� Ȯ����!! ");
        }
    }

}
