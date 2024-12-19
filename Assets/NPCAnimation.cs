using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    public Animator animator; 

    void Start()
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.LogError("flyyyy");
            animator.SetTrigger("Fly"); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Idle");
        }
    }
}
