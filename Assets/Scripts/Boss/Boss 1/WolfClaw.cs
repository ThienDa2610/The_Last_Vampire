using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfClaw : BossSkill
{
    Collider2D clawHitbox;
    [SerializeField] float clawDuration = 0.7f;
    private void Awake()
    {
        clawHitbox = transform.Find("ClawHitbox").GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //skillDamage = 20f;
        maxCooldown = 0f;
        skillRange = 2f;
        isFacingRight = true;
    }

    private IEnumerator ClawAttack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(clawDuration);

        IntoCooldown();

        ApplyDamage();

        skillManager.isCastingSkill = false;

    }

    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(ClawAttack());
    }
    private void ApplyDamage()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(clawHitbox, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {
            if (collidersToDamage[i].CompareTag("Player"))
            {
                HealthManager.Instance.takeDamage(skillDamage, gameObject);
            }
        }
    }
}
