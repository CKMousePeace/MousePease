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


    private FMOD.Studio.EventInstance SFX_PC_Run;           //기본 발자국
    private FMOD.Studio.EventInstance SFX_PC_Dash;          //데쉬
    private FMOD.Studio.EventInstance SFX_PC_Slow;          //Slow 상태
    private FMOD.Studio.EventInstance SFX_PC_Die;           //으악 죽음
    private FMOD.Studio.EventInstance SFX_PC_Fly;           //활공
    private FMOD.Studio.EventInstance SFX_PC_Jump_Up;       //점프
    private FMOD.Studio.EventInstance SFX_PC_Jump_Down;     //착지
    [SerializeField] CURRENT_TERRAIN currentTranin;
    [SerializeField] private GameObject PlayerJump;

    private void Start()
    {
        PlayerJump = GameObject.Find("Jump");
    }

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


    private void PlayFootstep(int terrain)      //플레이어 기본 발자국 소리
    {
        //if (PlayerJump.GetComponent<CJump>().g_isJump == true)
        
            SFX_PC_Run = RuntimeManager.CreateInstance("event:/Character player/SFX_PC_Run");
            SFX_PC_Run.setParameterByName("Tarrain", terrain);
            SFX_PC_Run.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            SFX_PC_Run.start();
            SFX_PC_Run.release();
        
    }

    private void PlayDash()                 //플레이어 데쉬 소리
    {
        SFX_PC_Dash = RuntimeManager.CreateInstance("event:/Character player/SFX_PC_Dash");
        SFX_PC_Run.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_PC_Run.start();
        SFX_PC_Run.release();
    }

    private void PlayFly()                 //플레이어 데쉬 소리
    {
        SFX_PC_Fly = RuntimeManager.CreateInstance("event:/Character player/SFX_PC_Fly");
        SFX_PC_Fly.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_PC_Fly.start();
        SFX_PC_Fly.release();
    }

    private void PlaySlow()
    {
        SFX_PC_Fly = RuntimeManager.CreateInstance("event:/Character player/SFX_PC_Slow");
        SFX_PC_Fly.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_PC_Fly.start();
        SFX_PC_Fly.release();
    }

    // ================================================================== //



    // ========================== player jump / Land ========================== //
    public void PlayJump()
    {
        SFX_PC_Slow = RuntimeManager.CreateInstance("event:/Character player/SFX_PC_Jump_Up");
        SFX_PC_Slow.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_PC_Slow.start();
        SFX_PC_Slow.release();

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
        SFX_PC_Jump_Down = RuntimeManager.CreateInstance("event:/Character player/SFX_PC_Jump_Down");
        SFX_PC_Jump_Down.setParameterByName("Tarrain", terrain);
        SFX_PC_Jump_Down.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_PC_Jump_Down.start();
        SFX_PC_Jump_Down.release();
    }

    // ================================================================== //





    // ========================== Boss ETC ========================== //

    public void PlayDead()              //플레이어 사망 사운드
    {
        SFX_PC_Die = RuntimeManager.CreateInstance("event:/Character player/SFX_PC_Die");
        SFX_PC_Die.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_PC_Die.start();
        SFX_PC_Die.release();
    }

    // ================================================================== //





}
