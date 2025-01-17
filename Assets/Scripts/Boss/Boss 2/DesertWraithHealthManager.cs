using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesertWraithHealthManager : EnemyHealthManager
{
    public GameObject bloodWaveIcon;

    public Image donedialogImage;
    public TMP_Text donedialogText;
    public string doneidleMessage;

    private void Start()
    {
        if (donedialogText != null)
        {
            donedialogText.enabled = false;
            donedialogImage.enabled = false;
        }

    }
    protected override void Die()
    {
        CastBloodWave.bloodWaveLearned = true;
        bloodWaveIcon.SetActive(true);
        StartCoroutine(ShowDialogForTime(2f));
        base.Die();
    }

    private IEnumerator ShowDialogForTime(float timeToShow)
    {
        donedialogText.enabled = true;
        donedialogImage.enabled = true;
        donedialogText.text = doneidleMessage;
        yield return new WaitForSecondsRealtime(timeToShow);  // Wait for the specified time

        donedialogText.enabled = false;
        donedialogImage.enabled = false;

    }
}
