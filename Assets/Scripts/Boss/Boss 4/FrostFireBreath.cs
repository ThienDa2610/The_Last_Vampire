using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostFireBreath : BossSkill
{
    public GameObject[] breaths;
    public float prepTime = 0.15f;
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
        yield return new WaitForSeconds(prepTime);

        GameObject breath = Instantiate(breaths[breathIndex],transform.position,Quaternion.identity) as GameObject;
        breath.transform.localScale = transform.localScale;
        breath.GetComponent<EnergyBlast>().shooter = gameObject;

        skillManager.isCastingSkill = false;
    }
    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(DragonBreath());
        IntoCooldown();
    }
}
