using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractGuide : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text dialogText;
    public string idleMessage;

    protected Animator animator;

    protected bool isPlayerNear = false;

    void Start()
    { 
        animator = GetComponent<Animator>();
        if (dialogText != null)
            dialogText.enabled = false;
    }

    void Update()
    {
        Interact();
    }
    public bool getIsNear()
    {
        return isPlayerNear;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (dialogText != null)
            {
                dialogText.enabled = true;
                dialogText.text = idleMessage;
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
            TurnOffGuide();
        }
    }
    protected void TurnOffGuide()
    {
        if (dialogText != null)
            dialogText.enabled = false;
    }
    protected virtual void Interact() { }
}
