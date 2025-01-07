using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissile : BossSkill
{
    public GameObject pf_Missile;
    public int numberOfMissile = 3;
    public float prepTime = 0.5f;
    public float yOffset = 1f;
    protected override void Start()
    {
        base.Start();
        skillDamage = 0f;
        maxCooldown = 15f;
        skillRange = 20f;
        isFacingRight = true;
    }
    private IEnumerator FiringMissile()
    {
        animator.SetTrigger("isFiring");
        for (int i = 0; i < numberOfMissile; i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(prepTime);
            StartFireMissile();
        }
        skillManager.isCastingSkill = false;
    }
    private void StartFireMissile()
    {
        Debug.Log("Fire!");
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z);
        GameObject missile = Instantiate(pf_Missile, spawnPosition, Quaternion.identity);
        missile.transform.localScale = transform.localScale;
        missile.GetComponent<EnergyBlast>().shooter = gameObject;
    }
    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(FiringMissile());
        IntoCooldown();
    }
}
