using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private Collider m_BGMcol;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(m_BGMcol);
        }
    }
}