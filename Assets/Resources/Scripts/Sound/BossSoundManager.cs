using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BossSoundManager : MonoBehaviour
{

    private enum CURRENT_TERRAIN
    {
        Carpet,
        Grass,
        WoodFloor

    };

    private FMOD.Studio.EventInstance m_FootSteps;
    [SerializeField] CURRENT_TERRAIN currentTranin;


    private void Update()
    {
        DetermineTerrain();
    }

    public void DetermineTerrain()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position, Vector3.down, 10.0f);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Carpat"))
            {
                currentTranin = CURRENT_TERRAIN.Carpet;
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


    private void PlayFootstep(int terrain)
    {
        m_FootSteps = RuntimeManager.CreateInstance("event:/Character enemy/Boss Footsteps");
        m_FootSteps.setParameterByName("Tarrain", terrain);
        m_FootSteps.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_FootSteps.start();
        m_FootSteps.release();
    }

    public void SelectAndPlayFootstep()
    {
        switch (currentTranin)
        {
            case CURRENT_TERRAIN.Carpet:
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
}
