using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VasePuzzle : MonoBehaviour
{
    public Image[] images;
    public Image[] images_vase;
    public Sprite[] waterBottles;
    private int currentIndex = 0;
    private int[] waterBottleStates = new int[4];

    public bool isPuzzleDone = false;

    void Start()
    {
        images[0].color = Color.green;
        for (int i = 0; i < waterBottleStates.Length; i++)
        {
            waterBottleStates[i] = 1;
            images_vase[i].sprite = waterBottles[0];  
        }
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            waterBottleStates[currentIndex] = (waterBottleStates[currentIndex] % 4) + 1;
            images_vase[currentIndex].sprite = waterBottles[waterBottleStates[currentIndex] - 1];
        }

        if (waterBottleStates[0] == 2 && waterBottleStates[1] == 4 && waterBottleStates[2] == 3 && waterBottleStates[3] == 4)
        {
            isPuzzleDone = true;
        }
        UpdateSelection();
    }
    void UpdateSelection()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = (i == currentIndex) ? Color.green : Color.black;
        }
    }
    void MoveGreenLeft()
    {
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;
    }

    void MoveGreenRight()
    {
        currentIndex = (currentIndex + 1) % images.Length;
    }

}
