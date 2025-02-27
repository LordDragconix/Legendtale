using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Soul : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float initialJumpForce = 5f;
    public float maxJumpForce = 10f;
    public float maxJumpTime = 0.5f;
    public float jumpForceIncreaseRate = 5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private float originalMoveSpeed;
    private bool isJumping = false;
    private bool isGrounded = false;
    private float jumpTimeCounter;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Move();
        Jump();
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 moveDirection = new Vector2(moveX, 0).normalized;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = originalMoveSpeed / 2;
        }
        else
        {
            moveSpeed = originalMoveSpeed;
        }

        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, initialJumpForce);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter < maxJumpTime)
            {
                jumpTimeCounter += Time.deltaTime;
                float currentJumpForce = Mathf.Lerp(initialJumpForce, maxJumpForce, jumpTimeCounter * jumpForceIncreaseRate / maxJumpTime);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, currentJumpForce);
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}