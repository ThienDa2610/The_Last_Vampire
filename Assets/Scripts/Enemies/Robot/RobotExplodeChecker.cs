using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotExplodeChecker : MonoBehaviour
{
    RobotHealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = GetComponentInParent<RobotHealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            healthManager.isInExplodeRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            healthManager.isInExplodeRange = false;
        }
    }
}
