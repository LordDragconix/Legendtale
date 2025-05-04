using UnityEngine;

public class OrangeFistChaseSpawner : MonoBehaviour
{
    public GameObject chaseFistPrefab;
    public Transform spawnPoint;

    private GameObject spawnedFist;

    void Start()
    {
        spawnedFist = Instantiate(chaseFistPrefab, spawnPoint.position, Quaternion.identity, transform); // 🔥 Make it a child of this spawner
    }
}
