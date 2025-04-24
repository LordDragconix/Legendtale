using UnityEngine;

public class BombDrop : MonoBehaviour
{
    public float fallSpeed = 5f;
    public GameObject explosionPrefab;
    public float yDestroyThreshold = -5f; // y position where the bomb explodes

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y <= yDestroyThreshold)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
