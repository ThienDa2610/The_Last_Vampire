using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostFireMeteor : BossSkill
{
    public GameObject[] breaths;
    public float ascendDelay = 1f;
    public int numberOfMeteor = 30;
    public float playerRadius = 5f;
    public float meteorDelay = 0.2f;
    public float meteorFallDelay = 3f;
    protected override void Start()
    {
        base.Start();
        maxCooldown = 30f;
        skillRange = 25f;
        isFacingRight = true;
    }
    private IEnumerator DragonMeteor(Transform target)
    {
        animator.SetTrigger("isAscending");
        sfxManager.Instance.PlaySound2D("Boss_4_3");
        rb.velocity = new Vector2(0f, 20f);
        yield return new WaitForSeconds(ascendDelay);

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;

        for (int i = 0; i < numberOfMeteor; i++)
        {
            int breathIndex = Random.Range(0, breaths.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(target.position.x - playerRadius, target.position.x + playerRadius), transform.position.y, transform.position.z);
            GameObject breath = Instantiate(breaths[breathIndex],spawnPosition,Quaternion.identity);
            breath.GetComponent<EnergyBlast>().isFalling = true;
            breath.GetComponent<Rigidbody2D>().gravityScale = 0.5f;

            yield return new WaitForSeconds(meteorDelay);
        }
        yield return new WaitForSeconds(meteorFallDelay);
        rb.velocity = new Vector2(0f, -20f);
        yield return new WaitForSeconds(ascendDelay);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1f;
        skillManager.isCastingSkill = false;
    }
    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(DragonMeteor(target));
        IntoCooldown();
    }
}
