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
                                MapLoader.Instance.LoadMap("Map4_Cave");
                                break;
                            case 2:
                                MapLoader.Instance.LoadMap("Map5_Ruin");
                                break;
                        }
                    }
                }
            }
        }
        else if (PassLv)
        {
            MapLoader.Instance.LoadMap(NextScene);
        }
        else
        {
            if (Boss == null && player != null)
            {
                if (Vector3.Distance(new Vector3(player.transform.position.x, 0f, 0f), new Vector3(triggerPosition1.x, 0f, 0f)) < 1f)
                {
                    MapLoader.Instance.LoadMap(NextScene);
                }
            }
        }
    }
}
