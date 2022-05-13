using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDashItem : CItemBase
{

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        var Actor = other.gameObject.GetComponent<CDynamicObject>();
        if (Actor == null) return;

        var Dash = Actor.GetController("Dash") as CDash;
        if (Dash == null) return;

        Dash.g_DashItem = true;
    }
}
