using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImageRow
{
    public Image[] imagesInRow;
    public Image[] bordersInRow;
}

public class Puzzle : MonoBehaviour
{
    public ImageRow[] rows = new ImageRow[4];
    public Color selectedBorderColor = Color.red;
    public Color selectedMoveBorderColor = Color.green;
    public Color defaultBorderColor = new Color(0f, 0f, 0f, 1f);

    private int currentX = 0;
    private int currentY = 0;
    private bool isSelected = false;

    public bool isPuzzleDone = false;

    void Start()
    {
        SetBorderColor(currentX, currentY, selectedBorderColor);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) MoveSelection(0, 1);
        if (Input.GetKeyDown(KeyCode.W)) MoveSelection(0, -1);
        if (Input.GetKeyDown(KeyCode.A)) MoveSelection(-1, 0);
        if (Input.GetKeyDown(KeyCode.D)) MoveSelection(1, 0);

        if (Input.GetKeyDown(KeyCode.Return)) ToggleSelection();
        if (isSelected)
        {
            if (Input.GetKeyDown(KeyCode.W)) MoveImage(0, -1);
            if (Input.GetKeyDown(KeyCode.S)) MoveImage(0, 1);
            if (Input.GetKeyDown(KeyCode.A)) MoveImage(-1, 0);
            if (Input.GetKeyDown(KeyCode.D)) MoveImage(1, 0);
        }
        CheckPuzzleCompletion();
        
    }
   
    void MoveSelection(int dx, int dy)
    {
        if (isSelected) return;

        SetBorderColor(currentX, currentY, defaultBorderColor);

        currentX = Mathf.Clamp(currentX + dx, 0, rows[0].imagesInRow.Length - 1);
        currentY = Mathf.Clamp(currentY + dy, 0, rows.Length - 1);

        SetBorderColor(currentX, currentY, selectedBorderColor);
    }
    void SetBorderColor(int x, int y, Color color)
    {
        Image image = rows[y].imagesInRow[x];
        Image border = rows[y].bordersInRow[x];
        if (border != null)
        {
            color.a = 1f;
            border.color = color;
        }
        /*Image[] borders = rows[y].bordersInRow;
        if (borders.Length >= 4)
        {
            borders[0].color = color;  // Top
        }*/
    }
    void ToggleSelection()
    {
        if (isSelected)
        {
            SetBorderColor(currentX, currentY, selectedBorderColor);
            isSelected = false;
        }
        else
        {
            SetBorderColor(currentX, currentY, selectedMoveBorderColor);
            isSelected = true;
        }
    }
    void MoveImage(int dx, int dy)
    {
        if (!isSelected) return;


        int newX = Mathf.Clamp(currentX + dx, 0, rows[0].imagesInRow.Length - 1);
        int newY = Mathf.Clamp(currentY + dy, 0, rows.Length - 1);

        if (newX != currentX || newY != currentY)
        {
            Image tempImage = rows[currentY].imagesInRow[currentX];
            rows[currentY].imagesInRow[currentX] = rows[newY].imagesInRow[newX];
            rows[newY].imagesInRow[newX] = tempImage;

            Image tempBorder = rows[currentY].bordersInRow[currentX];
            rows[currentY].bordersInRow[currentX] = rows[newY].bordersInRow[newX];
            rows[newY].bordersInRow[newX] = tempBorder;

            RectTransform currentBorderRectTransform = rows[currentY].bordersInRow[currentX].rectTransform;
            RectTransform newBorderRectTransform = rows[newY].bordersInRow[newX].rectTransform;

            Vector2 tempBorderPosition = currentBorderRectTransform.anchoredPosition;
            currentBorderRectTransform.anchoredPosition = newBorderRectTransform.anchoredPosition;
            newBorderRectTransform.anchoredPosition = tempBorderPosition;

            currentX = newX;
            currentY = newY;

            //SetBorderColor(currentX, currentY, selectedMoveBorderColor);

            LayoutRebuilder.ForceRebuildLayoutImmediate(currentBorderRectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(newBorderRectTransform);
        }
    }
    void CheckPuzzleCompletion()
    {
        isPuzzleDone = true;
        int borderIndex = 1;
        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].imagesInRow.Length; x++)
            {
                string expectedBorderName = borderIndex.ToString();

                Image currentBorder = rows[y].bordersInRow[x];


                if (currentBorder != null && currentBorder.name != expectedBorderName)
                {
                    isPuzzleDone = false;
                    break;
                }
                borderIndex++;
            }

            if (!isPuzzleDone) break;
        }
       
    }
    public void SetPuzzleState (bool state)
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