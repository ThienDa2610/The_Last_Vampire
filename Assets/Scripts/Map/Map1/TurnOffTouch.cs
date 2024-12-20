using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffTouch : MonoBehaviour
{
    public GameObject interactionText;
    public Animator torchAnimator; 
    public GameObject tilemap;    
    private bool isPlayerNear = false;
    public bool isTorchOn = true;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && isTorchOn)
        {
            TurnOffTorch(); 
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

    void TurnOffTorch()
    {
        torchAnimator.SetTrigger("TurnOff");

        tilemap.SetActive(true);
        isTorchOn = false;
        interactionText.gameObject.SetActive(false);
    }
}
