using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    public Transform playerTransform; // Assign the Transform of the player's sprite
    public BoxCollider2D boundaryBox; // Assign the BoxCollider2D of the boundary box

    private Vector2 boxMin;
    private Vector2 boxMax;

    void Start()
    {
        // Get the min and max points of the box from the BoxCollider2D bounds
        Bounds bounds = boundaryBox.bounds;
        boxMin = bounds.min;
        boxMax = bounds.max;
    }

    void Update()
    {
        // Get the size of the player sprite to adjust the boundaries
        Vector2 playerSize = playerTransform.GetComponent<SpriteRenderer>().bounds.size;

        // Calculate the boundaries for the player's position
        float minX = boxMin.x + playerSize.x / 2;
        float maxX = boxMax.x - playerSize.x / 2;
        float minY = boxMin.y + playerSize.y / 2;
        float maxY = boxMax.y - playerSize.y / 2;

        // Clamp the player's position within the box boundaries
        Vector3 clampedPosition = playerTransform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        // Apply the clamped position
        playerTransform.position = clampedPosition;
    }
}