using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CCheese_Shader : MonoBehaviour
{

    private GameObject m_Player;
    private Camera m_MainCamera;
    private Material m_Mat;


    [SerializeField] private LayerMask m_LayerMask;

    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_MainCamera = Camera.main;
        m_Mat = gameObject.GetComponent<Renderer>().material;
    }

    private void Update()
    {

        RaycastHit hit;
        if (Physics.Linecast(m_MainCamera.transform.position, m_Player.transform.position, out hit, m_LayerMask))
        {
            m_Mat.SetVector("_PlayerPos", hit.point);
        }
        else
        {
            m_Mat.SetVector("_PlayerPos", new Vector3(0.0f , 0.0f , -1000.0f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            m_Mat.SetVector("_PlayerPos", new Vector3(0.0f, 0.0f, -100.0f));
        }
    }
}
