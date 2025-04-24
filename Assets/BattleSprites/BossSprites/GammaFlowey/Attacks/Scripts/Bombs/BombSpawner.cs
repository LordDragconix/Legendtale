using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;
    public float spawnInterval = 1f;
    public float xMin = -8f;
    public float xMax = 8f;
    public float spawnY = 6f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnBomb), 0f, spawnInterval);
    }

    void SpawnBomb()
    {
        Vector3 spawnPos = new Vector3(Random.Range(xMin, xMax), spawnY, 0f);
        Instantiate(bombPrefab, spawnPos, Quaternion.identity);
    }
}
