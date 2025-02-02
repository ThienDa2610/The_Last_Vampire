using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeCoinManager : MonoBehaviour
{
    public TMP_Text GhostCountText;
    public TMP_Text BloodCountText;

    public int ghostCount = 0;
    public int bloodCount = 0;
    public bool dragonHeart = false;

    private SkillTreeUIManager manager;

    public Image getHeartdialogImage;
    public TMP_Text getHeartdialogText;
    public string getHeartidleMessage;

    void Start()
    {
        manager = GameObject.Find("/Canvas-SkillTree").GetComponent<SkillTreeUIManager>();
        if (PlayerPrefs.HasKey("SavedBloodCount"))
        {
            bloodCount = PlayerPrefs.GetInt("SavedBloodCount");
        }
        if (PlayerPrefs.HasKey("SavedGhostCount"))
        {
            ghostCount = PlayerPrefs.GetInt("SavedGhostCount");
        }
        if (getHeartdialogText != null)
        {
            getHeartdialogText.enabled = false;
            getHeartdialogImage.enabled = false;
        }
        UpdateBloodCountText();
        UpdateGhostCountText();
    }

    public void CollectGhost()
    {
        ghostCount++;
        UpdateGhostCountText();
    }
    public void CollectBlood()
    {
        bloodCount++;
        
        manager.bloodCount = bloodCount;
        manager.UpdatebloodCountText();
        UpdateBloodCountText();
    }

    public void CosumeBlood(int value)
    {
        bloodCount -= value;
        UpdateBloodCountText();
    }
    public void CollectDragonHeart()
    {
        dragonHeart = true;
        StartCoroutine(ShowDialogForTime(2f, getHeartidleMessage));
    }

    public void UpdateGhostCountText()
    {
        GhostCountText.text = ghostCount.ToString();
    }
    public void UpdateBloodCountText()
    {
        BloodCountText.text = bloodCount.ToString();
    }
    public bool SpendGhost(int amount)
    {
        if (ghostCount >= amount)
        {
            ghostCount -= amount;
            UpdateGhostCountText();
            return true;
        }
        return false; 
    }
    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.R) && bottleCount > 0)
        {
            UseBloodPotion();
        }*/
    }
    /*void UseBloodPotion()
    {
        bottleCount--;
        HealthManager.Instance.Heal(valueBlood);
        UpdateBottleCountText();
    }*/
    private IEnumerator ShowDialogForTime(float timeToShow, string idleMessages)
    {
        getHeartdialogText.enabled = true;
        getHeartdialogImage.enabled = true;
        getHeartdialogText.text = idleMessages;
        yield return new WaitForSecondsRealtime(timeToShow);  // Wait for the specified time

        getHeartdialogText.enabled = false;
        getHeartdialogImage.enabled = false;

    }
}
