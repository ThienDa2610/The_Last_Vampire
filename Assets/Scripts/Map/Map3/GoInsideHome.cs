using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoInsideHome : PlayerInteractGuide
{
    public Camera mainCamera;
    public Camera secondaryCamera;
    public Vector3 targetPos;
    public GameObject player;

    public Button firstButton;

    [SerializeField] public TMP_Text ans;

    public Canvas gamePlay;
    public Canvas password;

    private bool isIt = false;
    private string answer = "163693";
    protected override void SetupMore()
    {
        base.SetupMore();
        if (secondaryCamera != null)
        {
            secondaryCamera.gameObject.SetActive(false);
        }
        ans.text = "";
        gamePlay.gameObject.SetActive(true);
        password.gameObject.SetActive(false);
    }
    protected override void Interact()
    {
        base.Interact();
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            isIt = true;
            gamePlay.gameObject.SetActive(false);
            password.gameObject.SetActive(true);
            firstButton.Select();
            Time.timeScale = 0f;
        }
        if (isIt)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Close();
        }
    }

    public void Close()
    {
        gamePlay.gameObject.SetActive(true);
        password.gameObject.SetActive(false);
        ans.text = "";
        Time.timeScale = 1f;
    }

    public void Number(int num)
    {
        if (ans.text.Length >= 6)
            return;
        ans.text += num.ToString();
    }

    public void Execute()
    {
        if (ans.text == answer)
        {
            Close();
            player.transform.position = targetPos;
            if (mainCamera != null && secondaryCamera != null)
            {
                mainCamera.gameObject.SetActive(false);
                secondaryCamera.gameObject.SetActive(true);
            }
            Time.timeScale = 1f;
        }
        else
        {
            StartCoroutine(Incorrect());
        }
    }

    private IEnumerator Incorrect()
    {
        ans.text = "INCORRECT!";
        yield return new WaitForSecondsRealtime(2f);
        ans.text = "";
    }
}
