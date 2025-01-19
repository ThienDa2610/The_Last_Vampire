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
    public Options options;
    private void Start()
    {
        SetMenuAudioSettings();
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
    private void SetMenuAudioSettings()
    {
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);

        options.SetSFX(savedSFXVolume);
        options.SetMusic(savedMusicVolume);
    }

    public void NewGame()
    {
        //Xoa du lieu cu
        CheckPoint.ClearGameData();
        CheckPointJSON.DeleteSaveFile();
        Lv2CheckPoint.ClearGameData();
        Lv3CheckPoint.ClearGameData();
        Lv4CheckPoint.ClearGameData();
        Lv5CheckPoint.ClearGameData();
        MapLoader.Instance.LoadMap("Map1_Forest");
        MusicManager.Instance.PlayMusic("Level_1");
    }

    public void Resume()
    {
        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "Map1_Forest");
        MapLoader.Instance.LoadMap(savedSceneName);

        //CheckPoint.GetComponent<CheckPoint>().LoadGame();
        if (savedSceneName == "Map1_Forest")
        {
            MusicManager.Instance.PlayMusic("Level_1");
        }
        else if (savedSceneName == "Map2_Desert")
        {
            MusicManager.Instance.PlayMusic("Level_2");
        }
        else if (savedSceneName == "Map3_City")
        {
            MusicManager.Instance.PlayMusic("Level_3");
        }
        else if (savedSceneName == "Map4_Cave")
        {
            MusicManager.Instance.PlayMusic("Level_4");
        }
        else if (savedSceneName == "Map5_Ruin")
        {
            MusicManager.Instance.PlayMusic("Level_5");
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
