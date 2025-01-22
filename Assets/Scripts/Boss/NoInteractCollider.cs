using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInteractCollider : MonoBehaviour
{
    Collider2D collider;
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            Physics2D.IgnoreCollision(collision.collider, collider);
    }
}
