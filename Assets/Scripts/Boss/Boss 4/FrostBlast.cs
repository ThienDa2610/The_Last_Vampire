using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBlast : EnergyBlast
{
    public float stunDuration = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (HealthManager.Instance.takeDamage(skillDamage, shooter) == 0)
                StatusManager.Instance.InflictStun(stunDuration);
        }
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            CreateEffect();
            Destroy(gameObject);
        }
    }
}
