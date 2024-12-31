using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected float value = 20;
    protected virtual void TakeEffect(Collider2D collision)
    {

    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            TakeEffect(collision.collider);
            Destroy(gameObject);
        }
    }
}