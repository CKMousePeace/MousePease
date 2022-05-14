using UnityEngine;

public class BossEyes : CBossSense
{
    [Range(0, 180)]
    public float FieldOfView;
    private float m_FieldOfViewDot;

    private void Start()
    {
        m_FieldOfViewDot = 1 - Remap(FieldOfView * 0.5f, 0, 90, 0, 1f);

        // �þ߰��� ����. 1 - 1 /2 (0 ���� 1 ������ ������ Mapping)
        // ex.( X ���� Y) => 0�� = 1.00 , 45�� = 0.71 , 315�� = 0.71 , 90�� = 0.00 , 120�� = -0.50
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

    private bool IsInVisibleArea(Detectable detectable)     //���̴� (Ž���ϴ�) ����
    {
        float distance = Vector3.Distance(detectable.transform.position, this.transform.position);

        return distance <= Distance && Vector3.Dot(Direction(detectable.transform.position,
            this.transform.position), this.transform.forward) >= m_FieldOfViewDot;

        // x - y = (x1 - y1 , x2 - y2 , x3 - y3) ���� ����
    }

    private Vector3 Direction(Vector3 from, Vector3 to)
    {
        return (from - to).normalized;
    }

    private bool IsNotOccluded(Detectable detectable)       //�������� ������
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