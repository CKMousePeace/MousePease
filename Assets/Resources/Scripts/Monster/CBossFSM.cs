using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossFSM : MonoBehaviour
{

    private enum AIState
    {
        WayPoint,
        Jump,
        Smash,
        Bite,
        HoldDown,
        Death
    }

    [SerializeField] private AIState _currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
