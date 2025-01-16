using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnergyBlast : BossSkill
{
    public GameObject pf_energyBlast;
    public float prepTime = 0.15f;
    public float yOffset = 1f;
    protected override void Start()
    {
        base.Start();
        skillDamage = 0f;
        maxCooldown = 0f;
        skillRange = 20f;
        isFacingRight = true;
    }
    private IEnumerator ShootingEnergyBlast()
    {
        animator.SetTrigger("isShooting");
        sfxManager.Instance.PlaySound2D("Boss_2_2");
        yield return new WaitForSeconds(prepTime);

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z);
        GameObject energyBlast = Instantiate(pf_energyBlast, spawnPosition, Quaternion.identity);
        energyBlast.transform.localScale = transform.localScale;
        energyBlast.GetComponent<EnergyBlast>().shooter = gameObject;

        skillManager.isCastingSkill = false;
    }
    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(ShootingEnergyBlast());
        IntoCooldown();
    }
}
