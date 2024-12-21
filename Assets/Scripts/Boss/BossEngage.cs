using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEngage : MonoBehaviour
{
    BossSkillManager skillManager;
    private void Awake()
    {
        skillManager = GetComponentInParent<BossSkillManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skillManager.inFight = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skillManager.inFight = false;
        }
    }
}
