using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCAnimation : MonoBehaviour
{
    public Animator animator; 
    public Image dialogImage;
    public TMP_Text dialogText;
    public string idleMessage = "Hello!";

    void Start()
    {
        if (dialogText != null)
        {
            dialogText.enabled = false; 
            dialogImage.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Talk");
            animator.ResetTrigger("Fly");
            if (dialogText != null)
            {
                dialogText.enabled = true; 
                dialogImage.enabled = true;
                dialogText.text = idleMessage;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {            
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Fly"); 
            animator.ResetTrigger("Talk");
            if (dialogText != null)
            {
                dialogText.enabled = false; 
                dialogImage.enabled = false;
            }
        }
    }
}
