using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TealLargeKnife : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 direction = Vector2.left;
    public float lifetime = 5f; // New: time before auto-destroy

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.linearVelocity = direction * moveSpeed;
        Destroy(gameObject, lifetime); // Automatically destroy after 'lifetime' seconds
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KnifeDestroyer"))
        {
            Destroy(gameObject);
        }
    }
}
