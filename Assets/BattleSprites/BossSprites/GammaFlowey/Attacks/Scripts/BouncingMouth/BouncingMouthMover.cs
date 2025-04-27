using UnityEngine;

public class BouncingMouthMover : MonoBehaviour
{
    private Transform[] points;
    private int currentIndex = 0;

    public float moveSpeed = 5f;

    private bool hasStarted = false;
    private bool isDespawning = false;

    public void SetPoints(Transform[] bouncePoints)
    {
        points = bouncePoints;

        if (points == null || points.Length == 0)
        {
            Debug.LogWarning("No bounce points found. Destroying mouth.");
            Destroy(gameObject);
            return;
        }

        currentIndex = Random.Range(0, points.Length);
        transform.position = points[currentIndex].position;
        currentIndex = (currentIndex + 1) % points.Length;

        Invoke(nameof(EnableMovement), 0.1f);
    }

    void EnableMovement()
    {
        hasStarted = true;
    }

    void Update()
    {
        if (!hasStarted || isDespawning) return;

        if (points == null || points.Length == 0)
        {
            Debug.LogWarning("Bounce points missing during update. Destroying mouth.");
            Destroy(gameObject);
            return;
        }

        Transform target = points[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentIndex = (currentIndex + 1) % points.Length;
        }
    }

    public void Despawn()
    {
        isDespawning = true;
        Destroy(gameObject);
    }
}
