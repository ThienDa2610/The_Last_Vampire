using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Torch_OnOff : MonoBehaviour
{
    public bool isOn = true;
    public Animator torchAnimator;
    public Image dialogImage;
    public TMP_Text dialogText;
    public string idleMessage;
    private bool isPlayerNear = false;

    void Start()
    {
        dialogText.enabled = false;
        dialogImage.enabled = false;
    }
    
    public void ToggleTorch()
    {
        if (isOn)
        {
            torchAnimator.SetTrigger("TurnOff");
            isOn = false;
        }
        else
        {
            torchAnimator.SetTrigger("TurnOn");
            isOn = true;
        }
        dialogText.enabled = false;
        dialogImage.enabled = false;
    }

    public void SetTorchState(bool state)
    {
        if (isOn != state)
        {
            isOn = state;
            if (isOn)
            {
                torchAnimator.SetTrigger("TurnOn");
            }
            else
            {
                torchAnimator.SetTrigger("TurnOff");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (isOn)
            {
                dialogText.enabled = true;
                dialogImage.enabled = true;
                dialogText.text = idleMessage;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            dialogText.enabled = false;
            dialogImage.enabled = false;
        }
    }

    public bool IsOn()
    {
        return isOn;
    }


    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            ToggleTorch();
        }
    }
}
