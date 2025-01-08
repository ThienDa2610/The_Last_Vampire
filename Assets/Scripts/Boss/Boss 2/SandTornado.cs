using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTornado : BossSkill
{
    public float damageTimer = 0f;
    public float damageRate;
    public bool isSpinning = false;
    public float spinDuration;
    public bool shouldChase = true;
    EnemyHealthManager healthManager;
    private void Awake()
    {
        healthManager = GetComponent<EnemyHealthManager>();
    }
    protected override void Start()
    {
        base.Start();
        skillDamage = 15f;
        maxCooldown = 15f;
        skillRange = 2f;
        spinDuration = 5f;
        damageRate = 0.5f;
        isFacingRight = true;
    }
    protected override void Update()
    {
        base.Update();
        if (shouldChase && isSpinning)
        {
            Debug.Log("SandStorm chasing");
            skillManager.AdjustDirection(skillManager.player);
            skillManager.ApproachPlayer();
        }
    }
    private IEnumerator TornadoSpin()
    {
        animator.SetBool("isSpinning", true);
        isSpinning = true;
        healthManager.isInvincible = true;

        yield return new WaitForSeconds(spinDuration);

        animator.SetBool("isSpinning", false);
        isSpinning = false;
        healthManager.isInvincible = false;

        skillManager.isCastingSkill = false;
    }
    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(TornadoSpin());
        IntoCooldown();
    }
}
