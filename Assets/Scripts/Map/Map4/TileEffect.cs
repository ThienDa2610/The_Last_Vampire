using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileEffect : MonoBehaviour
{
    public GameObject tilemapObject; 
    private float shakeDuration = 2f;
    private float vanishDuration = 1f;
    //private float restoreDuration = 3f;  
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear)
        {
            StartCoroutine(HandleTilemapEffect());

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private IEnumerator HandleTilemapEffect()
    {
        StartCoroutine(ShakeTilemap());
        yield return new WaitForSecondsRealtime(shakeDuration);
        
        yield return StartCoroutine(HandleTilemapVanishing());

    }
    private IEnumerator HandleTilemapVanishing()
    {
        tilemapObject.SetActive(false);
        Invoke("RestoreTilemap", vanishDuration);

        yield return null;
    }
    private void RestoreTilemap()
    {
        tilemapObject.SetActive(true);
        isPlayerNear = false;
    }
    private IEnumerator ShakeTilemap()
    {
        Vector3 originalPosition = tilemapObject.transform.position; 
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 shakeOffset = Random.insideUnitCircle * 0.1f;  
            tilemapObject.transform.position = originalPosition + shakeOffset;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tilemapObject.transform.position = originalPosition;
    }
}
