using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapOpacity : MonoBehaviour
{
    public Tilemap tilemap1;  // Tilemap1
    public Tilemap tilemap2;  // Tilemap2

    // Start is called before the first frame update
    void Start()
    {
        SetTilemapOpacity(tilemap2, 1f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  
        {
            SetTilemapOpacity(tilemap2, 0f);  
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  
        {
            SetTilemapOpacity(tilemap2, 1f);  
        }
    }
    private void SetTilemapOpacity(Tilemap tilemap, float opacity)
    {
        Color color = tilemap.color;
        color.a = opacity;
        tilemap.color = color;
    }
}
