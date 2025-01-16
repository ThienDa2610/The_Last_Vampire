using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    public static MapLoader Instance;

    public Animator transition;
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
           Instance = this;
    }
    public void LoadMap(string mapName)
    {
        StartCoroutine(Load(mapName));
    }

    IEnumerator Load(string mapName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(mapName);
    }
}
