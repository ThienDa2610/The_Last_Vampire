using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : Collectible
{
    protected override void TakeEffect(Collider2D collision)
    {
        HealthManager.Instance.Heal(value);
    }
}
