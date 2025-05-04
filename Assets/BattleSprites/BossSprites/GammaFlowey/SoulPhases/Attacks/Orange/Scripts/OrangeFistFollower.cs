using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OrangeFistFollower : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = FindObjectOfType<Player>()?.transform;

        if (player == null)
        {
            Debug.LogWarning("No object with 'Player' script found!");
            enabled = false;
            return;
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;

            // 🔁 Rotate toward the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // -90 if the fist sprite faces up
        }
    }
}
