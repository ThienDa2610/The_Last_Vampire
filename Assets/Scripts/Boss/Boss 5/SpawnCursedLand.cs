using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCursedLand : BossSkill
{
    public GameObject pf_cursedLand;
    public float prepTime = 0.15f;
    public float yOffset = -2.8f;
    protected override void Start()
    {
        base.Start();
        maxCooldown = 20f;
        skillRange = 25f;
        isFacingRight = false;
    }
    private IEnumerator CurseLand(Transform target)
    {
        animator.SetTrigger("Casting");
        sfxManager.Instance.PlaySound2D("Boss_5_2");
        yield return new WaitForSeconds(prepTime);
        Vector3 spawnPosition = new Vector3(target.position.x, transform.position.y + yOffset, transform.position.z);
        GameObject cursedLand = Instantiate(pf_cursedLand,spawnPosition,Quaternion.identity);
        cursedLand.GetComponent<CursedLand>().duration = 8f;

        skillManager.isCastingSkill = false;
    }
    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(CurseLand(target));
        IntoCooldown();
    }
}
