using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheese : CStaticObject
{
    // Start is called before the first frame update
    private CPlayer m_Player;
    private CPlayerMovement m_PlayerMoveMent;

    protected override void Start()
    {
        base.Start();

        m_PlayerMoveMent = m_Player.GetController("Movement") as CPlayerMovement;
        if (m_PlayerMoveMent == null)
            Debug.LogError("Movement가 들어가지 않았습니다");
    }

}
