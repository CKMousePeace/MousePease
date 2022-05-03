using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheese : CStaticObject
{
    // Start is called before the first frame update    

    protected override void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //버프 추가
    }
    private void OnTriggerExit(Collider other)
    {
        //버프 삭제 
    }

}
