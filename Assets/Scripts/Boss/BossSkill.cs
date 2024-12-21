using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    protected float skillDamage;
    protected float maxCooldown;
    protected float currentCooldown;
    protected float skillRange;
    public float skillCastDuration;
    protected Collider2D Hitbox;
    protected ApplySkillDamage Applier;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentCooldown = 0;
        Hitbox = GetComponentInChildren<Collider2D>();
        Applier = GetComponentInChildren<ApplySkillDamage>();
        Applier.castingDamage = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (currentCooldown > 0)
            currentCooldown = (currentCooldown - Time.deltaTime > 0) ? currentCooldown - Time.deltaTime : 0;
    }
    public bool isReady()
    {
        if (currentCooldown == 0)
            return true;
        return false;
    }
    public virtual void Play()
    {

    }
    protected void IntoCooldown()
    {
        currentCooldown = maxCooldown;
    }
}
