using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossEfect : MonoBehaviour
{
    [SerializeField] ParticleSystem Footsteb_L;
    [SerializeField] ParticleSystem Footsteb_R;

    [SerializeField] ParticleSystem Idle_buble;
    [SerializeField] ParticleSystem Idle_MouthSmell;
    [SerializeField] ParticleSystem Idle_Start_L;
    [SerializeField] ParticleSystem Idle_Start_R;
    [SerializeField] ParticleSystem Idle_Saliva;
    [SerializeField] ParticleSystem Idle_Aura;

    [SerializeField] ParticleSystem Attack_Roar;
    [SerializeField] ParticleSystem Attack_Bite;

    [SerializeField] ParticleSystem Attack_HoldDown;

    [SerializeField] GameObject FL_;
    [SerializeField] GameObject FR_;
        
    
    private void Update()
    {
        Footsteb_L.transform.SetParent(null);
        Footsteb_R.transform.SetParent(null);
    }

    public void FootStepEfect_L()
    {
        Vector3 FL;
        FL = FL_.transform.position;

        Footsteb_L.transform.position = FL;
        Footsteb_L.Play();
        
    }

    public void FootStepEfect_R()
    {
        Vector3 FR;
        FR = FR_.transform.position;

        Footsteb_R.transform.position = FR;
        Footsteb_R.Play();
        
    }

    public void PlayRoar()
    {
        Attack_Roar.Play();
    }

    public void PlayBite()
    {
        Attack_Bite.Play();
    }

    public void PlayHoldDown()
    {
        Attack_HoldDown.Play();
    }

}
