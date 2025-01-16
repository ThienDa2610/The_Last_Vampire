using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    private GameObject player;
    public Button resumeButton;
    public TMP_Text resumeButtonText;
    public string resumeButtonTextDefault;

    private EventTrigger resumeButtonEventTrigger;

    private void Start()
    {
        MusicManager.Instance.PlayMusic("Menu");
        resumeButtonEventTrigger = resumeButton.GetComponent<EventTrigger>();
        if (PlayerPrefs.HasKey("SavedPositionX") && PlayerPrefs.HasKey("SavedPositionY") && PlayerPrefs.HasKey("SavedPositionZ"))
        {
            resumeButton.interactable = true;
            resumeButtonText.text = resumeButtonTextDefault;
            resumeButtonEventTrigger.enabled = true;
        }
        else
        {
            resumeButton.interactable = false;
            resumeButtonEventTrigger.enabled = false;
        }
    }
    public void NewGame()
    {
        //Xoa du lieu cu
        CheckPoint.ClearGameData();
        CheckPointJSON.DeleteSaveFile();

        MapLoader.Instance.LoadMap("Map1_Forest");
        MusicManager.Instance.PlayMusic("Level_1");

    }

    public void Resume()
    {
        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "Map1_Forest");
        SceneManager.LoadScene(savedSceneName);

        //CheckPoint.GetComponent<CheckPoint>().LoadGame();
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
    public void QuitGame()
    {
        Application.Quit();
    }

    /*public void No_ReStart()
    {
        MapLoader.Instance.LoadMap("Menu");
        MusicManager.Instance.PlayMusic("Menu");
    }*/
    
}
