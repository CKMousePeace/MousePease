using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAttack : CBossAttack
{

    protected void OnEnable()
    {
        StartCoroutine(BiteMode(5.0f));
    }

    protected void OnDisable()
    {
        return;
    }


    IEnumerator BiteMode(float Time)
    {
        while (true)
        {

            BossNav.speed = 10.0f;

            yield return new WaitForSeconds(Time);

            BossNav.speed = 6.0f;
            gameObject.SetActive(false);
            yield break;
        }
    }


}
