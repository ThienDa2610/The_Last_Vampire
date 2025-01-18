using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isTalking", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {            
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isTalking", false);

        }
    }
}
