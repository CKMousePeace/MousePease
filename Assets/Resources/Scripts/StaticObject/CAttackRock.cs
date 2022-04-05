using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAttackRock : MonoBehaviour
{

    //이동할 거리
    Vector3 destination = new Vector3(3, 3, 3);

    [SerializeField] private float MoveTime = 0;

    float radius = 10.0f;
    float power = 1000.0f;

    //Collider[] colliders = Physics.OverlapSphere(_transform.position, radius);

    void Update()
    {
        
    }

    private void MoveRock()
    {
        Vector3 speed = Vector3.zero;
        transform.position = Vector3.Lerp(transform.position, destination, MoveTime);
    }

    private void TestMovement()
    {

        

    }

    


    private void OnTriggerEnter(Collider col)
    {

        if (CompareTag("Player"))
        {



        }

    }

}
