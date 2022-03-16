using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMagnet : CStaticObject
{
    public enum MagnetType
    {
        Normal = 0,        
        NPole = 1,
        SPole = 2,
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
        else if (g_Pole == MagnetType.Normal)
            m_Renderer.material.color = Color.yellow;


    }

}
