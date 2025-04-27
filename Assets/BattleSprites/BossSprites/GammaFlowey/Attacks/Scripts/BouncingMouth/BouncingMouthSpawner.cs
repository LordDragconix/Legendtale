using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BouncingMouthSpawner : MonoBehaviour
{
    public GameObject mouthPrefab;
    public Transform[] bouncePoints;
    public int minMouths = 2;
    public int maxMouths = 5;
    public float spawnDelay = 0.2f; // Delay between each spawn
    public float despawnDelay = 0.3f; // Delay between each despawn

    private List<BouncingMouthMover> spawnedMouths = new List<BouncingMouthMover>();

    void Start()
    {
        transform.position = Vector3.zero;

        Transform[] pointsCopy = new Transform[bouncePoints.Length];
        for (int i = 0; i < bouncePoints.Length; i++)
        {
            GameObject pointCopy = new GameObject("BouncePoint_Copy_" + i);
            pointCopy.transform.position = bouncePoints[i].position;
            pointCopy.transform.parent = transform;
            pointsCopy[i] = pointCopy.transform;
        }

        StartCoroutine(SpawnMouths(pointsCopy));
    }

    IEnumerator SpawnMouths(Transform[] pointsCopy)
    {
        int mouthCount = Random.Range(minMouths, maxMouths + 1);

        for (int i = 0; i < mouthCount; i++)
        {
            GameObject mouth = Instantiate(mouthPrefab, Vector3.zero, Quaternion.identity, transform);
            var mover = mouth.GetComponent<BouncingMouthMover>();
            if (mover != null)
            {
                mover.SetPoints(pointsCopy);
                spawnedMouths.Add(mover);
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void DespawnMouths()
    {
        StartCoroutine(DespawnMouthsCoroutine());
    }

    IEnumerator DespawnMouthsCoroutine()
    {
        foreach (var mover in spawnedMouths)
        {
            if (mover != null)
            {
                mover.Despawn();
                yield return new WaitForSeconds(despawnDelay);
            }
        }
    }
}
