using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoDamage : MonoBehaviour
{
    SandTornado sandTornado;
    GameObject parent;
    void Start()
    {
        sandTornado = GetComponentInParent<SandTornado>();
    }
    private void Update()
    {
        if (sandTornado.damageTimer > 0)
            sandTornado.damageTimer -= Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && sandTornado.isSpinning && sandTornado.damageTimer <= 0)
        {
            HealthManager.Instance.takeDamage(sandTornado.skillDamage, parent);
            sandTornado.damageTimer = sandTornado.damageRate;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sandTornado.shouldChase = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sandTornado.shouldChase = true; ;
        }
    }
}
