using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LurkChecker : MonoBehaviour
{
    bool isLurking = true;
    WormMovement movement;
    Rigidbody2D rb;
    private void Start()
    {
        movement = GetComponentInParent<WormMovement>();
        rb = GetComponentInParent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isLurking = false;
            rb.gravityScale = 1f;

        }
        else if (collision.gameObject.layer == LayerMask.GetMask("Ground") && !isLurking)
        {
            movement.isActive = true;
        }
    }
}
