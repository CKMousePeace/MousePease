using System.Collections;
using UnityEngine;
using FMODUnity;

public class CBoomBoomCheese : MonoBehaviour
{
    [SerializeField] private GameObject m_BoomCheese;
    [SerializeField] private float m_BoomForce;

    [Header("ÆÄ±« µô·¹ÀÌ")]
    [SerializeField] private float DelayTime = 1;

    private FMOD.Studio.EventInstance SFX_WoodWallCrash;


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Boss"))
        {
            StartCoroutine(DelayBoomBoom(DelayTime));
        }
        else return;
    }

    IEnumerator DelayBoomBoom(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject BCS = Instantiate(m_BoomCheese, transform.position, transform.rotation);
        PlayWoodCrash();
        foreach (Rigidbody rb in BCS.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * m_BoomForce;
            rb.AddForce(force);
        }

        Destroy(gameObject);
    }



    private void PlayWoodCrash()
    {
        SFX_WoodWallCrash = RuntimeManager.CreateInstance("event:/Interactables/SFX_WoodWallCrash");
        SFX_WoodWallCrash.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        SFX_WoodWallCrash.start();
        SFX_WoodWallCrash.release();
    }
}
