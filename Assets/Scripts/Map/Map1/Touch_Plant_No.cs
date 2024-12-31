using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Touch_Plant_No : MonoBehaviour
{
    public Image dialogImage;
    public TMP_Text dialogText;
    public string idleMessage;

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
            gameObject.SetActive(false);
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
                gameObject.SetActive(false);
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
