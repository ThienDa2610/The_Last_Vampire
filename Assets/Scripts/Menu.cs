using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        MusicManager.Instance.PlayMusic("Menu");
    }
    public void NewGame()
    {
        MapLoader.Instance.LoadMap("Map1_Forest");
        MusicManager.Instance.PlayMusic("Level_1");
    }
    public void Resume()
    {
        return;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
