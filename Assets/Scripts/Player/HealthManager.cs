using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class HealthManager : MonoBehaviour
{
    static public HealthManager Instance { get; private set; }
    public GameObject gameOver;
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvincible = false;
    public UnityEngine.UI.Image healthbarOverlay;
    public float fallLimit = -6f;
    public Counter counter;

    //skill tree
    public static float rebirthCD;
    public static bool canRebirth2 = false;
    private float rebirthMaxCD = 240f;
    void Awake()
    {
        Instance = this;
        counter = GetComponent<Counter>();
        currentHealth = maxHealth;
        UpdateHealthbar();
    }
    private void Start()
    {
        //rebirth
        if (SkillTreeManager.Instance.IsSkillUnlocked(SkillTreeManager.SkillNode.Rebirth_1))
        {
            rebirthCD = 0;
        }
        else
        {
            rebirthCD = rebirthMaxCD;
        }

        if (SkillTreeManager.Instance.IsSkillUnlocked(SkillTreeManager.SkillNode.Rebirth_2))
            canRebirth2 = true;
        //save
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
        if (rebirthCD > 0 && SkillTreeManager.Instance.IsSkillUnlocked(SkillTreeManager.SkillNode.Rebirth_1))
        {
            rebirthCD = (rebirthCD - Time.deltaTime > 0) ? rebirthCD - Time.deltaTime : 0;
        }
        if (transform.position.y < fallLimit)
        {
            Dead();
        }
       
    }
    
    public bool takeDamage(float damage, GameObject damageDealer)
    {
        if (counter.isCountering && damageDealer != null)
        {
            counter.Countering(damageDealer);
            return false;
        }
        if (isInvincible) return false;
        currentHealth = (currentHealth - damage) < 0 ? 0 : (currentHealth - damage);
        UpdateHealthbar();
        if (currentHealth == 0)
        {
            if (rebirthCD == 0)
            {
                Heal(maxHealth * 0.5f);
                rebirthCD = rebirthMaxCD;
                UpdateHealthbar();
            }
            else if (canRebirth2)
            {
                Heal(maxHealth * 0.3f);
                canRebirth2 = false;
                UpdateHealthbar();
            }
            else
                Dead();
        }
        return true;
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
