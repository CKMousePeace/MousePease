using UnityEngine;

public class BossEyes : CBossSense
{
    [Range(0, 180)]
    public float FieldOfView;
    private float m_FieldOfViewDot;

    private void Start()
    {
        m_FieldOfViewDot = 1 - Remap(FieldOfView * 0.5f, 0, 90, 0, 1f);

        // 시야각은 내적. 1 - 1 /2 (0 에서 1 사이의 범위를 Mapping)
        // ex.( X 내적 Y) => 0도 = 1.00 , 45도 = 0.71 , 315도 = 0.71 , 90도 = 0.00 , 120도 = -0.50
    }

    private float Remap(float value, float originalStart, float originalEnd,
        float targetStart, float targetEnd)
    {
        return targetStart + (value - originalStart) * (targetEnd - targetStart) /
            (originalEnd - originalStart);
    }

    protected override bool HasDetected(Detectable detectable)
    {
        return IsInVisibleArea(detectable) && IsNotOccluded(detectable);
    }

    private bool IsInVisibleArea(Detectable detectable)     //보이는 (탐지하는) 구역
    {
        float distance = Vector3.Distance(detectable.transform.position, this.transform.position);

        return distance <= Distance && Vector3.Dot(Direction(detectable.transform.position,
            this.transform.position), this.transform.forward) >= m_FieldOfViewDot;

        // x - y = (x1 - y1 , x2 - y2 , x3 - y3) 백터 뺄샘
    }

    private Vector3 Direction(Vector3 from, Vector3 to)
    {
        return (from - to).normalized;
    }

    private bool IsNotOccluded(Detectable detectable)       //가려지지 않을때
    {
        if (Physics.Raycast(transform.position,
            detectable.transform.position - transform.position, out RaycastHit hit, Distance))
        {
            return hit.collider.gameObject.Equals(detectable.gameObject);

        }

        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = DebugDrawColor;
        for (float angle = -FieldOfView * 0.5f; angle <= FieldOfView * 0.5f; angle += 5f)
        {
            Vector3 lineEnd = RotatePointAroundPivot(transform.position + transform.forward * Distance,
                transform.position, angle);
            Gizmos.DrawLine(transform.position, lineEnd);
        }
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, float angles)
    {
        return Quaternion.Euler(0, angles, 0) * (point - pivot) + pivot;
    }
#endif
}