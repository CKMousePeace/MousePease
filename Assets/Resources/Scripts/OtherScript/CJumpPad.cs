using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CJumpPad : CStaticObject
{

    [SerializeField] private float m_Force;
    private CPlayer m_Player;
    private Rigidbody rb;

    [SerializeField] private Animator Animator;
    private FMOD.Studio.EventInstance SFX_Jump_Pad;

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.transform.CompareTag("Player"))
        {   
            if (rb == null)
                rb = collision.transform.GetComponent<Rigidbody>();

            if (m_Player == null)
                m_Player = collision.transform.GetComponent<CPlayer>();


            if (m_Player.CompareController("Jump"))
            {
                m_Player.DestroyController("Jump");
            }
            rb.velocity = Vector3.zero;            
            rb.velocity = Vector3.up * m_Force;

            //애니메 트리거
            Animator.SetTrigger("PlayerTrigger");
            PlayJumpPad();
        }
    }

    private void PlayJumpPad()
    {
        SFX_Jump_Pad = RuntimeManager.CreateInstance("event:/Interactables/SFX_Jump_Pad");
        SFX_Jump_Pad.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_Jump_Pad.start();
        SFX_Jump_Pad.release();

    }

}
