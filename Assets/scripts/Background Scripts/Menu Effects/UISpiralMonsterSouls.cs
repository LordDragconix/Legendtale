using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UISpiralSoulGenerator : MonoBehaviour
{
    public GameObject soulPrefab;  // Prefab for UI Image souls
    public int spiralArms = 4;     // Number of spiral arms (like galaxy arms)
    public float spawnRate = 0.1f; // Time between spawning new souls
    public float spiralTightness = 20f; // Controls spacing of the spiral
    public float speed = 200f;     // How fast the souls move outward
    public float rotationSpeed = 2f; // Speed of spiral rotation
    public float despawnDistance = 1000f; // Distance before souls get destroyed

    private List<SoulData> activeSouls = new List<SoulData>();
    private float spawnTimer = 0f;
    private int soulIndex = 0;

    private class SoulData
    {
        public RectTransform soulRect;
        public float angleOffset;
        public float radius;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0f;
            SpawnSoul();
        }

        MoveSoulsOutward();
    }

    void SpawnSoul()
    {
        float armOffset = (soulIndex % spiralArms) * (360f / spiralArms); // Each soul follows an arm
        float angle = armOffset + (soulIndex * spiralTightness);
        float rad = angle * Mathf.Deg2Rad;

        GameObject soul = Instantiate(soulPrefab, transform);
        RectTransform soulRect = soul.GetComponent<RectTransform>();

        soulRect.anchoredPosition = Vector2.zero; // Start at the center

        // Store soul data
        SoulData newSoul = new SoulData
        {
            soulRect = soulRect,
            angleOffset = angle,
            radius = 0f // Start at center, grows outward
        };

        activeSouls.Add(newSoul);
        soulIndex++; // Increase index for next spawn
    }

    void MoveSoulsOutward()
    {
        for (int i = activeSouls.Count - 1; i >= 0; i--)
        {
            SoulData soulData = activeSouls[i];

            // Increase radius (move outward)
            soulData.radius += speed * Time.deltaTime;

            // Calculate new position in a spiral motion
            float rad = (soulData.angleOffset + soulData.radius * rotationSpeed) * Mathf.Deg2Rad;
            float x = Mathf.Cos(rad) * soulData.radius;
            float y = Mathf.Sin(rad) * soulData.radius;

            soulData.soulRect.anchoredPosition = new Vector2(x, y);

            // Remove soul if too far off-screen
            if (soulData.radius > despawnDistance)
            {
                Destroy(soulData.soulRect.gameObject);
                activeSouls.RemoveAt(i);
            }
        }
    }
}
