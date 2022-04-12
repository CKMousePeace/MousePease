using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMagnet : CStaticObject
{
    public enum MagnetType
    {        
        NPole = 0,
        SPole = 1,
    }

    [SerializeField] private float m_Force;
    [SerializeField] private MagnetType m_Pole;

    public float g_Force => m_Force;
    public MagnetType g_Pole => m_Pole;
    private MeshRenderer m_Renderer;
    
    protected override void Start()
    {
        base.Start();
        m_Renderer = GetComponent<MeshRenderer>();
    }


    private void Update()
    {
        if (g_Pole == MagnetType.SPole)
            m_Renderer.material.color = Color.blue;
        else if (g_Pole == MagnetType.NPole)
            m_Renderer.material.color = Color.red;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, g_Force);
    }


}
