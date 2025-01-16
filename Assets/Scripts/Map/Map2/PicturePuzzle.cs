using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PicturePuzzle : MonoBehaviour
{
    public TMP_Text dialogText;
    public string idleMessage;

    public Image donedialogImage;
    public TMP_Text donedialogText;
    public string doneidleMessage;

    public GameObject pictureImage;

    private bool isPlayerNear = false;

    public Canvas gameplayCanvas;
    public Canvas puzzleCanvas;

    public Puzzle puzzleScript;
    public bool done = false;

    public PauseMenu isIt;
    private bool thisScript = false;
    private bool updated = false;
    void Start()
    {
        if (dialogText != null)
        {
            dialogText.enabled = false;
        }
        if (donedialogText != null)
        {
            donedialogText.enabled = false;
            donedialogImage.enabled = false;
        }
        puzzleCanvas.gameObject.SetActive(false);
        if (!puzzleScript.isPuzzleDone) { pictureImage.SetActive(false); }
        else 
        {
            pictureImage.SetActive(true);
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !puzzleScript.isPuzzleDone)
        {
            puzzleCanvas.gameObject.SetActive(true);
            thisScript = true;
            gameplayCanvas.gameObject.SetActive(false);
            isIt.isIt = false;
            Time.timeScale = 0f;
            dialogText.enabled = false;
        }
        if (puzzleScript.isPuzzleDone)
        {
            StartCoroutine(HandlePuzzleCompletion());
        }
        if (!isIt.isIt && Input.GetKeyDown(KeyCode.Escape) && thisScript)
        {
            CloseShop();
        }
    }
    private IEnumerator ShowDialogForTime(float timeToShow)
    {
        donedialogText.enabled = true;
        donedialogImage.enabled = true;
        donedialogText.text = doneidleMessage; 
        done = true;
        yield return new WaitForSecondsRealtime(timeToShow);  // Wait for the specified time

        donedialogText.enabled = false; 
        donedialogImage.enabled = false;

    }
    IEnumerator HandlePuzzleCompletion()
    {
        yield return new WaitForSecondsRealtime(1f);

        
        if (!updated)
        {
            isIt.isIt = true;
            updated = true;
            pictureImage.SetActive(true);
            puzzleCanvas.gameObject.SetActive(false);
            gameplayCanvas.gameObject.SetActive(true);
            Time.timeScale = 1f;
        }
        if (!done)
        {
            StartCoroutine(ShowDialogForTime(1f));
        }
    }
    public void CloseShop()
    {
        puzzleCanvas.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
        isIt.isIt = true;
        thisScript = false;
        Time.timeScale = 1f;
        dialogText.enabled = true;
        dialogText.text = idleMessage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !puzzleScript.isPuzzleDone)
        {
            isPlayerNear = true;

            dialogText.enabled = true;
            dialogText.text = idleMessage;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !puzzleScript.isPuzzleDone)
        {
            isPlayerNear = false;
            dialogText.enabled = false;
        }
    }
    public void SetPuzzleState(bool state)
    {
        if (done != state)
        {
            done = state;
            /*if (isPuzzleDone)
            {
                torchAnimator.SetTrigger("TurnOn");
                tilemap.SetActive(false);
            }
            else
            {
                torchAnimator.SetTrigger("TurnOff");
                tilemap.SetActive(true);
            }*/
        }
    }
}
