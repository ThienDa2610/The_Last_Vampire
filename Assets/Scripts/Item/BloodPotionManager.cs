using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BloodPotionManager : MonoBehaviour
{
    public TMP_Text bottleCountText;  
    public int bottleCount = 3; 
    public float valueBlood = 30;

    void Start()
    {
        if (PlayerPrefs.HasKey("SavedBloodPotionCount"))
        {
            bottleCount = PlayerPrefs.GetInt("SavedBloodPotionCount");
        }
        
        UpdateBottleCountText();
    }

    public void CollectBloodBottle()
    {
        bottleCount++;  
        UpdateBottleCountText();  
    }

    public void UpdateBottleCountText()
    {
        bottleCountText.text = "x" + bottleCount.ToString();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && bottleCount > 0)
        {
            UseBloodPotion();
        }
    }
    void UseBloodPotion()
    {
        bottleCount--;
        HealthManager.Instance.Heal(valueBlood);
        UpdateBottleCountText(); 
    }
    
}
