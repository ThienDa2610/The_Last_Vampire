using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{
    // How long this state should be active for before moving on
    public float duration;
    // bool to check whether or not the next attack in the sequence should be played or not
    protected bool shouldCombo;
    // The attack index in the sequence of attacks
    protected int attackIndex;
    // Cached already struck objects of said attack to avoid overlapping attacks on same target
    private List<Collider2D> collidersDamaged;


    //skill tree
    // //lifesteal
    public static float damage;
    public static bool lifeSteal = false;
    private float lifeStealPercent = 0.1f;
    // //blood lost
    public static bool Tear = false;
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        collidersDamaged = new List<Collider2D>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (StateMachine.animator.GetFloat("WeaponActive") > 0f)
        {
            Attack();
        }

        if (StateMachine.animator.GetFloat("AttackWindowOpen") > 0f && Input.GetKeyDown(KeyCode.J))
        {
            shouldCombo = true;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    protected void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(PlayerMeleeCombo.Instance.hitbox, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {

            if (!collidersDamaged.Contains(collidersToDamage[i]))
            {
                if (collidersToDamage[i].CompareTag("Enemy"))
                {
                    //Debug.Log(collidersToDamage[i].gameObject.name);
                    collidersToDamage[i].gameObject.GetComponent<EnemyHealthManager>().TakeDamage(damage);
                    //vampiric claws
                    if (lifeSteal)
                    {
                        HealthManager.Instance.Heal(damage * lifeStealPercent);
                    }
                    //tear
                    if (Tear || true)
                    {
                        collidersToDamage[i].gameObject.GetComponent<EnemyHealthManager>().InflictBloodLost();
                    }
                    collidersDamaged.Add(collidersToDamage[i]);
                }
            }
        }
    }
}
