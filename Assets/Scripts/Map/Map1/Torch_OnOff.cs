using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Torch_OnOff : MonoBehaviour
{
    public bool isOn = true;
    public Animator torchAnimator;
    public TMP_Text dialogText;
    public string idleMessage;
    private bool isPlayerNear = false;

    void Start()
    {
        dialogText.enabled = false;
    }
    
    public void ToggleTorch()
    {
        if (isOn)
        {
            torchAnimator.SetTrigger("TurnOff");
            sfxManager.Instance.PlaySound2D("turn_off_torch");
            isOn = false;
        }
        else
        {
            torchAnimator.SetTrigger("TurnOn");
            sfxManager.Instance.PlaySound2D("waving-torch1");
            isOn = true;
        }
        dialogText.enabled = false;
    }

    public void SetTorchState(bool state)
    {
        if (isOn != state)
        {
            isOn = state;
            if (isOn)
            {
                torchAnimator.SetTrigger("TurnOn");
                sfxManager.Instance.PlaySound2D("waving-torch1");
            }
            else
            {
                torchAnimator.SetTrigger("TurnOff");
                sfxManager.Instance.PlaySound2D("turn_off_torch");
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
