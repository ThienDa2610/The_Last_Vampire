using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    public GameObject interactionText_Touch1;
    
    public TurnOffTouch torchScript;

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Torch") && torchScript.isTorchOn) 
        {
            interactionText_Touch1.SetActive(true); 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Torch"))
        {
            interactionText_Touch1.SetActive(false);
        }
    }
}
