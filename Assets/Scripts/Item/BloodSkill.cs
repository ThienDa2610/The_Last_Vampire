using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSkill : Collectible
{
    public TypeCoinManager bloodSkillManager;
    void Start()
    {
        bloodSkillManager = FindObjectOfType<TypeCoinManager>();
    }

    protected override void TakeEffect(Collider2D collision)
    {
        base.TakeEffect(collision);  

        bloodSkillManager.CollectBlood();
    }
}
