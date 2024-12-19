using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float value = 20; 

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player")) 
    //     {
    //         PlayerController player = collision.GetComponent<PlayerController>();
    //         if (player != null)
    //         {
    //             player.ChangeHealth(value); 
    //         }
    //         Destroy(gameObject);
    //     }
    // }
}
