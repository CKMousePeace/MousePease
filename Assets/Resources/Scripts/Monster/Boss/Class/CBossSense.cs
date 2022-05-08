using UnityEngine;
using UnityEngine.Events;

public class CBossSense : MonoBehaviour
{
    public Detectable Detectable;           //감지할 요소
    public float Distance;                  //감지 거리

    protected bool IsSensing;

    public bool IsDetectionContinuous = true;

    public UnityAction<Detectable> OnDetect;
    public UnityAction<Detectable> OnLost;

#if UNITY_EDITOR
    public Color DebugDrawColor = Color.green;
#endif

    private void Detect(Detectable detectable)
    {
        IsSensing = true;
        OnDetect?.Invoke(detectable);
    }
    private void Lost(Detectable detectable)
    {
        IsSensing = false;
        OnLost?.Invoke(detectable);
    }

    void Update()
    {
        if (IsSensing)
        {
            if (!HasDetected(Detectable))
            {
                Lost(Detectable);
                return;
            }

            if (IsDetectionContinuous)
            {
                Detect(Detectable);
            }
        }
        else
        {
            if (!HasDetected(Detectable))
                return;

            Detect(Detectable);
        }
    }

    protected virtual bool HasDetected(Detectable detectable) => false;
}