using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDashItem : CItemBase
{
    [SerializeField] private float m_RotateSpeed = 100f;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, m_RotateSpeed * Time.deltaTime, 0));
    }

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
