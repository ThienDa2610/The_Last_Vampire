using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : EnemyAttack
{
    protected override void Start()
    {
        base.Start();
        attackDamage = 15f;
        attackRate = 1f;
        attackDuration = 0.4f;
    }
    protected override void Update()
    {
        base.Update();
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        if (isPlayerInRange)
        {
            if (attackTimer <= 0 && !isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }
    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDuration);
        HealthManager.Instance.takeDamage(attackDamage, transform.parent.gameObject);

        isAttacking = false;
        attackTimer = attackRate;
    }
}
