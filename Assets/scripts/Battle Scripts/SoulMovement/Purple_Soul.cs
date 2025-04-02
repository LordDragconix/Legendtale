using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Soul : MonoBehaviour
{
    public bool smoothVerticalMovement = false;
    public float moveSpeed = 5f;
    public float SlowMovement = 0.5f;
    public string lineTag = "Line";

    private Vector3 direction;
    private bool isSprinting = false;
    private float moveThreshold = 3f; // Threshold distance before snapping
    private float moveAccumulator = 0f; // Accumulates the distance moved towards the threshold

    void Start()
    {
        SnapToNearestLine();
    }

    void Update()
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeed = isSprinting ? moveSpeed * SlowMovement : moveSpeed;

        if (smoothVerticalMovement)
        {
            // Horizontal movement snaps to lines
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                direction = Input.GetKeyDown(KeyCode.A) ? Vector3.left : Vector3.right;
                SnapToLine(direction);
            }

            // Smooth vertical movement
            float verticalInput = Input.GetAxis("Vertical");
            MoveVertically(verticalInput, currentSpeed);
        }
        else
        {
            // Vertical movement snaps to lines
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                direction = Input.GetKeyDown(KeyCode.W) ? Vector3.up : Vector3.down;
                SnapToLine(direction);
            }

            // Smooth horizontal movement
            float horizontalInput = Input.GetAxis("Horizontal");
            MoveHorizontally(horizontalInput, currentSpeed);
        }
    }

    private void MoveHorizontally(float input, float speed)
    {
        float distanceToMove = input * speed * Time.deltaTime;
        transform.Translate(distanceToMove, 0, 0);
        AccumulateMovement(distanceToMove);
    }

    private void MoveVertically(float input, float speed)
    {
        float distanceToMove = input * speed * Time.deltaTime;
        transform.Translate(0, distanceToMove, 0);
        AccumulateMovement(distanceToMove);
    }

    private void AccumulateMovement(float distance)
    {
        moveAccumulator += Mathf.Abs(distance);
        if (moveAccumulator >= moveThreshold)
        {
            moveAccumulator = 0f; // Reset accumulator
            // Optional: Perform additional actions once the threshold is reached, like snapping
        }
    }

    private void SnapToLine(Vector3 moveDirection)
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag(lineTag);
        GameObject closestLine = null;
        float closestDistance = Mathf.Infinity;

        foreach (var line in lines)
        {
            float distance = Vector3.Distance(transform.position, line.transform.position);
            if (distance < closestDistance && IsInMoveDirection(transform.position, line.transform.position, moveDirection))
            {
                closestLine = line;
                closestDistance = distance;
            }
        }

        if (closestLine != null)
        {
            transform.position = new Vector3(
                smoothVerticalMovement ? closestLine.transform.position.x : transform.position.x,
                smoothVerticalMovement ? transform.position.y : closestLine.transform.position.y,
                transform.position.z);
        }
    }

    private void SnapToNearestLine()
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag(lineTag);
        GameObject closestLine = null;
        float closestDistance = Mathf.Infinity;

        foreach (var line in lines)
        {
            float distance = Vector3.Distance(transform.position, line.transform.position);
            if (distance < closestDistance)
            {
                closestLine = line;
                closestDistance = distance;
            }
        }

        if (closestLine != null)
        {
            transform.position = new Vector3(
                smoothVerticalMovement ? closestLine.transform.position.x : transform.position.x,
                smoothVerticalMovement ? transform.position.y : closestLine.transform.position.y,
                transform.position.z);
        }
    }

    // Checks if the target is in the desired move direction
    private bool IsInMoveDirection(Vector3 currentPosition, Vector3 targetPosition, Vector3 direction)
    {
        if (direction == Vector3.up)
            return targetPosition.y > currentPosition.y;
        if (direction == Vector3.down)
            return targetPosition.y < currentPosition.y;
        if (direction == Vector3.left)
            return targetPosition.x < currentPosition.x;
        if (direction == Vector3.right)
            return targetPosition.x > currentPosition.x;

        return false;
    }
}