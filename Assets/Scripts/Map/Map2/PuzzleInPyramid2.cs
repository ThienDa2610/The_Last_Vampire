using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleInPyramid2 : MonoBehaviour
{
    public Image dialogImage;
    public TMP_Text dialogText;
    public string idleMessage;

    public Image donedialogImage;
    public TMP_Text donedialogText;
    public string doneidleMessage;

    private bool isPlayerNear = false;

    public GameObject tilemapHide;

    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject tilemapSeen;

    public GameObject waterHide;
    public GameObject[] waterSeen;
    public Canvas gameplayCanvas;
    public Canvas puzzleCanvas;

    public ColorChangeAndInput puzzleScript_number;
    public VasePuzzle puzzleScript_vase;

    public PauseMenu isIt;

    public int Puzzle1Or2 = 1;
    public bool done = false;

    void Start()
    {
        if (dialogText != null)
        {
            dialogText.enabled = false;
            dialogImage.enabled = false;
        }
        puzzleCanvas.gameObject.SetActive(false);
        if (donedialogText != null)
        {
            donedialogText.enabled = false;
            donedialogImage.enabled = false;
        }

        if (tilemapSeen != null) { Puzzle1Or2 = 1; }
        else { Puzzle1Or2 = 2; }

        if (Puzzle1Or2 == 1)
        {
            if (!puzzleScript_number.isPuzzleDone)
            {
                tilemapHide.SetActive(true);
                tilemapSeen.SetActive(false);
                obj1.SetActive(true);
                obj2.SetActive(true);
                obj3.SetActive(true);
                obj4.SetActive(true);
            }
            else
            {
                tilemapHide.SetActive(false);
                tilemapSeen.SetActive(true);
                obj1.SetActive(false);
                obj2.SetActive(false);
                obj3.SetActive(false);
                obj4.SetActive(false);
            }
        }
        else
        {
            if (!puzzleScript_vase.isPuzzleDone)
            {
                tilemapHide.SetActive(true);
                waterHide.SetActive(true);
                foreach (GameObject waterObject in waterSeen)
                {
                    if (waterObject != null)
                    {
                        waterObject.SetActive(false); 
                    }
                }
            }
            else
            {
                tilemapHide.SetActive(false);
                waterHide.SetActive(false);
                foreach (GameObject waterObject in waterSeen)
                {
                    if (waterObject != null)
                    {
                        waterObject.SetActive(true); 
                    }
                }
            }
        }
    }

    void Update()
    {
        if (Puzzle1Or2 == 1)
        {
            if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !puzzleScript_number.isPuzzleDone)
            {
                puzzleCanvas.gameObject.SetActive(true);
                gameplayCanvas.gameObject.SetActive(false);
                isIt.isIt = false;
                Time.timeScale = 0f;
                dialogText.enabled = false;
                dialogImage.enabled = false;
            }
            if (puzzleScript_number.isPuzzleDone)
            {
                StartCoroutine(HandlePuzzleCompletion());
            }
        }
        else
        {
            if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !puzzleScript_vase.isPuzzleDone)
            {
                puzzleCanvas.gameObject.SetActive(true);
                gameplayCanvas.gameObject.SetActive(false);
                isIt.isIt = false;
                Time.timeScale = 0f;
                dialogText.enabled = false;
                dialogImage.enabled = false;
            }
            if (puzzleScript_vase.isPuzzleDone)
            {
                StartCoroutine(HandlePuzzleCompletion());
            }
        }
        if (!isIt.isIt && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePuzzle();
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
        puzzleCanvas.gameObject.SetActive(false);

        if (Puzzle1Or2 == 1)
        {
            tilemapSeen.SetActive(true);
            obj1.SetActive(false);
            obj2.SetActive(false);
            obj3.SetActive(false);
            obj4.SetActive(false);
            tilemapHide.SetActive(false);
        }
        else
        {
            tilemapHide.SetActive(false);
            waterHide.SetActive(false);
            foreach (GameObject waterObject in waterSeen)
            {
                if (waterObject != null)
                {
                    waterObject.SetActive(true);
                }
            }
            if (!done)
            {
                StartCoroutine(ShowDialogForTime(2f));
            }
        }
        
        Time.timeScale = 1f;
        gameplayCanvas.gameObject.SetActive(true);
        isIt.isIt = true;   
    }

    public void ClosePuzzle()
    {
        puzzleCanvas.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
        isIt.isIt = true;
        Time.timeScale = 1f;
        dialogText.enabled = true;
        dialogImage.enabled = true;
        dialogText.text = idleMessage;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Puzzle1Or2 == 1)
            {
                if (!puzzleScript_number.isPuzzleDone)
                {
                    isPlayerNear = true;

                    dialogText.enabled = true;
                    dialogImage.enabled = true;
                    dialogText.text = idleMessage;
                }
            }
            else if(Puzzle1Or2 == 2)
            {
                if (!puzzleScript_vase.isPuzzleDone)
                {
                    isPlayerNear = true;

                    dialogText.enabled = true;
                    dialogImage.enabled = true;
                    dialogText.text = idleMessage;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Puzzle1Or2 == 1)
            {
                if (!puzzleScript_number.isPuzzleDone)
                {
                    isPlayerNear = false;
                    dialogText.enabled = false;
                    dialogImage.enabled = false;
                }
            }
            else if (Puzzle1Or2 == 2)
            {
                if (!puzzleScript_vase.isPuzzleDone)
                {
                    isPlayerNear = false;
                    dialogText.enabled = false;
                    dialogImage.enabled = false;
                }
            }
            
        }
    }
}
