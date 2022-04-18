using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class IntroSoundManager : MonoBehaviour
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
    [SerializeField] CURRENT_TERRAIN currentTranin;



    private void Update()
    {
        
    }




    public void PlayDead() 
    {
        m_Boss_Dead = RuntimeManager.CreateInstance("event:/Character enemy/ 보스 사망");
        m_Boss_Dead.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_Boss_Dead.start();
        m_Boss_Dead.release();
    }

    public void PlayRoar()
    {
        m_Boss_Dead = RuntimeManager.CreateInstance("event:/Character enemy/ 보스 포효");
        m_Boss_Dead.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_Boss_Dead.start();
        m_Boss_Dead.release();
    }







}
