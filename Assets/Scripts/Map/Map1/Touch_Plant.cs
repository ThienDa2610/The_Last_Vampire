using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Plant : MonoBehaviour
{
    public GameObject interactionText;

    public Animator flowerAnimator;  
    public string animationTrigger = "Is_Touch"; 
    private bool isPlayerNear = false;  
    private bool hasBloomed = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !hasBloomed)
        {
            flowerAnimator.SetTrigger(animationTrigger);
            hasBloomed = true;
            interactionText.gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactionText.gameObject.SetActive(true);  
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactionText.gameObject.SetActive(false);  
        }
    }
}
