using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFootCol : MonoBehaviour
{

    [SerializeField] private GameObject m_SoundM;       //사운드 체커

    //다리 콜라이더 확인 (바닥과 충돌 체크)
    private void ColliderEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Carpat") ||
           col.gameObject.layer == LayerMask.NameToLayer("Grass") ||
           col.gameObject.layer == LayerMask.NameToLayer("WoodFloor"))
        {
            m_SoundM.gameObject.GetComponent<BossSoundManager>().DetermineTerrain();
            Debug.Log("사운드 넘어옴 확인해!! ");
        }
    }

}
