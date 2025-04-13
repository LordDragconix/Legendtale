using UnityEngine;

public class StarEyeSpawner : MonoBehaviour
{
    public GameObject spiralSpawnerPrefab;

    [Header("Offsets relative to this object")]
    public Vector3 leftEyeOffset = new Vector3(-1.46f, -1.01f, 0f);
    public Vector3 rightEyeOffset = new Vector3(2.37f, -1.02f, 0f);

    public bool spawnOnStart = true;

    private bool hasSpawned = false;

    void Start()
    {
        if (spawnOnStart)
            SpawnSpawners();
    }

    public void SpawnSpawners()
    {
        if (hasSpawned || spiralSpawnerPrefab == null)
            return;

        hasSpawned = true;

        Vector3 leftPos = transform.position + leftEyeOffset;
        Vector3 rightPos = transform.position + rightEyeOffset;

        Instantiate(spiralSpawnerPrefab, leftPos, Quaternion.identity, transform);
        Instantiate(spiralSpawnerPrefab, rightPos, Quaternion.identity, transform);
    }
}
