using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveWorm_Cocoon : MonoBehaviour
{
    public GameObject pf_CaveWorm;
    public bool isFalling = false;
    public bool hasFallen = false;

    private EnemyHealthManager caveWormHealthManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SpawnCaveWorm();
            Destroy(gameObject);
            hasFallen = true;
        }
    }
    public void SpawnCaveWorm()
    {
        GameObject caveWorm = Instantiate(pf_CaveWorm,transform.position,Quaternion.identity);
        caveWormHealthManager = caveWorm.GetComponent<EnemyHealthManager>();
    }
    public float GetHealth()
    {
        if (caveWormHealthManager != null)
        {
            return caveWormHealthManager.health;
        }
        return 0f;
    }
    public void SetHealth(float healthValue)
    {
        if (caveWormHealthManager != null)
        {
            caveWormHealthManager.health = Mathf.Clamp(healthValue, 0f, caveWormHealthManager.maxHealth); 
            caveWormHealthManager.UpdateHealthbar();
        }
    }
}
