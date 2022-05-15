using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CStartManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerBoi;
    [SerializeField] private Animator BossAnime;
    [SerializeField] private NavMeshAgent BossBoi;

    private void OnCollisionEnter(Collision col)
    {
        //�÷��̾� ����
        BossBoi.speed = 6;
        BossAnime.SetBool("IntroLoopChecker", true);

    }

}
