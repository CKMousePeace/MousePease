using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMagnetManager : MonoBehaviour
{
    [SerializeField] private List<CMagnet> m_magnet = new List<CMagnet>();
    public List<CMagnet> g_magnet => m_magnet;

    private void Start()
    {
        
        var MagnetObj = GameObject.FindGameObjectsWithTag("Magnet");
        foreach (var obj in MagnetObj)
        {
            m_magnet.Add(obj.GetComponent<CMagnet>());
        }
    }
}
