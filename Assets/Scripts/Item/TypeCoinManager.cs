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
    

    void Start()
    {
        if (PlayerPrefs.HasKey("SavedBloodCount"))
        {
            bloodCount = PlayerPrefs.GetInt("SavedBloodCount");
        }
        if (PlayerPrefs.HasKey("SavedGhostCount"))
        {
            ghostCount = PlayerPrefs.GetInt("SavedGhostCount");
            //test
            if(ghostCount == 0)
            {
                ghostCount = 12;
            }
        }
        UpdateGhostCountText();
        UpdateBloodCountText();
    }

    public void CollectGhost()
    {
        ghostCount++;
        UpdateGhostCountText();
    }
    public void CollectBlood()
    {
        bloodCount++;
        UpdateBloodCountText();
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
}
