using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;

public class CStartManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance m_Boss_Roar;
    [SerializeField] private GameObject PlayerBoi;
    [SerializeField] private Animator BossAnime;
    [SerializeField] private NavMeshAgent BossBoi;

    public void PlayRoar()
    {
        m_Boss_Roar = RuntimeManager.CreateInstance("event:/Character enemy/BossRoar");
        m_Boss_Roar.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        m_Boss_Roar.start();
        m_Boss_Roar.release();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            StartCoroutine(delayAnime());
            PlayRoar();

            Destroy(gameObject);
        }
    }
    IEnumerator delayAnime()
    {
        BossAnime.SetBool("IntroLoopChecker", true);
        yield return new WaitForSeconds(1.0f);
        BossBoi.speed = 6;
    }
}
