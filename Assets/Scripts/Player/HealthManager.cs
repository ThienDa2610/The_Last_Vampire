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
    void Start()
    {
        if (PlayerPrefs.HasKey("SavedHealth"))
        {
            currentHealth = PlayerPrefs.GetFloat("SavedHealth");
        }
        else
        {
            currentHealth = maxHealth;
        }
    }
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
        sfxManager.Instance.PlaySound2D("Die");

        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "Map1_Forest");
        SceneManager.LoadScene(savedSceneName);
        //Time.timeScale = 0;
        //gameOver.SetActive(true);
    }
    public void UpdateHealthbar()
    {
        healthbarOverlay.fillAmount = currentHealth / maxHealth;

    }
}
