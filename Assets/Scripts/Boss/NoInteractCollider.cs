using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInteractCollider : MonoBehaviour
{
    Collider2D bossCollider;
    private void Awake()
    {
        bossCollider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            Physics2D.IgnoreCollision(collision.collider, bossCollider);
    }
}
