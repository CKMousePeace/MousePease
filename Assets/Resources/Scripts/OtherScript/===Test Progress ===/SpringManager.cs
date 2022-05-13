using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringManager : MonoBehaviour
{
    //다이나믹 애니메 활성화 시킬 레벨
    public float g_DynamicRatio = 1.0f;

    public float            g_stiffnessForce;
    public AnimationCurve   g_stiffnessCurve;
    public float            g_dragForce;
    public AnimationCurve   g_dragCurve;
    public SpringBone[]     g_springBones;

    private void Start()
    {
        UpdateParameters();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (g_DynamicRatio >= 1.0f) g_DynamicRatio = 1.0f;
        else if (g_DynamicRatio <= 0.0f) g_DynamicRatio = 0.0f;
    }
#endif

    private void LateUpdate()
    {
        if (g_DynamicRatio != 0.0f)
            for (int i = 0; i < g_springBones.Length; i++)
                if (g_DynamicRatio > g_springBones[i].threshold)
                    g_springBones[i].UpdateSpring();
    }

    private void UpdateParameters()
    {
        UpdateParameter("g_stiffnessForce", g_stiffnessForce, g_stiffnessCurve);
        UpdateParameter("g_dragForce", g_dragForce, g_dragCurve);
    }

    private void UpdateParameter(string fieldName , float baseValue , AnimationCurve curve)
    {
#if UNITY_EDITOR
        var start = curve.keys[0].time;
        var end = curve.keys[curve.length - 1].time;

        var prop = g_springBones[0].GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

        for(int i = 0; i < g_springBones.Length; i++)
            if (!g_springBones[i].isUseEachBoneForceSetting)
            {
                var scale = curve.Evaluate(start + (end - start) * i / (g_springBones.Length - 1));
                prop.SetValue(g_springBones[i], baseValue * scale);
            }
#endif
    }


}
