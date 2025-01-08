using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float damagePerTick = 15f;
    public float damageRate = 0.5f;
    public float damageTimer = 0f;
    public float laserBeamDuration = 3f;
    public bool isPlayerInRange = false;
    public float initiateTime = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (initiateTime > 0)
        {
            initiateTime -= Time.deltaTime;
            return;
        }
        if (damageTimer > 0)
            damageTimer -= Time.deltaTime;
        if (damageTimer <= 0 && isPlayerInRange)
        {
            HealthManager.Instance.takeDamage(damagePerTick, null);
            damageTimer = damageRate;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
