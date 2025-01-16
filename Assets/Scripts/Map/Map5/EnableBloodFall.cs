using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnableBloodFall : MonoBehaviour
{
    public Animator animator;           
    private bool isPlayerInRange = false;
    public TMP_Text dialogText;
    public string idleMessage;
    public Image FaildialogImage;
    public TMP_Text FaildialogText;
    public string FailidleMessage;
    public TypeCoinManager typeCoinManager;

    private bool inActivate = false;
    void Start()
    {
        if (dialogText != null)
        {
            dialogText.enabled = false;
        }
        if (FaildialogImage != null)
        {
            FaildialogImage.enabled = false;
            FaildialogText.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !inActivate)
        {
            isPlayerInRange = true;
            if (dialogText != null)
            {
                dialogText.enabled = true;
                dialogText.text = idleMessage;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !inActivate)
        {
            isPlayerInRange = false;
            if (dialogText != null)
            {
                dialogText.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !inActivate)
        {
            if (typeCoinManager.dragonHeart)
            {
                animator.SetTrigger("HaveDragonHeart");
                inActivate = true;
            }
            else
            {
                StartCoroutine(ShowDialogForTime(2f));
            }
        }
    }
    private IEnumerator ShowDialogForTime(float timeToShow)
    {
        if (FaildialogText != null && FaildialogImage != null)
        {
            FaildialogText.enabled = true;
            FaildialogImage.enabled = true;
            FaildialogText.text = FailidleMessage; // Display saved message
        }

        yield return new WaitForSeconds(timeToShow);  // Wait for the specified time

        if (FaildialogText != null && FaildialogImage != null)
        {
            FaildialogText.enabled = false; // Hide saved message
            FaildialogImage.enabled = false;
        }
    }
}
