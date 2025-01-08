using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSand : MonoBehaviour
{
    public Movement movement;
    public float slowPercentage = 0.5f;
    public float duration = 5f;
    private float speedDif;
    private void Start()
    {
        Invoke("SelfDestruct", duration);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            speedDif = movement.moveSpeed * slowPercentage;
            movement.moveSpeed -= speedDif;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movement.moveSpeed += speedDif;
            speedDif = 0;
        }
    }
    private void SelfDestruct()
    {
        if (speedDif > 0)
            movement.moveSpeed += speedDif;
        Destroy(gameObject);
    }
}
