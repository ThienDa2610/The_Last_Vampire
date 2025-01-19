using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDefeatedAndSceneChanger : MonoBehaviour
{
    public GameObject Boss;
    public GameObject player;
    private Vector3 triggerPosition1;
    public float xTrigger;
    public string NextScene;
    public bool isScene3 = false;
    public bool PassLv = false;

    private bool hasSaved = false;

    void Start()
    {
        triggerPosition1 = new Vector3(xTrigger, triggerPosition1.y, triggerPosition1.z);
    }

    void Update()
    {
        if (isScene3)
        {
            if (DialogueManager.Instance.isNPCPaster && !DialogueManager.Instance.isOpen)
            {
                if (Boss == null && player != null)
                {
                    if (Vector3.Distance(new Vector3(player.transform.position.x, 0f, 0f), new Vector3(triggerPosition1.x, 0f, 0f)) < 1f)
                    {
                        switch (DialogueManager.Instance.chooseIndex)
                        {
                            case 0:
                                break;
                            case 1:
                                if (!hasSaved)  
                                {
                                    SaveBeforeNext();
                                    hasSaved = true;  
                                }
                                MapLoader.Instance.LoadMap("Map4_Cave");
                                break;
                            case 2:
                                if (!hasSaved)
                                {
                                    SaveBeforeNext();
                                    hasSaved = true;
                                }
                                MapLoader.Instance.LoadMap("Map5_Ruin");
                                break;
                        }
                    }
                }
            }
        }
        else if (PassLv)
        {
            if (!hasSaved)
            {
                SaveBeforeNext();
                hasSaved = true;
            }
            MapLoader.Instance.LoadMap(NextScene);
        }
        else
        {
            if (Boss == null && player != null)
            {
                if (Vector3.Distance(new Vector3(player.transform.position.x, 0f, 0f), new Vector3(triggerPosition1.x, 0f, 0f)) < 1f)
                {
                    if (!hasSaved)
                    {
                        SaveBeforeNext();
                        hasSaved = true;
                    }
                    MapLoader.Instance.LoadMap(NextScene);
                }
            }
        }
    }
    void SaveBeforeNext()
    {
        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "Map1_Forest");
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
    }
}
