using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : EnemyAttack
{
    protected override void Start()
    {
        base.Start();
        attackDamage = 15f;
        attackDuration = 0.4f;
        attackRate = 1f;
    }
    protected override IEnumerator Attack()
    {
        
        while (true)
        {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(attackDuration);
            HealthManager.Instance.takeDamage(attackDamage);
            yield return new WaitForSeconds(attackRate);
        }
    }
}
