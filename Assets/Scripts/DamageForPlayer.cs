using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageForPlayer : MonoBehaviour
{
    public float dmg;
    float dameRate = 1f;
    float nextDame;
    private bool isPlayerInTrigger = false;
    void Start()
    {
        nextDame = 0f;
    }

    void Update()
    {
        if (isPlayerInTrigger && nextDame < Time.time)
        {
            PlayerHealth thePlayerHealth = FindObjectOfType<PlayerHealth>();
            if (thePlayerHealth != null)
            {
                thePlayerHealth.HasDamage(dmg);
            }

            nextDame = Time.time + dameRate;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
