using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveWorm_Cocoon : MonoBehaviour
{
    public GameObject pf_CaveWorm;
    public bool isFalling = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SpawnCaveWorm();
            Destroy(gameObject);
        }
    }
    void SpawnCaveWorm()
    {
        GameObject caveWorm = Instantiate(pf_CaveWorm,transform.position,Quaternion.identity);
    }
}
