using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        MusicManager.Instance.PlayMusic("Menu");
    }
    public void NewGame()
    {
        //player = GameObject.FindWithTag("Player");
        CheckPoint.ClearGameData();
        CheckPointJSON.DeleteSaveFile();

        MapLoader.Instance.LoadMap("Map1_Forest");
        MusicManager.Instance.PlayMusic("Level_1");
        /*if (player != null)
        {
            
            player.transform.position = new Vector3(-6f, -1f, 5f);
        }*/
    }
    public void No_ReStart()
    {
        MapLoader.Instance.LoadMap("Menu");
        MusicManager.Instance.PlayMusic("Menu");
    }
}
