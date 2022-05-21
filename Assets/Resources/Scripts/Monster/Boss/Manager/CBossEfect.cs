using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossEfect : MonoBehaviour
{
    [SerializeField] ParticleSystem Footsteb_L;
    [SerializeField] ParticleSystem Footsteb_R;

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

}
