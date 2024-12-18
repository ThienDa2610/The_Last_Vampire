using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    static public HealthManager Instance { get; private set; }
    public GameObject gameOver;
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvincible = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
    }

    public void takeDamage(float damage)
    {
        if (isInvincible) return;
        currentHealth = (currentHealth - damage) < 0 ? 0 : (currentHealth - damage);
        if (currentHealth == 0)
        {
            Dead();
        }
    }
    private void Dead()
    {
        //gameOver.SetActive(true);
        Time.timeScale = 0;
    }
}
