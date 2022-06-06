using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CDashItem : CItemBase
{
    [SerializeField] private float m_RotateSpeed = 100f;
    private FMOD.Studio.EventInstance SFX_DashItem;
    private CPlayer m_Player;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, m_RotateSpeed * Time.deltaTime, 0));
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        var Actor = other.gameObject.GetComponent<CDynamicObject>();
        if (Actor == null) return;
        if (m_Player == null)
        {
            m_Player = Actor.transform.GetComponent<CPlayer>();
        }

        m_Player.g_fStamina = m_Player.g_fMaxStamina;
        PlayDashItem();


    }
    private void PlayDashItem()                 //플레이어 아이템 습득하는 소리
    {
        SFX_DashItem = RuntimeManager.CreateInstance("event:/Interactables/SFX_DashItem");
        SFX_DashItem.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_DashItem.start();
        SFX_DashItem.release();
    }

}
