using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public static StatusManager Instance;
    //stun
    public bool isStun = false;
    public float stunTimer;
    public GameObject eff_Frozen;
    public float frozenYOffset = -0.4f;
    //burn
    public bool isBurned = false;
    public float burnDamage = 5f;
    public float burnRate = 0.5f;
    public float nextBurnTime;
    public float burnedTimer;
    //trap
    public bool isTrap = false;
    public float nextTrapTime;
    public float trapTimer;
    public float trapRate = 1f;
    public float trapDamage = 15f;
    public float trapSlowPercent = 0.6f;
    public float trapSpeedDif;
    //slough
    public bool isInSlough = false;
    public float sloughSlowPercent = 0.5f;
    public float sloughSpeedDif;
    public float sloughjumpDif;
    private void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        if (isStun)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                isStun = false;
            }
        }
        if (isBurned)
        {
            burnedTimer -= Time.deltaTime;
            if (burnedTimer <= nextBurnTime)
            {
                HealthManager.Instance.takeDamage(burnDamage, null);
                nextBurnTime -= burnRate;
            }
            if (burnedTimer <= 0)
            {
                isBurned = false;
            }
        }
        if (isTrap)
        {
            trapTimer += Time.deltaTime;
            if (trapTimer >= nextTrapTime)
            {
                HealthManager.Instance.takeDamage(trapDamage, null);
                nextTrapTime += trapRate;
            }
        }
    }
    public void InflictStun(float stunDuration)
    {
        isStun = true;
        stunTimer = stunDuration;
        GameObject effFrozen = Instantiate(eff_Frozen, new Vector3(transform.position.x, transform.position.y + frozenYOffset, transform.position.z), Quaternion.identity);
        effFrozen.GetComponent<ExplosionEffect>().effectDuration = stunDuration;
        effFrozen.transform.SetParent(transform);
    }
    public void InflictBurn(float burnDuration)
    {
        isBurned = true;
        burnedTimer = burnDuration;
        nextBurnTime = burnDuration;
    }
    public void InflictTrap()
    {
        isTrap = true;
        trapTimer = 0f;
        nextTrapTime = 0f;
        trapSpeedDif = Movement.Instance.moveSpeed * trapSlowPercent;
        Movement.Instance.moveSpeed -= trapSpeedDif;
    }
    public void CleanseTrap()
    {
        if (!isTrap)
            return;
        isTrap = false;
        Movement.Instance.moveSpeed += trapSpeedDif;
    }
    public void InflictSlough()
    {
        if (isInSlough) return;
        isInSlough = true;
        
        sloughSpeedDif = Movement.Instance.moveSpeed * sloughSlowPercent;
        Movement.Instance.moveSpeed -= sloughSpeedDif;

        Debug.Log(Movement.Instance.jumpForce);
        sloughjumpDif = Movement.Instance.jumpForce;
        Movement.Instance.jumpForce = 0f;
    }
    public void CleanseSlough()
    {
        isInSlough = false;
        Movement.Instance.moveSpeed += sloughSpeedDif;
        Movement.Instance.jumpForce += sloughjumpDif;

        sloughSpeedDif = 0f;
        sloughjumpDif = 0f;
    }
}
