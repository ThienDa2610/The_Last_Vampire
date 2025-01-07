using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float attackDamage;     
    public float attackRate;
    public float attackDuration;
    public bool isAttacking;
    public bool isPlayerInRange;
    public float attackTimer;
    public Animator animator;
    public Rigidbody2D rb;

    protected virtual void Start()
    {
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        isPlayerInRange = false;
        isAttacking = false;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            rb.velocity = Vector2.zero;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Exit trigger collider");
            isPlayerInRange = false;
            isAttacking = false;
        }
    }

    protected virtual void Update()
    {
        
    }
}
