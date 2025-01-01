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
    public bool isDead = false;

    //skill tree
    // //blood lost
    private float bloodLostDuration = 2f;
    private float bloodLostDamage = 2f;
    private float bloodLostRate = 0.5f;
    private List<float> bloodLostTimer;
    private List<float> bloodLostDamageTimer;
    // //infection
    public static bool infectable = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        bloodLostTimer = new List<float>();
        bloodLostDamageTimer = new List<float>();
    }
    void Update()
    {
        if (isDead) return;

        //blood lost damage
        if (bloodLostTimer.Count > 0)
        {
            for (int i = 0; i < bloodLostTimer.Count; i++)
            {
                if (bloodLostTimer[i] < bloodLostDamageTimer[i])
                {
                    TakeDamage(bloodLostDamage);
                    bloodLostDamageTimer[i] -= bloodLostRate;
                }
            }
        }

        //bloodlost stack reduce
        for (int i = 0; i<bloodLostTimer.Count; i++)
        {
            bloodLostTimer[i] -= Time.deltaTime;
            if (bloodLostTimer[i] < 0)
            {
                bloodLostTimer.RemoveAt(i);
                bloodLostDamageTimer.RemoveAt(i);
            }
        }  

        UpdateHealthbar();
    }
    public void InflictBloodLost()
    {
        if (bloodLostTimer.Count < 3)
        {
            bloodLostTimer.Add(bloodLostDuration);
            bloodLostDamageTimer.Add(bloodLostDuration);
        }
        else
        {
            int lowestTimerIdx = bloodLostTimer.Count - 1;
            for (int i = 0; i < bloodLostTimer.Count - 1; i++)
            {
                if (bloodLostTimer[i] < bloodLostTimer[lowestTimerIdx])
                    lowestTimerIdx = i;
            }
            bloodLostTimer[lowestTimerIdx] = bloodLostDuration;
            bloodLostDamageTimer[lowestTimerIdx] = bloodLostDuration;
        }
    }
    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;
        if (infectable)
        {
            damageAmount *= 1 + bloodLostTimer.Count * 0.05f;
        }
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
        isDead = true;
        animator.SetTrigger("Enemy_die");
        sfxManager.Instance.PlaySound3D("Die", transform.position);
        if (SkillTreeManager.Instance.IsSkillUnlocked(SkillTreeManager.SkillNode.BloodBoiled))
        {
            Movement.Instance.InflictBloodBoiled();
        }
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
