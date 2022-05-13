using UnityEngine;

public class BossEars : CBossSense
{
    protected override bool HasDetected(Detectable detectable)
    {
        return Vector3.Distance(detectable.transform.position, transform.position)
            <= Distance && detectable.CanBeHear;


        //HasDetected ����� ���� -> ������ �� �ִ� ������Ʈ�� Distance �ȿ� �ְ� �� �ȿ� CanBeHear �� 
        //üũ�Ǿ��ִٸ� True �� ����
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = DebugDrawColor;
        Gizmos.DrawWireSphere(transform.position, Distance);
    }
#endif
}