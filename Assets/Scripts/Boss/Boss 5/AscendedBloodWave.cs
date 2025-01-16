using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscendedBloodWave : EnergyBlast
{
    public SinisterGaze sinisterGaze;
    private void Start()
    {
        base.Start();
        sinisterGaze = GetComponent<SinisterGaze>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (HealthManager.Instance.takeDamage(skillDamage,shooter) == 2)
            {
                sinisterGaze.Play(collision.transform);
            }
        }
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            CreateEffect();
            Destroy(gameObject);
        }
    }
}
