using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CCheese_Shader : MonoBehaviour
{  
    
    private MeshRenderer m_MashRender;
    
    [SerializeField] private Material m_Cheese_InPlayer;
    [SerializeField] private Material m_Cheese_base;
    

    private Material m_Cheese_Player;
    private float m_CheeseAlphaTime;
    private bool m_InCheese;    


    private void Start()
    {
        
        m_MashRender = gameObject.GetComponent<MeshRenderer>();
        m_Cheese_Player = new Material(m_Cheese_InPlayer);
    }

    private void Update()
    {
        if (m_CheeseAlphaTime >= 1.0f)
        {
            if (m_MashRender.sharedMaterial == m_Cheese_Player)
                m_MashRender.sharedMaterial = m_Cheese_base;
        }
        else if (m_InCheese == false)
        {
            m_CheeseAlphaTime += Time.deltaTime;
            if (m_MashRender.sharedMaterial == m_Cheese_Player)
                m_MashRender.sharedMaterial.SetColor("_BaseColor", new Color(1.0f, 1.0f, 1.0f, m_CheeseAlphaTime));
        
        }

        m_CheeseAlphaTime = Mathf.Clamp(m_CheeseAlphaTime, 0.5f, 1.0f);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("Player"))
        {            
            m_MashRender.sharedMaterial = m_Cheese_Player;
            m_MashRender.sharedMaterial.SetColor("_BaseColor" ,new Color(1.0f,1.0f,1.0f,0.5f));            
        }

        m_InCheese = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            m_CheeseAlphaTime -= Time.fixedDeltaTime;
            m_MashRender.sharedMaterial.SetColor("_BaseColor", new Color(1.0f, 1.0f, 1.0f, m_CheeseAlphaTime));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {            
            m_InCheese = false;
        }
    }

}
