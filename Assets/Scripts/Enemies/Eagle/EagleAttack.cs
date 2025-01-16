using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAttack : EnemyAttack
{
    public Transform player;
    public EagleMovement movement;
    public float attackSpeedMulti;
    public Transform parent;
    public bool isTargeting;
    public Vector3 target;
    protected override void Start()
    {
        base.Start();
        attackDamage = 12f;
        attackRate = 4f;
        isTargeting = false;
    }
    protected override void Update()
    {
        base.Update();

        if (isPlayerInRange)
        {
            if (!isAttacking)
            {
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0f)
                {
                    isAttacking = true;
                    target = player.position;
                    isTargeting = true;
                    animator.SetTrigger("Attack");
                    sfxManager.Instance.PlaySound2D("Enemy_2");
                    rb.velocity = new Vector2((target.x - parent.position.x) * movement.speed * attackSpeedMulti, (target.y - parent.position.y) * movement.speed * attackSpeedMulti);
                }
                else
                {
                    animator.SetTrigger("Walk");
                    rb.velocity = new Vector2(0f, movement.speed * 0.5f);
                }
            }
        }
        if (isTargeting)
        {
            if (target == parent.position)
            {
                isTargeting = false;
                isAttacking = false;
            }
        }
    }
}
