using System;
using UnityEngine;

public abstract class AbstractCollectable : MonoBehaviour
{
    private bool m_collected = false;
    
    protected virtual void Collect()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        m_collected = true;
        Collect();
    }
}
