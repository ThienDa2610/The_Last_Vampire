using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class CameraTileManager : MonoBehaviour
{
    public Camera camera4;
    public Camera camera5;
    public Tilemap tilemap;
    public TileBase tile; 
    public float tileLength = 20f; 
    private float maxDistance = 50f; 

    private float yTile;
    private List<Vector3Int> tilePositions = new List<Vector3Int>();

    void Update()
    {
        if (camera4.gameObject.activeSelf)
        {
            yTile = -31f;
            UpdateTileMap(camera4.transform.position);
        }
        else if (camera5.gameObject.activeSelf)
        {
            yTile = 103f;
            UpdateTileMap(camera5.transform.position);
        }
    }

    void UpdateTileMap(Vector3 cameraPosition)
    {
        float xStart = -28f;
        float xEnd = 31f;
        float cameraX = cameraPosition.x;

        float distanceToStart = Mathf.Abs(cameraX - xStart);
        float distanceToEnd = Mathf.Abs(cameraX - xEnd);
        if (distanceToStart < maxDistance || distanceToEnd < maxDistance)
        {
            CreateTileMap(xStart, xEnd, cameraPosition);
        }
        else
        {
            RemoveTilesOutsideCamera(cameraPosition);
        }
    }

    void CreateTileMap(float xStart, float xEnd, Vector3 cameraPosition)
    {
        for (float x = xStart; x <= xEnd; x++)
        {
            Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(x), Mathf.RoundToInt(yTile), 0);

            if (!tilePositions.Contains(tilePosition))
            {
                tilemap.SetTile(tilePosition, tile);
                tilePositions.Add(tilePosition);
            }
        }

        float cameraX = cameraPosition.x;

        for (float x = cameraX - maxDistance; x < cameraX; x++)
        {
            Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(x), Mathf.RoundToInt(yTile), 0);
            if (!tilePositions.Contains(tilePosition))
            {
                tilemap.SetTile(tilePosition, tile);
                tilePositions.Add(tilePosition);
            }
        }

        for (float x = cameraX; x < cameraX + maxDistance; x++)
        {
            Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(x), Mathf.RoundToInt(yTile), 0);
            if (!tilePositions.Contains(tilePosition))
            {
                tilemap.SetTile(tilePosition, tile);
                tilePositions.Add(tilePosition);
            }
        }

        RemoveTilesOutsideCamera(cameraPosition);
    }

    void RemoveTilesOutsideCamera(Vector3 cameraPosition)
    {
        List<Vector3Int> tilesToRemove = new List<Vector3Int>();

        foreach (var tilePosition in tilePositions)
        {
            float distance = Mathf.Abs(cameraPosition.x - tilePosition.x);
            if (distance > maxDistance)
            {
                tilesToRemove.Add(tilePosition); 
            }
        }

        foreach (var tilePosition in tilesToRemove)
        {
            tilemap.SetTile(tilePosition, null); 
            tilePositions.Remove(tilePosition); 
        }
    }
}
