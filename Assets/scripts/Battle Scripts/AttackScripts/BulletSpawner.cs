using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRate = 0.5f;
    public Transform[] spawnPoints;

    public bool randomSpawn = false; // NEW: toggle for random spawn mode

    private bool running = true;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (running)
        {
            if (randomSpawn)
            {
                // Pick a random spawn point
                int index = Random.Range(0, spawnPoints.Length);
                Instantiate(bulletPrefab, spawnPoints[index].position, Quaternion.identity);
            }
            else
            {
                // Spawn from all points
                foreach (Transform point in spawnPoints)
                {
                    Instantiate(bulletPrefab, point.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void StopAttack()
    {
        running = false;
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
