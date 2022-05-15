using System.Collections;
using UnityEngine;

public class CBoomBoomCheese : MonoBehaviour
{
    [SerializeField] private GameObject m_BoomCheese;
    [SerializeField] private float m_BoomForce;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {

            StartCoroutine(BoomCheesePart_Co());
            //BoomCheesePart();
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


    IEnumerator BoomCheesePart_Co()
    {

        yield return new WaitForSeconds(2.0f);

        GameObject BCS = Instantiate(m_BoomCheese, transform.position, transform.rotation);

        foreach (Rigidbody rb in BCS.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * m_BoomForce;
            rb.AddForce(force);
        }

        Destroy(gameObject);
    }
}
