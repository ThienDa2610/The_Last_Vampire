using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCAnimation : MonoBehaviour
{
    public Animator animator; 

    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Talk");
            animator.ResetTrigger("Idle");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {            
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Idle"); 
            animator.ResetTrigger("Talk");
        }
    }
}
