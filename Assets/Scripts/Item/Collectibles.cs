using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected float value = 20;
    protected virtual void TakeEffect(Collider2D collision)
    {

    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TakeEffect(collision);
            Destroy(gameObject);
        }
    }
}
