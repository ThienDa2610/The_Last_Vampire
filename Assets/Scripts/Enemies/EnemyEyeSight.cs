using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyeSight : MonoBehaviour
{
    public bool playerSpotted = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpotted = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpotted = false;
        }
    }
}
