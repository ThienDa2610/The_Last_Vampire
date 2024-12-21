using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float damage = 10f;     
    public float damageInterval = 1f;
    private bool isDamaging = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ApplyDamageOverTime());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines(); 
            isDamaging = false;
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        isDamaging = true;
        while (true)
        {
            HealthManager.Instance.takeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
