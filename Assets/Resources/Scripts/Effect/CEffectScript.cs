using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEffectScript : MonoBehaviour
{
    private ParticleSystem m_particle;
    private float m_time;

    private void Awake()
    {
        m_particle = gameObject.GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        m_time = 0.0f;
    }

    private void Update()
    {
        if (m_particle == null) return;

        m_time += Time.deltaTime;


        if (m_time >= m_particle.main.duration)
        {
            gameObject.SetActive(false);
        }
    }

}
