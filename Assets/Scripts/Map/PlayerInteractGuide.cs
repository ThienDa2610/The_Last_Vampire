using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractGuide : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text dialogText;
    public string idleMessage;

    public GameObject gameObj;
    public Animator animator;

    public bool isPlayerNear = false;

    void Start()
    { 
        gameObj = gameObject;
        animator = GetComponent<Animator>();
        if (dialogText != null)
            dialogText.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
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

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
            TurnOffGuide();
        }
    }
    public void TurnOffGuide()
    {
        if (dialogText != null)
            dialogText.enabled = false;
    }
}
