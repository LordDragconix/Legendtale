using UnityEngine;

public class RotateSouls : MonoBehaviour
{
    public Transform centerPoint;  // The logo's position
    public float maxRadius = 120f; // The final orbit radius
    public float expansionSpeed = 100f; // How fast they expand outward
    public float bobAmount = 20f;  // Maximum bobbing movement
    public float bobSpeed = 2f;    // Speed of bobbing motion
    public float rotationSpeed = 30f;
    public float bobTransitionSpeed = 1f; // How fast bobbing effect increases

    private float currentRadius = 0f; // Start at 0 radius
    private float angleOffset;
    private bool hasReachedMaxRadius = false;
    private float bobStrength = 0f; // Starts at 0, gradually increases

    void Update()
    {
        angleOffset += rotationSpeed * Time.deltaTime;

        // Expand smoothly using MoveTowards instead of Lerp
        currentRadius = Mathf.MoveTowards(currentRadius, maxRadius, expansionSpeed * Time.deltaTime);

        // Check if we reached the max radius
        if (!hasReachedMaxRadius && Mathf.Abs(currentRadius - maxRadius) < 0.5f)
        {
            hasReachedMaxRadius = true; // Allow bobbing once close to maxRadius
        }

        // Gradually transition into bobbing instead of snapping
        if (hasReachedMaxRadius)
        {
            bobStrength = Mathf.MoveTowards(bobStrength, 1f, Time.deltaTime * bobTransitionSpeed);
        }

        // Apply bobbing, starting at 0 and smoothly increasing
        float bobbing = Mathf.Sin(Time.time * bobSpeed) * bobAmount * bobStrength;

        for (int i = 0; i < transform.childCount; i++)
        {
            float angle = angleOffset + (i * 60f); // 360 degrees divided by 6 souls
            float rad = angle * Mathf.Deg2Rad;
            float dynamicRadius = currentRadius + bobbing;  // Bobbing smoothly phases in

            Vector3 newPos = new Vector3(Mathf.Cos(rad) * dynamicRadius, Mathf.Sin(rad) * dynamicRadius, 0);
            transform.GetChild(i).localPosition = newPos;
        }
    }
}
