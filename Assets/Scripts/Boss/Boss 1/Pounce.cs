using System.Collections;
using UnityEngine;

public class Pounce : BossSkill
{
    public Transform player; 
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float preparationTime = 1f;
    [SerializeField] float pounceForce = 15f;
    [SerializeField] float pounceDuration = 0.6f;

    private Animator animator; 
    private Rigidbody2D rb; 

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        skillDamage = 30f;
        maxCooldown = 10f;
        skillRange = 10f;
        skillCastDuration = 2f;
    }
    private IEnumerator Pouncing()
    {
        animator.SetTrigger("Prepare");
        yield return new WaitForSeconds(preparationTime);

        IntoCooldown();

        Applier.skillDamage = skillDamage;
        Applier.castingDamage = true;

        animator.SetTrigger("Pounce");
        rb.velocity = new Vector2(transform.localScale.x * pounceForce, 0f);
        yield return new WaitForSeconds(pounceDuration);

        Applier.castingDamage = false;
    }
    public override void Play()
    {
        base.Play();

        StartCoroutine(Pouncing());
    }
}
