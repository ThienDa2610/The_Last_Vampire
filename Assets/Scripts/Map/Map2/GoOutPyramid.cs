using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoOutPyramid : MonoBehaviour
{
    public TMP_Text dialogText;
    public string idleMessage;

    public Image ClosedialogImage;
    public TMP_Text ClosedialogText;
    public string CloseidleMessage;

    public Animator doorAnimator;
    public string closeDoorTrigger = "CloseDoor";
    public string openDoorTrigger = "OpenDoor";
    public bool doorClosed = false;

    public Camera mainCamera;
    public Camera secondaryCamera;
    public Camera thirdCamera;

    private bool isPlayerNear = false;
    public Puzzle puzzleScript;

    public GameObject player;
    public Vector3 targetPosition1;
    public Vector3 targetPosition2;

    void Start()
    {
        if (dialogText != null)
        {
            dialogText.enabled = false;
        }
        if (ClosedialogText != null)
        {
            ClosedialogText.enabled = false;
            ClosedialogImage.enabled = false;
        }
    }

    void Update()
    {
        if (secondaryCamera.gameObject.activeSelf && !doorClosed && !puzzleScript.isPuzzleDone)
        {
            StartCoroutine(PlayCloseDoorWithDelay());
            doorClosed = true;
        }      
        if (puzzleScript.isPuzzleDone)
        {
            StartCoroutine(PlayOpenDoorWithDelay());
            doorClosed = false;
        }
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            if (doorClosed)
            {
                StartCoroutine(ShowDialogForTime(1f));
            }
            else
            {
                if (secondaryCamera.gameObject.activeSelf)
                {
                    player.transform.position = targetPosition1;
                    secondaryCamera.gameObject.SetActive(false);
                }
                else if (thirdCamera.gameObject.activeSelf)
                {
                    player.transform.position = targetPosition2;
                    thirdCamera.gameObject.SetActive(false);
                }
                mainCamera.gameObject.SetActive(true);
            }
        } 
    }
    private IEnumerator ShowDialogForTime(float timeToShow)
    {

        ClosedialogText.enabled = true;
        ClosedialogImage.enabled = true;
        ClosedialogText.text = CloseidleMessage; // Display saved message

        yield return new WaitForSecondsRealtime(timeToShow);  // Wait for the specified time

        ClosedialogText.enabled = false; // Hide saved message
        ClosedialogImage.enabled = false;

    }
    private IEnumerator PlayCloseDoorWithDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);  
        doorAnimator.SetTrigger(closeDoorTrigger);  
        //sfxManager.Instance.PlaySound2D("pyramid_close");
    }
    private IEnumerator PlayOpenDoorWithDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        doorAnimator.SetTrigger(openDoorTrigger);
        //sfxManager.Instance.PlaySound2D("pyramid_open");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            dialogText.enabled = true;
            dialogText.text = idleMessage;
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
}
