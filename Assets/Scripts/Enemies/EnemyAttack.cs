using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float attackDamage = 10f;     
    public float damageRate = 1f;
    public bool isAttacking;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            animator.SetBool("Attack", true);
            StartCoroutine(ApplyDamageOverTime());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines(); 
            isAttacking = false;
            animator.SetBool("Attack", false);
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        isAttacking = true;
        while (true)
        {
            HealthManager.Instance.takeDamage(attackDamage);
            yield return new WaitForSeconds(damageRate);
        }
    }
}
