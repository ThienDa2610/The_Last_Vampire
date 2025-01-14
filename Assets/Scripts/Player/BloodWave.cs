using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodWave : EnergyBlast
{
    public bool isEnhanced = false;
    public int maxBloodLostStack = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealthManager enemyHealthManager = collision.gameObject.GetComponent<EnemyHealthManager>();
            enemyHealthManager.TakeDamage(skillDamage);
            if (isEnhanced)
            {
                for (int i = 0; i < maxBloodLostStack; i++)
                {
                    enemyHealthManager.InflictBloodLost();
                }
            }
        }
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ground"))
        {
            CreateEffect();
            Destroy(gameObject);
        }

    }
    
    
}
