using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : EnemyAttack
{
    protected override void Start()
    {
        base.Start();
        attackDamage = 15f;
<<<<<<< HEAD
        attackRate = 1f;
    }
    protected override void Update()
    {
        base.Update();

        if (isPlayerInRange)
        {
            if (isAttacking)
            {
                if (animator.GetFloat("AttackHit") > 0f)
                {
                    HealthManager.Instance.takeDamage(attackDamage, transform.parent.gameObject);
                }
                if (animator.GetFloat("AttackFinish") > 0f)
                {
                    attackTimer = attackRate;
                    isAttacking = false;
                }
            }
            else
            {
                attackTimer -= Time.deltaTime;
                if (attackTimer < 0f)
                {
                    isAttacking = true;
                    animator.SetTrigger("Attack");
                }
            }
=======
        attackDuration = 0.4f;
        attackRate = 1f;
    }
    protected override IEnumerator Attack()
    {
        
        while (true)
        {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(attackDuration);
            HealthManager.Instance.takeDamage(attackDamage, transform.parent.gameObject);
            yield return new WaitForSeconds(attackRate);
>>>>>>> BaoDi
        }
    }
}
