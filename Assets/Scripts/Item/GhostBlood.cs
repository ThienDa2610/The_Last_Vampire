using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlood : Collectible
{
    public TypeCoinManager ghostBloodManager;
    void Start()
    {
        ghostBloodManager = FindObjectOfType<TypeCoinManager>();
    }

    protected override void TakeEffect(Collider2D collision)
    {
        base.TakeEffect(collision);  

        ghostBloodManager.CollectGhost();
    }
}
