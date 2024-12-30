using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_OnOff : MonoBehaviour
{
    private bool isOn = true;
    public Animator torchAnimator;
    public GameObject toggleText;
    private bool isPlayerNear = false;

    void Start()
    {
        toggleText.SetActive(false);
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
        toggleText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (isOn)
            {
                toggleText.SetActive(true);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            toggleText.SetActive(false);
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
