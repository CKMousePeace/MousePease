using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class CItemInfo : ScriptableObject
{
    [SerializeField] private string m_ItemName;
    [SerializeField] private List<int> m_ValueInt;
    [SerializeField] private List<float> m_ValueFloat;
    [SerializeField] private List<Vector3> m_ValueVector3;

    public string g_ItemName => m_ItemName;
    public List<int> g_ValueInt => m_ValueInt;
    public List<float> g_ValueFloat => m_ValueFloat;
    public List<Vector3> g_ValueVector3 => m_ValueVector3;
}
