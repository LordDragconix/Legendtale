using UnityEngine;

public class BouncingMouthSpawner : MonoBehaviour
{
    public GameObject mouthPrefab;
    public Transform[] bouncePoints;
    public int minMouths = 2;
    public int maxMouths = 5;

    void Start()
    {
        // Reset spawner's position
        transform.position = Vector3.zero;

        // Create a copy of the bounce points to avoid mouth errors if they’re destroyed
        Transform[] pointsCopy = new Transform[bouncePoints.Length];
        for (int i = 0; i < bouncePoints.Length; i++)
        {
            GameObject pointCopy = new GameObject("BouncePoint_Copy_" + i);
            pointCopy.transform.position = bouncePoints[i].position;
            pointCopy.transform.parent = transform; // Keep cleanup tidy
            pointsCopy[i] = pointCopy.transform;
        }

        int mouthCount = Random.Range(minMouths, maxMouths + 1);

        for (int i = 0; i < mouthCount; i++)
        {
            GameObject mouth = Instantiate(mouthPrefab, Vector3.zero, Quaternion.identity, transform); // parented to spawner
            var mover = mouth.GetComponent<BouncingMouthMover>();
            if (mover != null)
                mover.SetPoints(pointsCopy);
        }
    }
}
