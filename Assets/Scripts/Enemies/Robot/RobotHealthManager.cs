using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHealthManager : EnemyHealthManager
{
    public bool isInExplodeRange = false;
    public float explodeDamage = 30f;
    [SerializeField] public float explodeDelay = 1f;
    public GameObject pf_Explosion;
    protected override void Die()
    {
        isDead = true;
        animator.SetTrigger("Enemy_die");
        sfxManager.Instance.PlaySound3D("Die", transform.position);
        if (SkillTreeManager.Instance.IsSkillUnlocked(SkillTreeManager.SkillNode.BloodBoiled))
        {
            Movement.Instance.InflictBloodBoiled();
        }
        StartCoroutine(ExplodeOnDead());
    }
    IEnumerator ExplodeOnDead()
    {
        yield return new WaitForSeconds(explodeDelay);

        Instantiate(pf_Explosion, transform.position, Quaternion.identity);
        if (isInExplodeRange)
        {
            HealthManager.Instance.takeDamage(explodeDamage, null);
        }

        DropItem();
        Destroy(gameObject);
    }
}
