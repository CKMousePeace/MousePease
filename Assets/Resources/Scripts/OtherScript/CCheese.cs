using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CCheese : MonoBehaviour
{
    //
    static class CheeseInfo 
    {
        static private Material m_BaseCheese;
        static private Material m_InPlayerCheese;

        public static Material BaseGetMaterial()
        {
            if (m_BaseCheese == null)
                m_BaseCheese = Resources.Load<Material>("Prefabs\\Environment\\material\\Cheeze");
            return m_BaseCheese;
        }

        public static Material InPlayerGetMaterial()
        {
            if (m_InPlayerCheese == null)
                m_InPlayerCheese = Resources.Load<Material>("Prefabs\\Environment\\material\\Cheeze_1");
            return m_InPlayerCheese;
        }
    }


    // Start is called before the first frame update    
    private MeshRenderer m_MashRender;


    private Material m_Cheese_InPlayer;
    private Material m_Cheese_base;
    private float m_CheeseAlphaTime;
    private bool m_InCheese;

    private void Awake()
    {
        m_Cheese_base = CheeseInfo.BaseGetMaterial() ;
        m_Cheese_InPlayer = new Material(CheeseInfo.InPlayerGetMaterial());

        m_MashRender = gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (m_CheeseAlphaTime >= 1.0f)
        {
            if (m_MashRender.sharedMaterial == m_Cheese_InPlayer)
                m_MashRender.sharedMaterial = m_Cheese_base;
        }
        else if (m_InCheese == false)
        {
            m_CheeseAlphaTime += Time.deltaTime;
            if (m_MashRender.sharedMaterial == m_Cheese_InPlayer)
                m_MashRender.sharedMaterial.SetColor("_BaseColor", new Color(1.0f, 1.0f, 1.0f, m_CheeseAlphaTime));
        
        }

        m_CheeseAlphaTime = Mathf.Clamp(m_CheeseAlphaTime, 0.5f, 1.0f);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("Player"))
        {
            m_MashRender.sharedMaterial = m_Cheese_InPlayer;
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
