using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheese : CStaticObject
{
    // Start is called before the first frame update    
    [SerializeField] CBuffBase.BuffInfo m_BuffInfo;

    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerStay(Collider other)
    {
        var Target = other.gameObject.GetComponent<CPlayer>();
        if (Target == null)
            return;
        Target.GenerateBuff("Fast" , m_BuffInfo);
    }

    private void OnTriggerExit(Collider other)
    {
        var Target = other.gameObject.GetComponent<CPlayer>();
        if (Target == null)
            return;

        Target.DestroyBuff("Fast");
    }
}
