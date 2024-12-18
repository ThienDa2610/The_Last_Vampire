using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public float health = 15.0f;     
    public float damage = 20.0f;     
    public float damageInterval = 5.0f; 
    public Animator animator;

    private bool isDamaging = false;
    private bool isTakeDame = false;
    private EnemySoundManager soundManager;

    void Start()
    {
        soundManager = GetComponent<EnemySoundManager>();
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player")) 
    //     {
    //         PlayerController player = collision.gameObject.GetComponent<PlayerController>();
    //         if (player != null && !isDamaging)
    //         {
    //             StartCoroutine(ApplyDamageOverTime(player));
    //         }
    //     }
    // }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines(); 
            isDamaging = false;
        }
    }
    
    // private IEnumerator ApplyDamageOverTime(PlayerController player)
    // {
    //     isDamaging = true;
    //     while (true)
    //     {
    //         player.TakeDamage(damage); 
    //         yield return new WaitForSeconds(damageInterval); 
    //     }
    // }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; 
        soundManager.PlayTakeDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Enemy_die");
        soundManager.PlayDeathSound();
        Destroy(gameObject,0.5f); 
    }
}
