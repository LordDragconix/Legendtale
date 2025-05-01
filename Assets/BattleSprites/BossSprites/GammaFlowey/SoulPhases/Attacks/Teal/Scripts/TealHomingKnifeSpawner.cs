using UnityEngine;

public class TealHomingKnifeSpawner : MonoBehaviour
{
    public GameObject knifePrefab;
    public int minKnives = 1;
    public int maxKnives = 3;
    public float spawnRadius = 5f;

    void Start()
    {
        SpawnKnives();
    }

    public void SpawnKnives()
    {
        int knifeCount = Random.Range(minKnives, maxKnives + 1);

        for (int i = 0; i < knifeCount; i++)
        {
            Vector2 spawnPos = (Vector2)FindObjectOfType<Player>().transform.position + Random.insideUnitCircle.normalized * spawnRadius;
            GameObject knife = Instantiate(knifePrefab, spawnPos, Quaternion.identity, this.transform); // 🔥 Parent under spawner

            // Knife will now be a child of the spawner
        }
    }
}
