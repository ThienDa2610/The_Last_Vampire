using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAttackPoint : MonoBehaviour
{
    public EagleAttack attacker;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && attacker.isAttacking)
        {
            Debug.Log("Attack hit");
            HealthManager.Instance.takeDamage(attacker.attackDamage, gameObject);
            Debug.Log("Attacking off");
            attacker.attackTimer = attacker.attackRate;
            attacker.isAttacking = false;
        }
    }
}
