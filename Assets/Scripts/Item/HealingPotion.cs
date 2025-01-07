using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : Collectible
{
<<<<<<< HEAD
    private BloodPotionManager bloodPotionManager;
=======
    public BloodPotionManager bloodPotionManager;
>>>>>>> BaoDi
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
