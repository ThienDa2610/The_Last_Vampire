using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public Button firstButton;
    private Canvas pauseMenu;
    public Canvas gameplayCanvas;
    private Animator animator;
    public bool isPaused = false;

    private RectTransform pauseMenuRect;
    public float slideSpeed = 5f;

    public bool isIt = true;

    private void Start()
    {
        pauseMenu = GetComponent<Canvas>();
        pauseMenu.enabled = false;

        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
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
        gameplayCanvas.enabled = false;
        isPaused = true;
        pauseMenu.enabled = true;

        StartCoroutine(SlideIn());

        firstButton.Select();
        Time.timeScale = 0f;
    }

    private IEnumerator SlideIn()
    {
        animator.SetBool("isOpen", true);
        yield return new WaitForSecondsRealtime(1f);
    }
    public void Continue()
    {
        StartCoroutine(SlideOut());
        gameplayCanvas.enabled = true;
        isPaused = false;
        Time.timeScale = 1f;
    }

    private IEnumerator SlideOut()
    {
        animator.SetBool("isOpen", false);
        yield return new WaitForSecondsRealtime(1f);
        pauseMenu.enabled = false;        
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "Map1_Forest");
        CheckPointJSON.DeleteSaveFile();
        if (savedSceneName == "Map1_Forest")
        {
            CheckPoint.ClearGameData();
        }
        else if (savedSceneName == "Map2_Desert")
        {
            Lv2CheckPoint.ClearGameData();
        }
        else if (savedSceneName == "Map3_City")
        {
            Lv3CheckPoint.ClearGameData();
        }
        else if (savedSceneName == "Map4_Cave")
        {
            Lv4CheckPoint.ClearGameData();
        }
        else if (savedSceneName == "Map5_Ruin")
        {
            Lv5CheckPoint.ClearGameData();
        }

        MapLoader.Instance.LoadMap(savedSceneName);
    }
    public void Quit()
    {
        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "Map1_Forest");
        CheckPointJSON.Instance.SaveGame();
        
        if (savedSceneName == "Map1_Forest")
        {
            CheckPoint.Instance.SaveGame();
        }
        else if (savedSceneName == "Map2_Desert")
        {
            Lv2CheckPoint.Instance.SaveGame();
        }
        else if (savedSceneName == "Map3_City")
        {
            Lv3CheckPoint.Instance.SaveGame();
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
    }
}
