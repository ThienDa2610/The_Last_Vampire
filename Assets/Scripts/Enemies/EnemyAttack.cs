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

    protected virtual void Start()
    {
        animator = GetComponentInParent<Animator>();
        isPlayerInRange = false;
        isAttacking = false;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            isAttacking = false;
        }
    }

    protected virtual void Update()
    {
        
    }
}
