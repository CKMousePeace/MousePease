using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HoldDownAttack : CBossAttack
{
    [Header("HoldDown 콜라이더")]
    [SerializeField] private GameObject HoldDownCol;

    [Header("떨어질 지점")]
    [SerializeField] private Transform m_Target;

    [Header("날아갈 각도")]
    public float m_InitialAngle = 50f;
    [SerializeField] private Rigidbody m_Rigidbody;      //보스 리지드바디

    //[Header("날아갈 속도")]
    //[SerializeField] private float m_Speed = 10;

    //[Header("날아갈 높이")]
    //[SerializeField] private float m_HeightArc = 1;
    //private Vector3 m_StartPosition;
    private bool m_IsStart = false;
    private bool m_isGround = false;
    private bool RayCastBoii = false;



    void Start()
    {
        //m_StartPosition = transform.position;
    }
    void FixedUpdate()
    {
        if (m_IsStart == true)
        {
            Vector3 velocity = GetVelocity(transform.position, m_Target.position, m_InitialAngle);
            m_Rigidbody.velocity = velocity;

        }
        else return;
    }
    private void Update()
    {
        Debug.Log("이즈 그라운드상태!! :" + m_isGround);
        Debug.DrawRay(transform.position, transform.up * -1, Color.blue, 0.3f);

        if (RayCastBoii == true)
        {
            RayCastBoi();
        }
        else return;
    }

    protected void OnEnable()
    {
        BossNav.enabled = false;

        StartCoroutine(HoldDownMode(2, 4.0f, 1.0f));
    }

    protected void OnDisable()
    {
        m_IsStart = false;
        BossRigid.isKinematic = true;
        HoldDownCol.SetActive(false);
        return;
    }


    private void RayCastBoi()
    {
        RaycastHit hit;
        float MaxDistance = 8.0f;
        int layerMask = 1 << LayerMask.NameToLayer("Floor");
        if (Physics.Raycast(transform.position, transform.up * -1, out hit, MaxDistance, layerMask))
        {
            Debug.Log("레이케스트 힛!");
            BossAni.SetBool("isGround", true);
            m_isGround = true;

        }

    }

    IEnumerator HoldDownMode(int WaitTime , float LoopTime , float HDTime )
    {
        while (true)
        {
            //2초 대기
            BossAni.SetTrigger("HoldReady");
            yield return new WaitForSeconds(WaitTime);

            BossAni.SetTrigger("HoldDownStart");
            BossAni.SetBool("isGround" , false);
            m_isGround = false;
            m_IsStart = true;

            yield return new WaitForSeconds(1.0f);      //레이 아래로 쏘자마자 인식 되는거 막는거야!
            RayCastBoii = true;

            yield return new WaitUntil(() => m_isGround == true);

            yield return new WaitForSeconds(HDTime);        //착지 후 대기시간. 
            if (m_isGround == true) BossAni.SetTrigger("HoldFinish");
            BossNav.enabled = true;
            gameObject.SetActive(false);
            yield break;

        }
    }

    //private void Parabola()
    //{
    //    if(m_IsStart == true)
    //    {
    //        Debug.Log("점프 실행");
    //        float x0 = m_StartPosition.x;
    //        float x1 = m_Target.position.x;
    //        float distance = x1 - x0;
    //        float nextX = Mathf.MoveTowards(transform.position.x, x1, m_Speed * Time.deltaTime);
    //        float baseY = Mathf.Lerp(m_StartPosition.y, m_Target.position.y, (nextX - x0) / distance);
    //        float arc = m_HeightArc * (nextX - x0) * (nextX - x1) / (-0.25f * distance * distance);
    //        Vector3 nextPosition = new Vector3(nextX, baseY + arc, transform.position.z);

    //        transform.rotation = LookAt3D(nextPosition - transform.position);
    //        transform.position = nextPosition;


    //        if (nextPosition == m_Target.position)
    //            Arrived();
    //    }
    //}
    //void Arrived()
    //{
    //    gameObject.SetActive(false);
    //}
    //Quaternion LookAt3D(Vector3 forward)
    //{
    //    return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    //}


    public Vector3 GetVelocity(Vector3 BossTrans, Vector3 target, float initialAngle)
    {
        BossRigid.isKinematic = false;
        HoldDownCol.SetActive(true);

        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
        Vector3 planarPosition = new Vector3(BossTrans.x, 0, BossTrans.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = BossTrans.y - target.y;

        float initialVelocity
            = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity
            = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects
            = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > BossTrans.x ? 1 : -1);
        Vector3 finalVelocity
            = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        m_IsStart = false;
        return finalVelocity;
    }

}
