using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    Rigidbody2D rb;
    CaveWorm_Cocoon cocoon;
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        cocoon = GetComponentInParent<CaveWorm_Cocoon>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.gravityScale = 2f;
            cocoon.isFalling = true;
        }
    }
}
