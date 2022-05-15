using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossEfect : MonoBehaviour
{

    [SerializeField] ParticleSystem Footsteb_L;
    [SerializeField] ParticleSystem Footsteb_R;

    public void FootStepEfect_L()
    {
        Footsteb_L.Play();
    }

    public void FootStepEfect_R()
    {
        Footsteb_R.Play();
    }

}
