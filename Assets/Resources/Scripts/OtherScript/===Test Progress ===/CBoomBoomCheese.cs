using UnityEngine;

public class CBoomBoomCheese : MonoBehaviour
{
    [SerializeField] private GameObject m_BoomCheese;
    [SerializeField] private float m_BoomForce;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            BoomCheesePart();
        }
    }


    private void BoomCheesePart()
    {
        GameObject BCS=  Instantiate(m_BoomCheese, transform.position, transform.rotation);

        foreach(Rigidbody rb in BCS.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * m_BoomForce;
            rb.AddForce(force);
        }

        Destroy(gameObject);
    }
}
