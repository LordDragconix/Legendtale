using UnityEngine;

public class FriendlinessPellet : MonoBehaviour
{
    private Vector3 moveDirection;
    public float speed = 5f;
    public float delayBeforeMove = 1f;
    public float lifetime = 10f;
    public float rotationSpeed = 180f; // Degrees per second

    private bool shouldMove = false;

    public void SetDirection(Vector3 dir)
    {
        moveDirection = dir.normalized;
        Invoke(nameof(EnableMovement), delayBeforeMove);
        Destroy(gameObject, lifetime);
    }

    void EnableMovement()
    {
        shouldMove = true;
    }

    void Update()
    {
        // Spin the pellet visually
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // Move after delay
        if (shouldMove)
            transform.position += moveDirection * speed * Time.deltaTime;
    }
}
