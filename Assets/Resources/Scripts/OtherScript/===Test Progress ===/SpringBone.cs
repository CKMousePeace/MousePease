using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBone : MonoBehaviour
{
    //움직일 최상위 본 For Moving parent Bone
    public Transform g_Child;

    //본의 방향 Bone Direction
    [SerializeField] Vector3 m_boneAxis = new Vector3(-1.0f, 0.0f, 0.0f);
    [SerializeField] float m_Radius = 0.05f;

    //stiffnessForce 및 dragForce 를 사용 할 것인지에 대한 확인
    public bool isUseEachBoneForceSetting = false;

    //Spring 이 다시 돌아 올 힘
    [SerializeField] float m_stiffnessForce = 0.01f;

    //감쇠력 damping force
    [SerializeField] float m_DragForce = 0.4f;
    [SerializeField] Vector3 m_SpringForce = new Vector3(0.0f, -0.0001f, 0.1f);
    [SerializeField] SpringCollider[] colliders;        //스크립트 springCollider 확인 
    [SerializeField] bool Debug_Spring = true;

    public float threshold = 0.01f;
    private float m_springLength;
    private Quaternion m_localRotation;
    private Transform m_Trs;
    private Vector3 m_currTipPos;
    private Vector3 m_prevTipPos;

    private Transform m_org;

    private SpringManager m_managerRef;       //스크립트 SpringManager 확인 


    private void Awake()
    {
        m_Trs = transform;
        m_localRotation = transform.localRotation;

        m_managerRef = GetParentSpringManager(transform);
    }

    private SpringManager GetParentSpringManager(Transform t)
    {
        var springManager = t.GetComponent<SpringManager>();

        if (springManager != null)
            return springManager;

        if (t.parent != null)
            return GetParentSpringManager(t.parent);

        return null;
    }

    private void Start()
    {
        m_springLength = Vector3.Distance(m_Trs.position, g_Child.position);
        m_currTipPos = g_Child.position;
        m_prevTipPos = g_Child.position;

    }

    public void UpdateSpring()
    {

        m_org = m_Trs;
        m_Trs.localRotation = Quaternion.identity * m_localRotation;

        float sqrDr = Time.deltaTime * Time.deltaTime;

        //stiffness
        Vector3 force = m_Trs.rotation * (m_boneAxis * m_stiffnessForce) / sqrDr;

        //Drag
        force += (m_prevTipPos - m_currTipPos) * m_DragForce / sqrDr;

        force += m_SpringForce / sqrDr;

        //이전 프레임과 값 같지 않게. Made Before Frame =/ current Frame
        Vector3 temp = m_currTipPos;

        //Verlet
        m_currTipPos = (m_currTipPos - m_prevTipPos) + m_currTipPos + (force * sqrDr);

        //Length Return
        m_currTipPos = ((m_currTipPos - m_Trs.position).normalized * m_springLength) + m_Trs.position;

        //콜리전 충돌 
        for(int i=0; i<colliders.Length; i++)
            if(Vector3.Distance (m_currTipPos,colliders[i].transform.position) <= (m_Radius + colliders[i].g_Radius))
            {
                Vector3 normal = (m_currTipPos - colliders[i].transform.position).normalized;
                m_currTipPos = colliders[i].transform.position + (normal * (m_Radius + colliders[i].g_Radius));
                m_currTipPos = ((m_currTipPos - m_Trs.position).normalized * m_springLength) + m_Trs.position;
            }


        m_prevTipPos = temp;

        //Rotation
        Vector3 aimVector = m_Trs.TransformDirection(m_boneAxis);
        Quaternion aimRotarion = Quaternion.FromToRotation(aimVector, m_currTipPos - m_Trs.position);

        Quaternion secondaryRotation = aimRotarion * m_Trs.rotation;
        m_Trs.rotation = Quaternion.Lerp(m_org.rotation, secondaryRotation, m_managerRef.g_DynamicRatio);
    }


    private void OnDrawGizmos()
    {
        if (Debug_Spring)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(m_currTipPos, m_Radius);
        }
    }

}
