using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBoss : CDynamicObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected void Update()
    {
        if (CompareBuff("")) return;

        foreach (var controller in m_ControllerBases)
        {

        }

    }

}
