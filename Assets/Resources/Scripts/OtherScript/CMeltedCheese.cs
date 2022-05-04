using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMeltedCheese : CStaticObject
{
    
    [SerializeField , Header("Float[0]->���ο� ��  Float[1] -> ���� �ð�")]   
    CBuffBase.BuffInfo m_BuffInfo;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        var Target = other.gameObject.GetComponent<CPlayer>();
        if (Target == null)
            return;        
        Target.GenerateBuff("Slow", m_BuffInfo);

    }

}
