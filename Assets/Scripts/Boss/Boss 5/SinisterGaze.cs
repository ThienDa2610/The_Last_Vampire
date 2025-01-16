using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinisterGaze : MonoBehaviour
{
    public float skillDamage;
    public float stunDuration = 2f;
    void Start()
    {
        skillDamage = 40f;
    }
    public void Play(Transform target)
    {
        StatusManager.Instance.InflictStun(2f);
        HealthManager.Instance.takeDamage(skillDamage, null);
    }
}
