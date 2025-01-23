using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LearnNewSkillNotice : MonoBehaviour
{
    public GameObject object1;  
    public GameObject object2;

    public Image donedialogImage;
    public TMP_Text donedialogText;
    public string doneidleMessage1;
    public string doneidleMessage2;
    private bool Skill1 = false;
    private bool Skill2 = false;

    private void Start()
    {
        if (donedialogText != null)
        {
            donedialogText.enabled = false;
            donedialogImage.enabled = false;
        }
    }
    private void Update()
    {
        if (object1.activeSelf && !Skill1)
        {
            StartCoroutine(ShowDialogForTime(2f, doneidleMessage1));
            Skill1 = true;
        }
        if (object2.activeSelf && !Skill2)
        {
            StartCoroutine(ShowDialogForTime(2f, doneidleMessage2));
            Skill2 = true;
        }
    }
    private IEnumerator ShowDialogForTime(float timeToShow, string idleMessages)
    {
        donedialogText.enabled = true;
        donedialogImage.enabled = true;
        donedialogText.text = idleMessages;
        yield return new WaitForSecondsRealtime(timeToShow);  // Wait for the specified time

        donedialogText.enabled = false;
        donedialogImage.enabled = false;

    }
}
