using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaserBeam : BossSkill
{
    public GameObject pf_laserBeam;
    public float prepTime = 0.15f;
    public float yOffset = 1f;
    public float xOffset = 5f;
    public int maxLaserBeam = 5;
    public LayerMask groundLayer;
    protected override void Start()
    {
        base.Start();
        skillDamage = 0f;
        maxCooldown = 10f;
        skillRange = 20f;
        isFacingRight = true;
    }
    private IEnumerator ShootingLaserBeam()
    {
        animator.SetTrigger("isShooting");
        yield return new WaitForSeconds(prepTime);
        StartShootLaserBeam();
        skillManager.isCastingSkill = false;
    }
    private void StartShootLaserBeam()
    {
        for (int i = 1; i <= maxLaserBeam; i++)
        {
            RaycastHit2D groundHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - yOffset), (transform.localScale.x > 0) ? Vector2.right : Vector2.left, i * xOffset, groundLayer);
            if (groundHit)
                break;
            Vector3 spawnPosition = new Vector3(transform.position.x + i * xOffset, transform.position.y - yOffset, transform.position.z);
            GameObject laserBeam = Instantiate(pf_laserBeam, spawnPosition, Quaternion.identity);
            //set shooter
        }
    }
    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(ShootingLaserBeam());
        IntoCooldown();
    }
}
