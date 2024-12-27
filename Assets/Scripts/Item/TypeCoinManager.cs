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
