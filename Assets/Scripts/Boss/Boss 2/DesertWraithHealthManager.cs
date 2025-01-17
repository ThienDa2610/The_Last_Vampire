using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesertWraithHealthManager : EnemyHealthManager
{
    public GameObject bloodWaveIcon;

    
    protected override void Die()
    {
        CastBloodWave.bloodWaveLearned = true;
        bloodWaveIcon.SetActive(true);
        
        base.Die();
    }

}
