using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public GameObject dropItemPrefab;
    public Transform dropPoint;
    private bool isTakingDamage = false;
    public int dropChance = 100;
    public float health = 15.0f;
    public Animator animator;
    private EnemySoundManager soundManager;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        soundManager = GetComponent<EnemySoundManager>();

    }
    void Update()
    {
        if (isDead) return;
        if (health <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        soundManager.PlayTakeDamage();
    }

    private void Die()
    {
        animator.SetTrigger("Enemy_die");
        soundManager.PlayDeathSound();
        isDead = true;
        DropItem();
        Destroy(gameObject, 0.5f);
    }

    private void DropItem()
    {
        Debug.Log("Dropped");
        if (Random.Range(0, 100) <= dropChance)
        {
            Vector3 spawnPosition = dropPoint != null ? dropPoint.position : transform.position;
            Instantiate(dropItemPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
