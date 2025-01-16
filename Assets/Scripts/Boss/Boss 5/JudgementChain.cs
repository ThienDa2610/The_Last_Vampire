using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementChain : BossSkill
{
    Collider2D clawHitbox;
    public SinisterGaze sinisterGaze;
    [SerializeField] float clawDuration = 0.7f;
    private void Awake()
    {
        sinisterGaze = GetComponent<SinisterGaze>();
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

    private IEnumerator ChainOfJudgement(Transform target)
    {
        animator.SetTrigger("Attack");
        sfxManager.Instance.PlaySound2D("Boss_5_3");
        yield return new WaitForSeconds(clawDuration);

        IntoCooldown();

        ApplyDamage(target);

        skillManager.isCastingSkill = false;

    }

    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(ChainOfJudgement(target));
    }
    private void ApplyDamage(Transform target)
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(clawHitbox, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {
            if (collidersToDamage[i].CompareTag("Player"))
            {
                if (HealthManager.Instance.takeDamage(skillDamage, gameObject) == 2)
                {
                    sinisterGaze.Play(target);
                }
            }
        }
    }
}
