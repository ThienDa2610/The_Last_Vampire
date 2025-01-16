using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class FlowerTilemapSwitch : MonoBehaviour
{
    public Tilemap tilemapOld;  
    public GameObject tilemapNew;  
    public float detectionRadius = 10f;  

    private Transform player;

    public TMP_Text dialogText;
    public Image dialogImage;
    public string idleMessage1;
    public string idleMessage2;

    public bool hallucinated = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        tilemapOld.GetComponent<TilemapRenderer>().enabled = true; 
        tilemapNew.SetActive(false);
        if (dialogText != null)
        {
            dialogText.enabled = false;
            dialogImage.enabled = false;
        }
    }

    public void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > detectionRadius && hallucinated)
        {
            StartCoroutine(ShowDialogForTime(2f, idleMessage2));
            hallucinated = false;
            tilemapOld.GetComponent<TilemapRenderer>().enabled = true;
            tilemapNew.SetActive(false);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hallucinated)
            {
                hallucinated = true;
                StartCoroutine(ShowDialogForTime(3f, idleMessage1));
                tilemapOld.GetComponent<TilemapRenderer>().enabled = false;
                tilemapNew.SetActive(true);
            }
        }
    }
    private IEnumerator ShowDialogForTime(float timeToShow, string idleMessage)
    {
        if (dialogText != null && dialogText != null)
        {
            dialogText.enabled = true;
            dialogImage.enabled = true;
            dialogText.text = idleMessage; // Display saved message
        }

        yield return new WaitForSeconds(timeToShow);  // Wait for the specified time

        if (dialogText != null && dialogText != null)
        {
            dialogText.enabled = false; // Hide saved message
            dialogImage.enabled = false;
        }
    }
}
