using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedLand : MonoBehaviour
{
    public float duration;
    private void Start()
    {
        Invoke("SelfDestruct", duration);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StatusManager.Instance.InflictTrap();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StatusManager.Instance.CleanseTrap();
        }
    }
    void SelfDestruct()
    {
        StatusManager.Instance.CleanseTrap();
        Destroy(gameObject);
    }
}
