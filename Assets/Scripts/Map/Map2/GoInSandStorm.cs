using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GoInSandStorm : MonoBehaviour
{
    public Camera mainCamera;
    public Camera forthCamera;
    public Camera fifthCamera;

    public GameObject player;
    public GameObject obj1;
    public GameObject obj2;

    public Vector3 targetPosition1;
    Vector2 triggerPosition1;
    public Vector3 targetPosition2;
    Vector2 triggerPosition2;

    private bool hasTriggered1 = false;
    private bool hasTriggered2 = false;

    public float delayTime = 3f;

    public bool SS1 = false;
    public bool SS2 = false;

    //quiz
    public Puzzle puzzleScript;

    public Image dialogImage;
    public TMP_Text dialogText;

    private bool result = false;

    private string[] initialMessages = new string[]
    {
        "Enter sandstorm area.",
        "Heard a strange voice.",
        "Follow me."
    };

    private string[] rightLeftMessages = new string[]
    {
        "Right.",
        "Left."
    };

    public int AllCorrect = 0;
    void Start()
    {
        triggerPosition1 = new Vector2(77f, 1f);
        triggerPosition2 = new Vector2(244f, 1f);

        obj1.SetActive(false);
        obj2.SetActive(false);

        if (fifthCamera != null)
        {
            fifthCamera.gameObject.SetActive(false);
        }
        if (forthCamera != null)
        {
            forthCamera.gameObject.SetActive(false);
        }
        if (dialogText != null)
        {
            dialogText.enabled = false;
            dialogImage.enabled = false;
        }
    }

    void Update()
    {
        if (!hasTriggered1 && !SS1 && Math.Abs(player.transform.position.x - triggerPosition1.x) < 1f && Math.Abs(player.transform.position.y - triggerPosition1.y) < 5f)
        {
            hasTriggered1 = true;
            SS1 = true;
            obj1.SetActive(true);
            StartCoroutine(TeleportPlayer1());
        }
        if(!hasTriggered2 && !SS2 && Math.Abs(player.transform.position.x - triggerPosition2.x) < 1f && Math.Abs(player.transform.position.y - triggerPosition2.y) < 5f)
        {
            hasTriggered2 = true;
            SS2 = true;
            obj2.SetActive(true);
            StartCoroutine(TeleportPlayer2());
        }


        if (result && AllCorrect == 6)
        {
            if (forthCamera.gameObject.activeSelf)
            {
                player.transform.position = triggerPosition1;
                mainCamera.gameObject.SetActive(true);
                forthCamera.gameObject.SetActive(false);
                obj1.SetActive(false);
            }
            else if (fifthCamera.gameObject.activeSelf)
            {
                player.transform.position = triggerPosition2;
                mainCamera.gameObject.SetActive(true);
                fifthCamera.gameObject.SetActive(false);
                obj2.SetActive(false);
            }
            StartCoroutine(ShowDialogForTime(2f));
        }
    }
    private IEnumerator ShowDialogForTime(float timeToShow)
    {
        dialogText.text = "Exit sandstorm area.";
        yield return new WaitForSecondsRealtime(timeToShow);

        dialogText.enabled = false;
        dialogImage.enabled = false;

    }
    IEnumerator TeleportPlayer1()
    {
        yield return new WaitForSeconds(delayTime);

        player.transform.position = targetPosition1;
        if (mainCamera != null && forthCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            forthCamera.gameObject.SetActive(true);
            //nho them dieu kien
            if (puzzleScript.isPuzzleDone)
            {
                StartCoroutine(DisplayMessages(forthCamera));
            }

        }
    }
    IEnumerator TeleportPlayer2()
    {
        yield return new WaitForSeconds(delayTime);

        player.transform.position = targetPosition2;
        if (mainCamera != null && forthCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            fifthCamera.gameObject.SetActive(true);
            if (puzzleScript.isPuzzleDone)
            {
                StartCoroutine(DisplayMessages(fifthCamera));
            }
        }
    }
    IEnumerator DisplayMessages(Camera camera)
    {
        dialogImage.enabled = true;
        dialogText.enabled = true;
        foreach (string message in initialMessages)
        {
            dialogText.text = message;
            yield return new WaitForSeconds(2f);
        }
        yield return StartCoroutine(DisplayRandomRightLeftMessages(camera));

        //dialogImage.enabled = false;
    }

    IEnumerator DisplayRandomRightLeftMessages(Camera camera)
    {
        AllCorrect = 0;
        int times = 6;
        bool showRight = UnityEngine.Random.value > 0.5f;

        for (int i = 0; i < times; i++)
        {
            string message = showRight ? rightLeftMessages[0] : rightLeftMessages[1];
            dialogText.text = message;

            float duration = UnityEngine.Random.Range(1f, 4f);  // Random time for right/left

            yield return StartCoroutine(CheckPlayerInput(showRight, duration));

            if (!result)
            {
                dialogText.text = "You are lost. Try again.";
                yield return new WaitForSeconds(2f);
                yield return StartCoroutine(DisplayRandomRightLeftMessages(camera));  
                yield break;  
            }
            showRight = !showRight;
            dialogText.enabled = false;
            yield return new WaitForSeconds(1f);
            dialogText.enabled = true;
            AllCorrect++;
        }

    }
    IEnumerator CheckPlayerInput(bool showRight, float duration)
    {
        float timeSpent = 0f;
        bool isInputCorrect = false;
        bool hasStarted = false;

        while (timeSpent < duration)
        {
            if (showRight && Input.GetKey(KeyCode.D))  // Press D when "Right" is shown
            {
                if (!hasStarted)  
                {
                    hasStarted = true;
                }
                timeSpent += Time.deltaTime;
                isInputCorrect = true;
            }
            else if (!showRight && Input.GetKey(KeyCode.A))  // Press A when "Left" is shown
            {
                if (!hasStarted)
                {
                    hasStarted = true;
                }
                timeSpent += Time.deltaTime;
                isInputCorrect = true;
            }
            else if (showRight && Input.GetKey(KeyCode.A))  // Pressing the wrong key when "Right" is shown
            {
                result = false;
                yield break;
            }
            else if (!showRight && Input.GetKey(KeyCode.D))  // Pressing the wrong key when "Left" is shown
            {
                result = false;
                yield break;
            }
            if (hasStarted && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && timeSpent < duration - 1f)  
            {
                result = false;
                yield break;
            }
            // If the player releases the key too early, or holds it too long:
            if (isInputCorrect && timeSpent > duration + 1f)
            {
                result = false;
                yield break;
            }

            
            yield return null;  // Wait until the next frame
        }

        // If player presses the correct key and time is within the correct range
        result = (isInputCorrect && timeSpent >= duration - 0.5f && timeSpent <= duration + 0.5f);
    }
    public void SetSS1State(bool state)
    {
        if (SS1 != state)
        {
            SS1 = state;
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
    public void SetSS2State(bool state)
    {
        if (SS2 != state)
        {
            SS2 = state;
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

