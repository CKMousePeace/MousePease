using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class CBuffBase : MonoBehaviour
{
    [Serializable]
    public struct BuffInfo
    {
        
        [SerializeField] private List<int> m_Value_Int;
        [SerializeField] private List<float> m_Value_Float;
        [SerializeField] private List<Vector3> m_Value_Vector3;

        public List<int> g_Value_Int => m_Value_Int;
        public List<float> g_Value_Float => m_Value_Float;
        public List<Vector3> g_Value_Vector3 => m_Value_Vector3;
    }


    [SerializeField] private string m_BuffName; 
    private CDynamicObject m_DynamicObject;    

    protected CDynamicObject g_DynamicObject => m_DynamicObject;
    public string g_BuffName => m_BuffName;
    public virtual void init(CDynamicObject dynamicObject) => m_DynamicObject = dynamicObject;
    public void GenerateBuff(BuffInfo buffinfo)
    {        
        OnBuffInit(buffinfo);
    }
    protected abstract void OnBuffInit(BuffInfo buff);    
}
