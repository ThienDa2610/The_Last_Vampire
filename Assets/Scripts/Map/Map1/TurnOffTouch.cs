using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnOffTouch : MonoBehaviour
{
    public TMP_Text dialogText;
    public string idleMessage;
    public Animator torchAnimator; 
    public GameObject tilemap;    
    private bool isPlayerNear = false;
    public bool isTorchOn = true;
    
    void Start()
    {
        if (dialogText != null)
        {
            dialogText.enabled = false;
        }
        if (isTorchOn)
        {
            tilemap.SetActive(false);
        }
        else
        {
            tilemap.SetActive(true);
        }
    }
    public void TurnOffTorch()
    {
        if (isTorchOn)
        {
            torchAnimator.SetTrigger("TurnOff");
            sfxManager.Instance.PlaySound2D("turn_off_torch");
            isTorchOn = false;
            tilemap.SetActive(true);
        }
        else
        {
            torchAnimator.SetTrigger("TurnOn");
            sfxManager.Instance.PlaySound2D("waving-torch1");
            isTorchOn = true;
            tilemap.SetActive(false);
        }
        dialogText.enabled = false;
    }
    public void SetTorchState(bool state)
    {
        if (isTorchOn != state)
        {
            isTorchOn = state;
            if (isTorchOn)
            {
                torchAnimator.SetTrigger("TurnOn");
                tilemap.SetActive(false);
            }
            else
            {
                torchAnimator.SetTrigger("TurnOff");
                tilemap.SetActive(true);
            }
        }
    }
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            TurnOffTorch(); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerNear = true;
            if (isTorchOn)
            {
                dialogText.enabled = true;
                dialogText.text = idleMessage;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            dialogText.enabled = false;

        } 
    }
    public bool IsTorchOn()
    {
        return isTorchOn;
    }

}
