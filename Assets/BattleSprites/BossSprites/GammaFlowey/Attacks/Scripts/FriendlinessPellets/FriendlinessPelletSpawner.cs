using UnityEngine;

public class FriendlinessPelletSpawner : MonoBehaviour
{
    public GameObject pelletPrefab;
    public int pelletCount = 12;
    public float radius = 5f;
    public float pelletSpeed = 5f;
    public float spawnDelay = 2f;

    public AudioClip pelletSpawnSFX; // 🎵 Drag your sound file here in the Inspector
    private AudioSource audioSource;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("No player found with tag 'Player'.");
            enabled = false;
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        InvokeRepeating(nameof(SpawnPellets), 0f, spawnDelay);
    }

    void SpawnPellets()
    {
        if (pelletSpawnSFX != null)
        {
            audioSource.PlayOneShot(pelletSpawnSFX);
        }

        Vector3 playerPos = player.position;

        for (int i = 0; i < pelletCount; i++)
        {
            float angle = i * Mathf.PI * 2 / pelletCount;
            Vector3 spawnPos = playerPos + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            GameObject pellet = Instantiate(pelletPrefab, spawnPos, Quaternion.identity);
            var script = pellet.GetComponent<FriendlinessPellet>();
            if (script != null)
            {
                Vector3 dirToPlayer = playerPos - spawnPos;
                script.speed = pelletSpeed;
                script.SetDirection(dirToPlayer);
            }
        }
    }
}
