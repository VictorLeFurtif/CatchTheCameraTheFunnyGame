using System.Collections.Generic;
using UnityEngine;

public class NewMapElementsTrigger : MonoBehaviour
{
    [Header("Sol Pooling")]
    [SerializeField] private GameObject solPrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private float solLength = 40f;

    private Queue<GameObject> solPool = new Queue<GameObject>();
    private float nextSpawnZ;

    private void Start()
    {
        nextSpawnZ = transform.parent.position.z;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject sol = Instantiate(
                solPrefab,
                new Vector3(0f, 0f, nextSpawnZ),
                Quaternion.identity
            );

            solPool.Enqueue(sol);
            nextSpawnZ += solLength;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameObject sol = solPool.Dequeue();

        sol.transform.position = new Vector3(0f, 0f, nextSpawnZ);

        solPool.Enqueue(sol);

        nextSpawnZ += solLength;
    }
}