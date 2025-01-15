using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public static StatusManager Instance;
    public bool isStun = false;
    public float stunTimer;

    public bool isBurned = false;
    public float burnDamage = 5f;
    public float burnRate = 0.5f;
    public float nextBurnTime;
    public float burnedTimer;
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
    }
    public void InflictStun(float stunDuration)
    {
        isStun = true;
        stunTimer = stunDuration;
    }
    public void InflictBurn(float burnDuration)
    {
        isBurned = true;
        burnedTimer = burnDuration;
        nextBurnTime = burnDuration;
    }
}
