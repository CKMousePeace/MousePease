using UnityEngine;

public class BossEars : CBossSense
{
    protected override bool HasDetected(Detectable detectable)
    {
        return Vector3.Distance(detectable.transform.position, transform.position)
            <= Distance && detectable.CanBeHear;


        //HasDetected 덮어쓰기 제공 -> 감지할 수 있는 오브젝트가 Distance 안에 있고 그 안에 CanBeHear 가 
        //체크되어있다면 True 를 리턴
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = DebugDrawColor;
        Gizmos.DrawWireSphere(transform.position, Distance);
    }
#endif
}