using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlatformManager : MonoBehaviour
{
    [SerializeField] private List<CPlatform> m_platform = new List<CPlatform>();
    public List<CPlatform> g_platform => m_platform;


    private void Start()
    {        
        var platformObj = GameObject.FindGameObjectsWithTag("Floor");
        foreach (var obj in platformObj)
        {
            m_platform.Add(obj.GetComponent<CPlatform>());
        }
    }
}
