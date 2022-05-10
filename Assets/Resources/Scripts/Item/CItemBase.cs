using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CItemBase : MonoBehaviour
{
    [SerializeField] private CItemInfo m_itemInfo;

    protected virtual void OnTriggerEnter(Collider other)
    {        
        gameObject.SetActive(false);
    }
}
