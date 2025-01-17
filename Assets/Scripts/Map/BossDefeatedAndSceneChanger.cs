using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDefeatedAndSceneChanger : MonoBehaviour
{
    public GameObject Boss;
    public GameObject player;
    private Vector3 triggerPosition1;
    public float xTrigger = 241f;
    public string NextScene = "Map2_Desert";

    public bool PassLv = false;

    void Start()
    {
        triggerPosition1 = new Vector3(xTrigger, triggerPosition1.y, triggerPosition1.z);
    }

    void Update()
    {
        if (Boss == null && player != null)
        {
            if (Vector3.Distance(new Vector3(player.transform.position.x, 0f, 0f), new Vector3(triggerPosition1.x, 0f, 0f)) < 1f)
            {
                SceneManager.LoadScene(NextScene);
            }
        }
        else if (PassLv)
        {
            SceneManager.LoadScene(NextScene);
        }
    }
}
