using System.Collections;
using UnityEngine;

public class Pounce : BossSkill
{
    //[SerializeField] float maxDistance = 10f;
    [SerializeField] float preparationTime = 1f;
    [SerializeField] float pounceForce = 5f;
    [SerializeField] float pounceDuration = 0.4f;

    protected override void Start()
    {
        base.Start();
        skillDamage = 30f;
        maxCooldown = 8f;
        skillRange = 10f;
        isFacingRight = true;
    }
    private IEnumerator Pouncing()
    {
        animator.SetTrigger("Prepare");
        yield return new WaitForSeconds(preparationTime);


        Applier.skillDamage = skillDamage;
        Applier.castingDamage = true;

        animator.SetTrigger("Pounce");
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
