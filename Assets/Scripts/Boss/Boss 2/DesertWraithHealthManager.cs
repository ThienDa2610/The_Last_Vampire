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
        PlayerPrefs.SetInt("BloodWaveState", CastBloodWave.bloodWaveLearned ? 1 : 0);
        base.Die();
    }

}
