using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorChangeAndInput : MonoBehaviour
{
    public Image[] images;    
    public TMP_Text[] texts;  
    private int currentIndex = 0;  
    private string[] inputValues = new string[4];

    public bool isPuzzleDone = false;

    void Start()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            inputValues[i] = "00";
            texts[i].text = inputValues[i];
        }
        images[0].color = Color.green;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveGreenLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveGreenRight();
        }

        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))  
            {
                HandleInput(i.ToString());
            }
        }
        if (inputValues[0] == "04" && inputValues[1] == "03" && inputValues[2] == "04" && inputValues[3] == "06")
        {
            isPuzzleDone = true;
        }
    }
    void MoveGreenLeft()
    {
        images[currentIndex].color = Color.black;
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;
        images[currentIndex].color = Color.green;
    }

    void MoveGreenRight()
    {
        images[currentIndex].color = Color.black;
        currentIndex = (currentIndex + 1) % images.Length;
        images[currentIndex].color = Color.green;
    }

    void HandleInput(string input)
    {
        inputValues[currentIndex] = inputValues[currentIndex].Substring(1) + input;
        texts[currentIndex].text = inputValues[currentIndex];
    }
    public void SetPuzzleState(bool state)
    {
        if (isPuzzleDone != state)
        {
            isPuzzleDone = state;
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
