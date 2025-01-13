using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shopkeeper : MonoBehaviour
{
    public Canvas gameplayCanvas;

    public Canvas shopCanvas;

    public Image dialogImage;
    public TMP_Text dialogText;
    public string idleMessage;

    private bool isPlayerNear = false;
    private bool thisScript = false;

    public PauseMenu isIt;
    // Start is called before the first frame update
    void Start()
    {
        dialogText.enabled = false;
        dialogImage.enabled = false;
        shopCanvas.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            gameplayCanvas.gameObject.SetActive(false);
            thisScript = true;
            isIt.isIt = false;
            shopCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
            dialogText.enabled = false;
            dialogImage.enabled = false;
        }
    }
    public void CloseShop()
    {
        if (thisScript)
        {
            Time.timeScale = 1f;
            gameplayCanvas.gameObject.SetActive(true);
            shopCanvas.gameObject.SetActive(false);
            thisScript = false;
            isIt.isIt = true;
            dialogText.enabled = true;
            dialogImage.enabled = true;
            dialogText.text = idleMessage;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (!shopCanvas.gameObject.activeSelf)
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

            dialogText.enabled = false;
            dialogImage.enabled = false;

        }
    }
}
