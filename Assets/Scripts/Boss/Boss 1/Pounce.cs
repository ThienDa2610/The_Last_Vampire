using System.Collections;
using UnityEngine;

public class Pounce : BossSkill
{
    [SerializeField] float preparationTime = 1f;
    [SerializeField] float pounceForce = 5f;
    [SerializeField] float pounceDuration = 0.4f;
    protected Collider2D Hitbox;
    protected ApplySkillDamage Applier;


    protected override void Start()
    {
        base.Start();
        Hitbox = transform.Find("Hitbox").GetComponent<Collider2D>();
        Applier = transform.Find("Hitbox").GetComponent<ApplySkillDamage>();
        skillDamage = 30f;
        maxCooldown = 8f;
        skillRange = 10f;
        isFacingRight = true;
        Applier.castingDamage = false;
    }
    private IEnumerator Pouncing()
    {
        animator.SetTrigger("Prepare");
        yield return new WaitForSeconds(preparationTime);


        Applier.skillDamage = skillDamage;
        Applier.castingDamage = true;

        animator.SetTrigger("Pounce");
        sfxManager.Instance.PlaySound2D("Boss1_1");
        rb.velocity = new Vector2(transform.localScale.x * pounceForce, 0f);
        yield return new WaitForSeconds(pounceDuration);

        rb.velocity = Vector2.zero;
        Applier.castingDamage = false;
        skillManager.isCastingSkill = false;
    }
    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(Pouncing());
        IntoCooldown();
    }
}
