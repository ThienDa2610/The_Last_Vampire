using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Touch_Plant : MonoBehaviour
{
    public Image dialogImage;
    public TMP_Text dialogText;
    public string idleMessage;

    public Animator flowerAnimator;  
    public string animationTrigger = "Is_Touch"; 
    private bool isPlayerNear = false;  
    public bool hasBloomed = false;
    void Start()
    {
        if (dialogText != null)
        {
            dialogText.enabled = false;
            dialogImage.enabled = false;
        }
    }
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !hasBloomed)
        {
            flowerAnimator.SetTrigger(animationTrigger);
            sfxManager.Instance.PlaySound2D("hoa_khong_no");
            hasBloomed = true;
            if (dialogText != null)
            {
                dialogText.enabled = false;
                dialogImage.enabled = false;
            }
        }
    }
    public void SetPlantState(bool state)
    {
        if (hasBloomed != state)
        {
            hasBloomed = state;
            if (hasBloomed)
            {
                flowerAnimator.SetTrigger(animationTrigger);
                sfxManager.Instance.PlaySound2D("hoa_no");
                dialogText.enabled = false;
                dialogImage.enabled = false;
            }
            
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (!hasBloomed && dialogText != null)
            {
                dialogText.enabled = true;
                dialogImage.enabled = true;
                dialogText.text = idleMessage;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (dialogText != null)
            {
                dialogText.enabled = false;
                dialogImage.enabled = false;
            }
        }
    }
}
