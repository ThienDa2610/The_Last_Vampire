using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHeart : Collectible
{
    public TypeCoinManager dragonHeartManager;
    void Start()
    {
        dragonHeartManager = FindObjectOfType<TypeCoinManager>();
    }

    protected override void TakeEffect(Collider2D collision)
    {
        base.TakeEffect(collision);  

        dragonHeartManager.CollectDragonHeart();
    }
}
