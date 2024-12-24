using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySkillDamage : MonoBehaviour
{
    public float skillDamage;
    public bool castingDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && castingDamage)
        {
            HealthManager.Instance.takeDamage(skillDamage, transform.parent.gameObject);
        }
    }
}
