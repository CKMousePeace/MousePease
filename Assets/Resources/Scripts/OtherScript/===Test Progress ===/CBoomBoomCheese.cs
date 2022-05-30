using System.Collections;
using UnityEngine;

public class CBoomBoomCheese : MonoBehaviour
{
    [SerializeField] private GameObject m_BoomCheese;
    [SerializeField] private float m_BoomForce;

    [Header("ÆÄ±« µô·¹ÀÌ ÃÊ´ÜÀ§")]
    [SerializeField] private int DelayTime = 1;


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Boss"))
        {
            StartCoroutine(DelayBoomBoom(DelayTime));
        }
        else return;
    }

    IEnumerator DelayBoomBoom(int delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject BCS = Instantiate(m_BoomCheese, transform.position, transform.rotation);

        foreach (Rigidbody rb in BCS.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * m_BoomForce;
            rb.AddForce(force);
        }

        Destroy(gameObject);
    }

}
