using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_Soul : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    private float originalMoveSpeed;

    private void Start()
    {
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        Move();
        Fire();
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

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = bulletSpawnPoint.up * bulletSpeed;
        }
    }
}