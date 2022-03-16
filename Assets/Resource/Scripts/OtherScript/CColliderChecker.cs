using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CColliderChecker : MonoBehaviour
{
    [Header("Collider 流立 持绢林技咯")]
    [SerializeField] private Collider m_collider;
    public Collider g_Collider => m_collider;
    
    public delegate void CollisionEnter(Collision other);
    public delegate void CollisionStay (Collision other);
    public delegate void CollisionExit (Collision other);
    public delegate void TriggerEnter(Collider other);
    public delegate void TriggerStay (Collider other);
    public delegate void TriggerExit (Collider other);

    public CollisionEnter m_ColliderEnter;
    public CollisionStay  m_ColliderStay;
    public CollisionExit  m_ColliderExit;
    public TriggerEnter m_TriggerEnter;
    public TriggerStay  m_TriggerStay;
    public TriggerExit  m_TriggerExit;

    
    private void OnTriggerEnter(Collider other)
    {
        if (m_TriggerEnter == null) return;
        m_TriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (m_TriggerExit == null) return;
        m_TriggerExit(other);
    }
    private void OnTriggerStay(Collider other)
    {
        if (m_TriggerStay == null) return;
        m_TriggerStay(other);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (m_ColliderEnter == null) return;
        m_ColliderEnter(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (m_ColliderStay == null) return;
        m_ColliderStay(collision);

    }
    private void OnCollisionExit(Collision collision)
    {
        if (m_ColliderExit == null) return;
        m_ColliderExit(collision);
    }
}
