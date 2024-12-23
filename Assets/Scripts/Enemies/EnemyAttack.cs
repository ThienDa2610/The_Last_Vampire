using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float attackDamage;     
    public float attackRate;
    public float attackDuration;
    public bool isAttacking;
    public Animator animator;

    protected virtual void Start()
    {
        animator = GetComponentInParent<Animator>();
        isAttacking = false;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines(); 
            isAttacking = false;
        }
    }

    protected virtual IEnumerator Attack()
    {
        return null;
    }
}
