using UnityEngine;
using System.Collections.Generic;

public class SpiralProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int spiralArms = 4;
    public float spawnRate = 0.1f;
    public float spiralTightness = 20f;
    public float speed = 5f;
    public float rotationSpeed = 2f;
    public float projectileLifetime = 5f;

    public float spawnerLifetime = 3f; // How long it spawns before auto-stopping

    private float spawnTimer = 0f;
    private float totalLifetime = 0f;
    private int projectileIndex = 0;
    private bool isSpawning = true;

    private class SpiralProjectile
    {
        public Transform transform;
        public float angleOffset;
        public float radius;
        public float timeAlive;
    }

    private List<SpiralProjectile> activeProjectiles = new List<SpiralProjectile>();

    void Update()
    {
        // Timer to auto-stop spawning after a while
        if (isSpawning)
        {
            totalLifetime += Time.deltaTime;

            if (totalLifetime >= spawnerLifetime)
                isSpawning = false;

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnTimer = 0f;
                SpawnProjectile();
            }
        }

        // Always move existing projectiles
        MoveProjectilesOutward();

        // When all projectiles are gone and we're not spawning anymore, destroy this object
        if (!isSpawning && activeProjectiles.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    void SpawnProjectile()
    {
        float armOffset = (projectileIndex % spiralArms) * (360f / spiralArms);
        float angle = armOffset + (projectileIndex * spiralTightness);

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        SpiralProjectile newProj = new SpiralProjectile
        {
            transform = proj.transform,
            angleOffset = angle,
            radius = 0f,
            timeAlive = 0f
        };

        activeProjectiles.Add(newProj);
        projectileIndex++;
    }

    void MoveProjectilesOutward()
    {
        for (int i = activeProjectiles.Count - 1; i >= 0; i--)
        {
            var proj = activeProjectiles[i];

            if (proj.transform == null)
            {
                activeProjectiles.RemoveAt(i);
                continue;
            }

            proj.timeAlive += Time.deltaTime;
            if (proj.timeAlive > projectileLifetime)
            {
                Destroy(proj.transform.gameObject);
                activeProjectiles.RemoveAt(i);
                continue;
            }

            proj.radius += speed * Time.deltaTime;
            float rad = (proj.angleOffset + proj.radius * rotationSpeed) * Mathf.Deg2Rad;
            float x = Mathf.Cos(rad) * proj.radius;
            float y = Mathf.Sin(rad) * proj.radius;

            proj.transform.position = transform.position + new Vector3(x, y, 0f);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
