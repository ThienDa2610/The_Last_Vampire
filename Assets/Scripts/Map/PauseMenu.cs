﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public Canvas gameplayCanvas;
    private bool isPaused = false;

    private RectTransform pauseMenuRect;
    public float slideSpeed = 5f;

    public bool isIt = true;
    private void Start()
    {
        gameplayCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
        pauseMenuRect = gameObject.GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (isIt && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                Continue();
            }
        }
    }
    public void PauseGame()
    {
        gameplayCanvas.gameObject.SetActive(false);
        isPaused = true;
        gameObject.SetActive(true);

        StartCoroutine(SlideInMenu());

        Time.timeScale = 0f;  
    }
    public void Continue()
    {
        gameplayCanvas.gameObject.SetActive(true);
        isPaused = false;
        //pauseMenu.SetActive(false);

        StartCoroutine(SlideOutMenu());

        Time.timeScale = 1f;
    }
    public void Replay()
    {
        Time.timeScale = 1f;
        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "Map1_Forest");

        if (savedSceneName == "Map1_Forest")
        {
            CheckPoint.ClearGameData();
            CheckPointJSON.DeleteSaveFile();
        }
        else if (savedSceneName == "Map2_Desert")
        {
            Lv2CheckPoint.ClearGameData();
        }
        else if (savedSceneName == "Map3_City")
        {
            Lv4CheckPoint.ClearGameData();
        }
        else if (savedSceneName == "Map4_Cave")
        {
            Lv4CheckPoint.ClearGameData();
        }
        else if (savedSceneName == "Map5_Ruin")
        {
            Lv5CheckPoint.ClearGameData();
        }

        SceneManager.LoadScene(savedSceneName);

        if (savedSceneName == "Map1_Forest")
        {
            MusicManager.Instance.PlayMusic("Level_1");
        }
        else if (savedSceneName == "Map2_Desert")
        {
            MusicManager.Instance.PlayMusic("Level_1");
        }
        else if (savedSceneName == "Map3_City")
        {
            MusicManager.Instance.PlayMusic("Level_1");
        }
        else if (savedSceneName == "Map4_Cave")
        {
            MusicManager.Instance.PlayMusic("Level_1");
        }
        else if (savedSceneName == "Map5_Ruin")
        {
            MusicManager.Instance.PlayMusic("Level_1");
        }
    }
    public void Quit()
    {
        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "Map1_Forest");
        if (savedSceneName == "Map1_Forest")
        {
            CheckPoint.Instance.SaveGame();
            CheckPointJSON.Instance.SaveGame();
        }
        else if (savedSceneName == "Map2_Desert")
        {
            Lv2CheckPoint.Instance.SaveGame();
        }
        else if (savedSceneName == "Map3_City")
        {
            Lv2CheckPoint.Instance.SaveGame();
        }
        else if (savedSceneName == "Map4_Cave")
        {
            Lv4CheckPoint.Instance.SaveGame();
        }
        else if (savedSceneName == "Map5_Ruin")
        {
            Lv5CheckPoint.Instance.SaveGame();
        }
        
        Time.timeScale = 1f;
        MapLoader.Instance.LoadMap("Menu");
        MusicManager.Instance.PlayMusic("Menu");
    }
   
    private IEnumerator SlideInMenu()
    {
        Vector3 targetPosition = Vector3.zero; 
        Vector3 startPosition = new Vector3(Screen.width, 0, 0); 
        pauseMenuRect.anchoredPosition = startPosition;

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.unscaledDeltaTime * slideSpeed;
            pauseMenuRect.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            yield return null;
        }
        pauseMenuRect.anchoredPosition = targetPosition;
        
    }

    private IEnumerator SlideOutMenu()
    {
        Vector3 targetPosition = new Vector3(Screen.width, 0, 0); 
        Vector3 startPosition = pauseMenuRect.anchoredPosition;

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.unscaledDeltaTime * slideSpeed;
            pauseMenuRect.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            yield return null;
        }
        pauseMenuRect.anchoredPosition = targetPosition; 
        gameObject.SetActive(false); 
    }
}
