using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscendedShoot : BossSkill
{
    public GameObject pf_ascendedWave;
    public float prepTime = 0.15f;
    public float yOffset = 1f;
    protected override void Start()
    {
        base.Start();
        skillDamage = 0f;
        maxCooldown = 10f;
        skillRange = 20f;
        isFacingRight = true;
    }
    private IEnumerator ShootingAscendedWave()
    {
        animator.SetTrigger("Casting");
        yield return new WaitForSeconds(prepTime);

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        GameObject energyBlast = Instantiate(pf_ascendedWave, spawnPosition, Quaternion.identity);
        Vector3 newScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        energyBlast.transform.localScale = newScale;
        energyBlast.GetComponent<EnergyBlast>().shooter = gameObject;

        skillManager.isCastingSkill = false;
    }
    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(ShootingAscendedWave());
        IntoCooldown();
    }
}
