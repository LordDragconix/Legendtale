using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class TealHomingKnife : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float reaimInterval = 1.5f; // Time between re-aims

    private Rigidbody2D rb;
    private Transform player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>()?.transform;

        if (player != null)
        {
            FireAtPlayer();
            StartCoroutine(ReaimForever());
        }
    }

    void FireAtPlayer()
    {
        if (player == null)
            return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed; // 🔥 Correct movement (Unity 6 style)

        // 🔥 Rotate to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // 🔥 90 degree correction for sprite facing upwards
    }


    IEnumerator ReaimForever()
    {
        while (true) // 🔥 Infinite loop until knife is destroyed
        {
            yield return new WaitForSeconds(reaimInterval);
            FireAtPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KnifeDestroyer"))
        {
            Destroy(gameObject);
        }
    }
}
