using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDefeatedAndSceneChanger : MonoBehaviour
{
    public GameObject Boss;
    public GameObject player;
    private Vector3 triggerPosition1;

    void Start()
    {
        triggerPosition1 = new Vector3(241f, triggerPosition1.y, triggerPosition1.z);
    }

    void Update()
    {
        if (Boss == null && player != null)
        {
            if (Vector3.Distance(new Vector3(player.transform.position.x, 0f, 0f), new Vector3(triggerPosition1.x, 0f, 0f)) < 1f)
            {
                SceneManager.LoadScene("Map2_Desert");
            }
        }
    }
}
