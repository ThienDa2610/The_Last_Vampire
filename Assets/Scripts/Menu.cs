using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Map1_Forest");
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
