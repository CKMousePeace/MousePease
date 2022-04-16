using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheese : MonoBehaviour
{
    private bool isChecker = false;

    private void OnCollisionEnter(Collision col)
    {
        isChecker = true;

        if (col.collider.CompareTag("Player")){

            if (isChecker == true)
            {
                //StartCoroutine(SelfDestroy());
            }

        }
    }

    IEnumerator SelfDestroy(float time)
    {
        yield return new WaitForSeconds(time);
    }

}
