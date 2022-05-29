using System.Collections;
using UnityEngine;

namespace Assets.Resources.Scripts.Monster.Boss.Class
{
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
}