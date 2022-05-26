using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BossSoundManager : MonoBehaviour
{
    private enum CURRENT_TERRAIN
    {
        Normal,
        Grass,
        WoodFloor

    };


    private FMOD.Studio.EventInstance SFX_Boss_Walk;            //걷기(뛰기)
    private FMOD.Studio.EventInstance m_Boss_Attck;
    private FMOD.Studio.EventInstance m_Boss_AttckReady;
    private FMOD.Studio.EventInstance m_Boss_AttckRelease;

    private FMOD.Studio.EventInstance SFX_Boss_Pounce;          //HD (덮치기)
    private FMOD.Studio.EventInstance SFX_Boss_Pounce_Voice;    //HD (텊치기 보이스)

    private FMOD.Studio.EventInstance SFX_Boss_Rush;            //러쉬 스킬
    private FMOD.Studio.EventInstance SFX_Boss_Dead;            //으악 죽음
    private FMOD.Studio.EventInstance SFX_Boss_Roar;            //포효
    private FMOD.Studio.EventInstance SFX_Boss_Jump_Up;         //점프 업
    private FMOD.Studio.EventInstance SFX_Boss_Jump_Down;       //점프 착지
    [SerializeField] CURRENT_TERRAIN currentTranin;



    private void Update()
    {
        DetermineTerrain();
    }

    private void DetermineTerrain()     //지형 감지 함수  (인자를 fmod 에 넘겨줌)
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position, Vector3.down, 10.0f);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Normal"))
            {
                currentTranin = CURRENT_TERRAIN.Normal;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Grass"))
            {
                currentTranin = CURRENT_TERRAIN.Grass;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("WoodFloor"))
            {
                currentTranin = CURRENT_TERRAIN.WoodFloor;
            }

        }

    }


    // ========================== Boss FootStep ========================== //

    public void SelectAndPlayFootstep()
    {
        switch (currentTranin)
        {
            case CURRENT_TERRAIN.Normal:
                PlayFootstep(0);
                break;


            case CURRENT_TERRAIN.Grass:
                PlayFootstep(1);
                break;

            case CURRENT_TERRAIN.WoodFloor:
                PlayFootstep(2);
                break;
        }
    }

    private void PlayFootstep(int terrain)
    {
        SFX_Boss_Walk = RuntimeManager.CreateInstance("event:/Character enemy/SFX_Boss_Walk");
        SFX_Boss_Walk.setParameterByName("Tarrain", terrain);
        SFX_Boss_Walk.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_Boss_Walk.start();
        SFX_Boss_Walk.release();
    }

    // ================================================================== //


    // ========================== Boss Attack ========================== //
    public void SelectAndPlayAttack()
    {
        switch (currentTranin)
        {
            case CURRENT_TERRAIN.Normal:
                PlayAttck(0);
                break;


            case CURRENT_TERRAIN.Grass:
                PlayAttck(1);
                break;

            case CURRENT_TERRAIN.WoodFloor:
                PlayAttck(2);
                break;
        }
    }

    private void PlayAttck(int terrain)     //보스 공격 사운드
    {
        m_Boss_Attck = RuntimeManager.CreateInstance("event:/Character enemy/SFX_Boss_Pounce");
        m_Boss_Attck.setParameterByName("Tarrain", terrain);
        m_Boss_Attck.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_Boss_Attck.start();
        m_Boss_Attck.release();
    }

    public void PlayAttackReady()       //보스 공격 준비 사운드
    {
        m_Boss_AttckReady = RuntimeManager.CreateInstance("event:/Character enemy/보스 공격 준비");
        m_Boss_AttckReady.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_Boss_AttckReady.start();
        m_Boss_AttckReady.release();
    }

    public void PlayAttackRelease()       //보스 공격후 사운드
    {
        m_Boss_AttckRelease = RuntimeManager.CreateInstance("event:/Character enemy/보스 공격 후");
        m_Boss_AttckRelease.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_Boss_AttckRelease.start();
        m_Boss_AttckRelease.release();
    }


    // ================================================================== //






    // ========================== Boss ETC ========================== //

    public void PlayDead()              //보스 사망 사운드
    {
        SFX_Boss_Dead = RuntimeManager.CreateInstance("event:/Character enemy/SFX_Boss_Dead");
        SFX_Boss_Dead.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_Boss_Dead.start();
        SFX_Boss_Dead.release();
    }

    public void PlayRoar()               //보스 포효
    {
        SFX_Boss_Roar = RuntimeManager.CreateInstance("event:/Character enemy/SFX_Boss_Roar");
        SFX_Boss_Roar.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_Boss_Roar.start();
        SFX_Boss_Roar.release();
    }
    public void BossJumpUp()
    {
        SFX_Boss_Jump_Up = RuntimeManager.CreateInstance("event:/Character enemy/SFX_Boss_Jump_Up");
        SFX_Boss_Jump_Up.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_Boss_Jump_Up.start();
        SFX_Boss_Jump_Up.release();
    }


    public void SelectAndPlayJumpDoen()
    {
        switch (currentTranin)
        {
            case CURRENT_TERRAIN.Normal:
                BossJumpDown(0);
                break;


            case CURRENT_TERRAIN.Grass:
                BossJumpDown(1);
                break;

            case CURRENT_TERRAIN.WoodFloor:
                BossJumpDown(2);
                break;
        }
    }

    private void BossJumpDown(int terrain)
    {
        SFX_Boss_Jump_Down = RuntimeManager.CreateInstance("event:/Character enemy/SFX_Boss_Jump_Down");
        SFX_Boss_Jump_Down.setParameterByName("Tarrain", terrain);
        SFX_Boss_Jump_Down.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_Boss_Jump_Down.start();
        SFX_Boss_Jump_Down.release();
    }


    // ================================================================== //




}
