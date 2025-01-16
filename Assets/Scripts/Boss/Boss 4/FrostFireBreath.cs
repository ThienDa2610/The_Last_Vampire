using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostFireBreath : BossSkill
{
    public GameObject[] breaths;
    public float prepTime = 0.15f;
    public float yOffset = 1.5f;
    public float xOffset = 5.5f;
    protected override void Start()
    {
        base.Start();
        maxCooldown = 0f;
        skillRange = 20f;
        isFacingRight = true;
    }
    private IEnumerator DragonBreath()
    {
        int breathIndex = Random.Range(0, breaths.Length);
        animator.SetTrigger("isSpitting");
        sfxManager.Instance.PlaySound2D("Boss_4_1");
        yield return new WaitForSeconds(prepTime);
        Vector3 spawnPosition = new Vector3(transform.position.x + xOffset * transform.localScale.x, transform.position.y + yOffset, transform.position.z);
        GameObject breath = Instantiate(breaths[breathIndex], spawnPosition, Quaternion.identity);
        breath.transform.localScale = transform.localScale;
        breath.GetComponent<EnergyBlast>().shooter = gameObject;
        breath.GetComponent<EnergyBlast>().isFalling = false;

        skillManager.isCastingSkill = false;
    }
    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(DragonBreath());
        IntoCooldown();
    }
}
