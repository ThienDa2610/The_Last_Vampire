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

    // Start is called before the first frame update
    void Start()
    {
        dialogText.enabled = false;
        dialogImage.enabled = false;
        shopCanvas.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            shopCanvas.gameObject.SetActive(true);
            gameplayCanvas.gameObject.SetActive(false);
            Time.timeScale = 0f;
            dialogText.enabled = false;
            dialogImage.enabled = false;
        }
    }
    public void CloseShop()
    {
        shopCanvas.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f;
        dialogText.enabled = true;
        dialogImage.enabled = true;
        dialogText.text = idleMessage;
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
