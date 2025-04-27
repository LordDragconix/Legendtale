using System.Collections.Generic;
using UnityEngine;

public class TealKnifeCircle : MonoBehaviour
{
    [Header("Knife Settings")]
    public GameObject knifePrefab;
    public int minKnives = 6;
    public int maxKnives = 12;
    public float spawnRadius = 5f;
    public float moveSpeed = 2f;
    public float spinSpeed = 90f; // Degrees per second

    private List<KillerKnife> activeKnives = new List<KillerKnife>();
    private Vector3 playerPositionAtSpawn;

    private void Start()
    {
        playerPositionAtSpawn = GameObject.FindGameObjectWithTag("Player").transform.position;
        SpawnKnives();
    }

    private void Update()
    {
        foreach (var knife in activeKnives)
        {
            if (knife != null)
            {
                knife.UpdateKnife(moveSpeed, spinSpeed);
            }
        }
    }

    private void SpawnKnives()
    {
        Vector3 centrePoint = playerPositionAtSpawn;

        int knifeCount = Random.Range(minKnives, maxKnives + 1);
        float angleStep = 360f / knifeCount;

        for (int i = 0; i < knifeCount; i++)
        {
            float angle = i * angleStep;
            Vector3 spawnPos = centrePoint + Quaternion.Euler(0, 0, angle) * Vector3.up * spawnRadius;
            GameObject knife = Instantiate(knifePrefab, spawnPos, Quaternion.identity, transform);

            KillerKnife killer = new KillerKnife(knife.transform, angle, spawnRadius, centrePoint);
            activeKnives.Add(killer);
        }
    }

    private class KillerKnife
    {
        public Transform transform;
        private float currentAngle;
        private float currentRadius;
        private Vector3 targetCentre;

        public KillerKnife(Transform transform, float startAngle, float startRadius, Vector3 targetCentre)
        {
            this.transform = transform;
            this.currentAngle = startAngle;
            this.currentRadius = startRadius;
            this.targetCentre = targetCentre;
        }

        public void UpdateKnife(float moveSpeed, float spinSpeed)
        {
            if (transform == null) return;

            // Spin around the centre
            currentAngle += spinSpeed * Time.deltaTime;
            currentAngle %= 360f;

            // Move closer by shrinking the radius
            currentRadius -= moveSpeed * Time.deltaTime;

            // If the knife reaches the centre, destroy it
            if (currentRadius <= 0f)
            {
                Object.Destroy(transform.gameObject);
                transform = null;
                return;
            }

            // Update position based on radius and angle
            Vector3 offset = new Vector3(Mathf.Sin(currentAngle * Mathf.Deg2Rad), Mathf.Cos(currentAngle * Mathf.Deg2Rad), 0) * currentRadius;
            transform.position = targetCentre + offset;

            // Rotate to face the centre (top of sprite points towards centre)
            Vector3 lookDir = targetCentre - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}