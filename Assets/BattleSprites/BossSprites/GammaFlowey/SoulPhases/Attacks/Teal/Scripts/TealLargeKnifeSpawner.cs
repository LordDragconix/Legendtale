using UnityEngine;
using System.Collections;

public class TealLargeKnifeSpawner : MonoBehaviour
{
    [Header("Knife Prefabs")]
    public GameObject blueKnifePrefab;
    public GameObject orangeKnifePrefab;

    [Header("Spawn Points")]
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    [Header("Knife Settings")]
    public float knifeSpeed = 5f;
    public Transform centreReferencePoint; // optional
    public float spawnInterval = 1.5f; // how often to spawn knives

    private void Start()
    {
        StartCoroutine(SpawnKnivesOverTime());
    }

    IEnumerator SpawnKnivesOverTime()
    {
        while (true)
        {
            SpawnKnife();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnKnife()
    {
        bool spawnLeft = Random.value > 0.5f;
        bool isOrange = Random.value > 0.5f;

        Vector2 spawnPos = spawnLeft ? (Vector2)leftSpawnPoint.position : (Vector2)rightSpawnPoint.position;
        Vector2 moveDirection = spawnLeft ? Vector2.right : Vector2.left; // 🔥 Straight across!

        GameObject prefabToSpawn = isOrange ? orangeKnifePrefab : blueKnifePrefab;

        GameObject knife = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        // Set speed and direction
        var knifeScript = knife.GetComponent<TealLargeKnife>();
        if (knifeScript != null)
        {
            knifeScript.moveSpeed = knifeSpeed;
            knifeScript.direction = moveDirection;
        }
    }

}
