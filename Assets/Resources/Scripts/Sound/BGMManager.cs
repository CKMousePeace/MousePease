using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private Collider m_BGMcol;
    [SerializeField] private GameObject BGM;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(BGM);
            Destroy(m_BGMcol);
        }
    }
}