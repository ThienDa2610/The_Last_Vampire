using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    static public HealthManager Instance { get; private set; }
    public GameObject gameOver;
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvincible = false;
    public Image healthbarOverlay;
    public float fallLimit = -6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (transform.position.y < fallLimit)
        {
            Dead();
        }
    }
    void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
        UpdateHealthbar();
    }
    public void takeDamage(float damage)
    {
        if (isInvincible) return;
        currentHealth = (currentHealth - damage) < 0 ? 0 : (currentHealth - damage);
        UpdateHealthbar();
        if (currentHealth == 0)
        {
            Dead();
        }
    }
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        UpdateHealthbar();
    }
    private void Dead()
    {
        //Time.timeScale = 0;
        //gameOver.SetActive(true);
        MapLoader.Instance.LoadMap("GameOver");
        MusicManager.Instance.PlayMusic("Menu");
    }
    private void UpdateHealthbar()
    {
        healthbarOverlay.fillAmount = currentHealth / maxHealth;

    }
}
