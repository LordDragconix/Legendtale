using UnityEngine;

public class StarEyeSpawner : MonoBehaviour
{
    public GameObject spiralSpawnerPrefab;

    [Header("Offsets relative to this object")]
    public Vector3 leftEyeOffset = new Vector3(-1.85f, 3.28f, 0f);
    public Vector3 rightEyeOffset = new Vector3(1.85f, 3.28f, 0f);



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

        // Spawn as children so we can set localPosition directly
        GameObject left = Instantiate(spiralSpawnerPrefab, transform);
        GameObject right = Instantiate(spiralSpawnerPrefab, transform);

        // Set their exact local positions
        left.transform.localPosition = new Vector3(-1.85f, 3.28f, 0f);
        right.transform.localPosition = new Vector3(1.85f, 3.28f, 0f);

        // Detach from parent if needed (make them world-space independent)
        left.transform.SetParent(null);
        right.transform.SetParent(null);

        var leftSpawner = left.GetComponent<SpiralProjectileSpawner>();
        var rightSpawner = right.GetComponent<SpiralProjectileSpawner>();

        AttackManager.Instance?.RegisterSpawner(leftSpawner);
        AttackManager.Instance?.RegisterSpawner(rightSpawner);
    }




}
