using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoToPyramid : MonoBehaviour
{
    public TMP_Text dialogText;
    public string idleMessage;
    public Vector3 targetPosition;

    public Camera mainCamera;  
    public Camera secondaryCamera;

    public GameObject player;

    private bool isPlayerNear = false;


    void Start()
    {
        if (dialogText != null)
        {
            dialogText.enabled = false;
        }
        if (secondaryCamera != null)
        {
            secondaryCamera.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            player.transform.position = targetPosition;
            if (mainCamera != null && secondaryCamera != null)
            {
                mainCamera.gameObject.SetActive(false);  
                secondaryCamera.gameObject.SetActive(true);
            }
        }
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
