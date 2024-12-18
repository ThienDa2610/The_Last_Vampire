using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    public Animator animator; 

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.LogError("flyyy");
            animator.SetTrigger("Fly"); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Idle");
        }
    }
}
