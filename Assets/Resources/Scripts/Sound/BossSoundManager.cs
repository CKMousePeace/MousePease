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


    private FMOD.Studio.EventInstance m_Boss_FootSteps;
    private FMOD.Studio.EventInstance m_Boss_Attck;
    private FMOD.Studio.EventInstance m_Boss_AttckReady;
    private FMOD.Studio.EventInstance m_Boss_AttckRelease;
    private FMOD.Studio.EventInstance m_Boss_Dead;
    private FMOD.Studio.EventInstance m_Boss_Roar;
    private FMOD.Studio.EventInstance m_Boss_Jump;
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
        m_Boss_FootSteps = RuntimeManager.CreateInstance("event:/Character enemy/Boss Footsteps");
        m_Boss_FootSteps.setParameterByName("Tarrain", terrain);
        m_Boss_FootSteps.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_Boss_FootSteps.start();
        m_Boss_FootSteps.release();
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
        m_Boss_Attck = RuntimeManager.CreateInstance("event:/Character enemy/Boss Attack");
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
        m_Boss_Dead = RuntimeManager.CreateInstance("event:/Character enemy/ 보스 사망");
        m_Boss_Dead.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_Boss_Dead.start();
        m_Boss_Dead.release();
    }

    public void PlayRoar()
    {
        m_Boss_Roar = RuntimeManager.CreateInstance("event:/Character enemy/BossRoar");
        m_Boss_Roar.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_Boss_Roar.start();
        m_Boss_Roar.release();
    }
    public void PlayJump()
    {
        m_Boss_Jump = RuntimeManager.CreateInstance("event:/Character enemy/BossJump");
        m_Boss_Jump.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_Boss_Jump.start();
        m_Boss_Jump.release();
    }


    // ================================================================== //





}
