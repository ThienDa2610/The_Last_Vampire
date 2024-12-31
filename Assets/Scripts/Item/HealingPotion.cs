using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : Collectible
{
    public BloodPotionManager bloodPotionManager;
    void Start()
    {
        bloodPotionManager = FindObjectOfType<BloodPotionManager>();
    }

    protected override void TakeEffect(Collider2D collision)
    {
        base.TakeEffect(collision);  

        bloodPotionManager.CollectBloodBottle();
    }
}
