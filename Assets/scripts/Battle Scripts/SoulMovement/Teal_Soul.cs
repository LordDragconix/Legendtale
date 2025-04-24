using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teal_Soul : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float detectionRadius = 5f;
    private float originalMoveSpeed;
    private bool canMove = false;

    private void Start()
    {
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        DetectEnemies();
        if (canMove)
        {
            Move();
        }
    }

    void DetectEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        canMove = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Attack"))
            {
                canMove = true;
                break;
            }
        }
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = originalMoveSpeed / 2;
        }
        else
        {
            moveSpeed = originalMoveSpeed;
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}