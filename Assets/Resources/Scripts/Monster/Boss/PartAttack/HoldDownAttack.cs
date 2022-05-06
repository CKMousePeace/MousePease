using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HoldDownAttack : CBossAttack
{
    [Tooltip("HoldDown 콜라이더")]
    [SerializeField] private GameObject HoldDownCol;


    public Transform m_Target;
    public float m_InitialAngle = 50f; // 처음 날라가는 각도
    [SerializeField]private Rigidbody m_Rigidbody;

    public float m_Speed = 10;
    public float m_HeightArc = 1;
    private Vector3 m_StartPosition;
    private bool m_IsStart;


    void Start()
    {
        m_StartPosition = transform.position;
    }


    void Update()
    {
        //Parabola();

        if(m_IsStart == true)
        {
            Vector3 velocity = GetVelocity(transform.position, m_Target.position, m_InitialAngle);
            m_Rigidbody.velocity = velocity;
        }
    }


    protected void OnEnable()
    {
        BossNav.enabled = false;
        //BossNav.isStopped = true;
        BossAni.SetTrigger("HoldReady");
        StartCoroutine(HoldDownMode(2, m_Speed/m_Speed +1 ));
    }

    protected void OnDisable()
    {
        m_IsStart = false;
        Boss.GetComponent<Rigidbody>().isKinematic = true;
        HoldDownCol.SetActive(false);
        return;
    }




    IEnumerator HoldDownMode(int WaitTime , float HDTime )
    {
        while (true)
        {
            Debug.Log("2초 대기");
            yield return new WaitForSeconds(WaitTime);
            BossAni.SetTrigger("HoldLoop");

            m_IsStart = true;
   
            yield return new WaitForSeconds(HDTime);

            BossNav.enabled = true;
            gameObject.SetActive(false);
            yield break;

        }
    }


    private void Parabola()
    {
        if(m_IsStart == true)
        {
            Debug.Log("점프 실행");
            float x0 = m_StartPosition.x;
            float x1 = m_Target.position.x;
            float distance = x1 - x0;
            float nextX = Mathf.MoveTowards(transform.position.x, x1, m_Speed * Time.deltaTime);
            float baseY = Mathf.Lerp(m_StartPosition.y, m_Target.position.y, (nextX - x0) / distance);
            float arc = m_HeightArc * (nextX - x0) * (nextX - x1) / (-0.25f * distance * distance);
            Vector3 nextPosition = new Vector3(nextX, baseY + arc, transform.position.z);

            transform.rotation = LookAt3D(nextPosition - transform.position);
            transform.position = nextPosition;


            if (nextPosition == m_Target.position)
                Arrived();
        }
    }

    void Arrived()
    {
        gameObject.SetActive(false);
    }

    Quaternion LookAt3D(Vector3 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }


    public Vector3 GetVelocity(Vector3 player, Vector3 target, float initialAngle)
    {
        Boss.GetComponent<Rigidbody>().isKinematic = false;
        HoldDownCol.SetActive(true);

        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
        Vector3 planarPosition = new Vector3(player.x, 0, player.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = player.y - target.y;

        float initialVelocity
            = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity
            = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects
            = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > player.x ? 1 : -1);
        Vector3 finalVelocity
            = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        m_IsStart = false;
        return finalVelocity;
    }

}
