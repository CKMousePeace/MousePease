using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CDoor : MonoBehaviour
{

    [SerializeField] private GameObject[] m_Door;
    [SerializeField] private float m_Open = 2;
    [SerializeField] private float m_Close = 1;
    private bool m_isDoor = false;

    Renderer ButtonColor;

    // Start is called before the first frame update
    void Start()
    {
        ButtonColor = gameObject.GetComponent<Renderer>();
    }   

    // Update is called once per frame
    void Update()
    {

        if (m_isDoor)
        {
            Quaternion targetRotation0 = Quaternion.Euler(0, -90.0f, 0);
            m_Door[0].transform.localRotation = Quaternion.Slerp(m_Door[0].transform.localRotation, targetRotation0, m_Open * Time.deltaTime);

            Quaternion targetRotation1 = Quaternion.Euler(0, 270.0f, 0);
            m_Door[1].transform.localRotation = Quaternion.Slerp(m_Door[1].transform.localRotation, targetRotation1, m_Open * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            m_Door[0].transform.localRotation = Quaternion.Slerp(m_Door[0].transform.localRotation, targetRotation, m_Close * Time.deltaTime);

            Quaternion targetRotation1 = Quaternion.Euler(0, 180, 0);
            m_Door[1].transform.localRotation = Quaternion.Slerp(m_Door[1].transform.localRotation, targetRotation1, m_Close * Time.deltaTime);
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Trigger"))
        {
            ButtonColor.material.color = Color.blue;
            m_isDoor = true;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Trigger"))
        {
            ButtonColor.material.color = Color.red;
            m_isDoor = false;

        }
    }
}


//혹시 모를 변경점을 위해 (플레이어가 직접 오픈)
//Ray DoorForRay = new Ray(transform.position, transform.forward);
//RaycastHit hit; if (Physics.Raycast(DoorForRay, out hit, 얼마나 멀리 탐지할지)){
//    if (hit.collider.CompareTag("Trigger")) {
//        hit.collider.transform.parent.GetComponent<DoorScript>().ChangeDoorState();
//    }
//}
