using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Soul : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float originalMoveSpeed;

    private void Start()
    {
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        Move();
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
}