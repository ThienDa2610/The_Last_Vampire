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
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
    }

    private void Die()
    {
        animator.SetTrigger("Enemy_die");
        sfxManager.Instance.PlaySound3D("Die", transform.position);
        isDead = true;
        DropItem();
        Destroy(gameObject, 0.5f);
    }

    private void DropItem()
    {
        if (Random.Range(0, 100) <= dropChance)
        {
            Vector3 spawnPosition = dropPoint != null ? dropPoint.position : transform.position;
            Instantiate(dropItemPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
