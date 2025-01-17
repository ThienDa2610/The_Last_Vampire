using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : EnemyAttack
{
    public GameObject pf_energyBlast;
    public GameObject parent;
    protected override void Start()
    {
        base.Start();
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
        sfxManager.Instance.PlaySound2D("Enemy5");
        yield return new WaitForSeconds(attackDuration);

        GameObject energyBlast = Instantiate(pf_energyBlast,transform.position, Quaternion.identity);
        energyBlast.transform.localScale = parent.transform.localScale;
        energyBlast.GetComponent<EnergyBlast>().shooter = parent;

        isAttacking = false;
        attackTimer = attackRate;
    }
}
