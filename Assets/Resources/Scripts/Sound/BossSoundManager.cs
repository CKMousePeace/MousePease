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


    private FMOD.Studio.EventInstance m_B_FootSteps;
    private FMOD.Studio.EventInstance m_B_Attck;
    private FMOD.Studio.EventInstance m_B_AttckReady;
    private FMOD.Studio.EventInstance m_B_AttckRelease;
    private FMOD.Studio.EventInstance m_B_Dead;
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
        m_B_FootSteps = RuntimeManager.CreateInstance("event:/Character enemy/Boss Footsteps");
        m_B_FootSteps.setParameterByName("Tarrain", terrain);
        m_B_FootSteps.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_B_FootSteps.start();
        m_B_FootSteps.release();
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
        m_B_Attck = RuntimeManager.CreateInstance("event:/Character enemy/Boss Attack");
        m_B_Attck.setParameterByName("Tarrain", terrain);
        m_B_Attck.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_B_Attck.start();
        m_B_Attck.release();
    }

    public void PlayAttackReady()       //보스 공격 준비 사운드
    {
        m_B_AttckReady = RuntimeManager.CreateInstance("event:/Character enemy/보스 공격 준비");
        m_B_AttckReady.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_B_AttckReady.start();
        m_B_AttckReady.release();
    }

    public void PlayAttackRelease()       //보스 공격후 사운드
    {
        m_B_AttckRelease = RuntimeManager.CreateInstance("event:/Character enemy/보스 공격 후");
        m_B_AttckRelease.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_B_AttckRelease.start();
        m_B_AttckRelease.release();
    }


    // ================================================================== //






    // ========================== Boss ETC ========================== //

    public void PlayDead()              //보스 사망 사운드
    {
        m_B_Dead = RuntimeManager.CreateInstance("event:/Character enemy/ 보스 사망");
        m_B_Dead.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_B_Dead.start();
        m_B_Dead.release();
    }

    // ================================================================== //

}
