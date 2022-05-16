using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStaticObject : CActor
{   
    protected override void Start()
    {       
            
    }

    protected bool CrushChecker(CDynamicObject obj)
    {
        if (obj.CompareBuff("Invincibility"))
        {
            return true;
        }
        return false;
    }
}
