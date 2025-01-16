using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlast : EnergyBlast
{
    public float burnDuration = 2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (HealthManager.Instance.takeDamage(skillDamage, shooter) == 0)
                StatusManager.Instance.InflictBurn(burnDuration);
        }
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            CreateEffect();
            Destroy(gameObject);
        }

    }
}
