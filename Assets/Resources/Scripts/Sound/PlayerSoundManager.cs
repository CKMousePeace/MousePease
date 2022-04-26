using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerSoundManager : MonoBehaviour
{
    private enum CURRENT_TERRAIN
    {
        Normal,
        Grass,
        WoodFloor
    };


    private FMOD.Studio.EventInstance m_player_FootSteps;
    private FMOD.Studio.EventInstance m_player_Dead;
    private FMOD.Studio.EventInstance m_player_Jump;
    private FMOD.Studio.EventInstance m_player_JumpLand;
    private FMOD.Studio.EventInstance m_player_CheeseRush;
    private FMOD.Studio.EventInstance m_palyer_ADDREADY;
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

    // ========================== player FootStep ========================== //

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
        m_player_FootSteps = RuntimeManager.CreateInstance("event:/Character player/Player Footsteps");
        m_player_FootSteps.setParameterByName("Tarrain", terrain);
        m_player_FootSteps.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_player_FootSteps.start();
        m_player_FootSteps.release();
    }

    // ================================================================== //



    // ========================== player jump / Land ========================== //
    public void PlayJump()
    {
        m_player_Jump = RuntimeManager.CreateInstance("event:/Character player/ 플레이어 점프");
        m_player_Jump.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_player_Jump.start();
        m_player_Jump.release();

    }

    public void SelectAndPlayJumpLand()
    {
        switch (currentTranin)
        {
            case CURRENT_TERRAIN.Normal:
                PlayJumpLand(0);
                break;


            case CURRENT_TERRAIN.Grass:
                PlayJumpLand(1);
                break;

            case CURRENT_TERRAIN.WoodFloor:
                PlayJumpLand(2);
                break;
        }
    }

    public void PlayJumpLand(int terrain)
    {
        m_player_JumpLand = RuntimeManager.CreateInstance("event:/Character player/ 플레이어 점프착지");
        m_player_JumpLand.setParameterByName("Tarrain", terrain);
        m_player_JumpLand.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_player_JumpLand.start();
        m_player_JumpLand.release();
    }

    // ================================================================== //





    // ========================== Boss ETC ========================== //

    public void PlayDead()              //플레이어 사망 사운드
    {
        m_player_Dead = RuntimeManager.CreateInstance("event:/Character player/ 플레이어 사망");
        m_player_Dead.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_player_Dead.start();
        m_player_Dead.release();
    }

    // ================================================================== //





}
