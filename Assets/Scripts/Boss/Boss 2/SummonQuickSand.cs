using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonQuickSand : BossSkill
{
    public GameObject pf_quickSand;
    public float prepTime = 0.5f;
    public int numberOfQuickSand = 3;
    [SerializeField] private float summonRadius = 10f;
    [SerializeField] private float summonYOffset = 1f;
    protected override void Start()
    {
        base.Start();
        skillDamage = 0f;
        maxCooldown = 20f;
        skillRange = 10f;
        isFacingRight = true;
    }
    private IEnumerator Summoning()
    {
        animator.SetTrigger("isCasting");
        sfxManager.Instance.PlaySound2D("Boss_2_3");
        yield return new WaitForSeconds(prepTime);

        float maxRange = transform.position.x + summonRadius * transform.localScale.x;
        for (int i = 0; i < numberOfQuickSand; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(maxRange, transform.position.x), transform.position.y - summonYOffset);
            GameObject quickSand = Instantiate(pf_quickSand, spawnPosition, Quaternion.identity);
            quickSand.GetComponent<QuickSand>().movement = skillManager.player.GetComponent<Movement>();
        }

        skillManager.isCastingSkill = false;
    }
    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(Summoning());
        IntoCooldown();
    }

}
