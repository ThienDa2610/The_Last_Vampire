using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    public GameObject[] dropItemPrefab;
    public Transform dropPoint;
    public bool isTakingDamage = false;
    public float[] dropChance;
    public float maxHealth = 15f;
    public float health;
    public Animator animator;
    public Image healthbarOverlay;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
    }
    void Update()
    {
        UpdateHealthbar();
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health < 0)
        {
            Die();
        }
    }
    void UpdateHealthbar()
    {
        healthbarOverlay.fillAmount = health / maxHealth;
    }

    private void Die()
    {
        animator.SetTrigger("Enemy_die");
        sfxManager.Instance.PlaySound3D("Die", transform.position);
        DropItem();
        Destroy(gameObject, 0.5f);
    }

    private void DropItem()
    {
        for (int i = 0; i< dropItemPrefab.Length; i++)
        {
            if (Random.Range(0f,100f) <= dropChance[i])
            {
                Vector3 spawnPosition = dropPoint != null ? dropPoint.position : transform.position;
                Instantiate(dropItemPrefab[i], spawnPosition, Quaternion.identity);
            }
        }
        
    }
}
