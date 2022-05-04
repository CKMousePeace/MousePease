using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldDownAttack : CBossAttack
{
    [Tooltip("HoldDown 콜라이더")]
    [SerializeField] private GameObject HoldDownCol;

    void Start()
    {
        
    }


    void Update()
    {
        
    }


    protected void OnEnable()
    {
        //준비 대기 시간2초

        // 플레이어 위치로 포물선 운동

        //오브젝트 HoldDown Col 활성화
    }


}
