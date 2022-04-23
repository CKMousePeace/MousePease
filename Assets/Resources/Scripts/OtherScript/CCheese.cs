using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CCheese : MonoBehaviour
{
    // Start is called before the first frame update
    private CPlayer m_Player;
    [SerializeField] private Material m_Material;
    
    void Update()
    {
        //if (m_Player == null)
        //    m_Player = GameObject.FindWithTag("Player").GetComponent<CPlayer>();
        //
        //if (m_Material == null) return;
        //var Pos = m_Player.transform.position;
        //var ShaderPos = new Vector4(Pos.x, Pos.y, Pos.z, 1.0f);        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
            m_Material.SetFloat("_AlphaColor", 0.5f);
    }

}
