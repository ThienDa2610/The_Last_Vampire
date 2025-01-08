using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeUIManager : MonoBehaviour
{
    public TMP_Text bloodCountText;

    public GameObject skillTree;
    public Canvas gameplayCanvas;
    public TMP_Text bloodText;  
    public static SkillTreeUIManager instance;   
    public Skill[] skills;
    public SkillButton[] SkillButton;
    public Skill activeSkill;

    public int bloodCount = 0;

    public Button unLockButton;
    public TMP_Text buttonText;
    public Image buttonImage;

    private bool isOpen = false;

    void Start()
    {
        skillTree.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
        if (PlayerPrefs.HasKey("SavedBloodCount"))
        {
            bloodCount = PlayerPrefs.GetInt("SavedBloodCount");
            // test
            if(bloodCount == 0)
            {
                bloodCount = 12;
            }
        }
        UpdatebloodCountText();
    }


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!isOpen && Input.GetKeyDown(KeyCode.C))
        {
            skillTree.SetActive(true);
            gameplayCanvas.gameObject.SetActive(false);
            isOpen = true;
            Time.timeScale = 0f;
            if (activeSkill == null)
            {
                activeSkill = skills[0];
            }
        }
        if (isOpen)
        {
            checkUnLock();
        }
    }

    public void UpdatebloodCountText()
    {
        bloodCountText.text = bloodCount.ToString();
    }

    public void SetActiveSkill(Skill skill)
    {
        activeSkill = skill;
        
    }


    public void CloseSkillTree()
    {
        skillTree.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
        isOpen = false;
        Time.timeScale = 1f;
    }

    private void checkUnLock()
    {
        unLockButton.interactable = false;
        SetButtonOpacity(0.5f);
        if(activeSkill.isUnlocked)
        {
            return;
        }
        if (bloodCount < activeSkill.cost)
        {
            return;
        }
        if (activeSkill.previousSkill != null && !activeSkill.previousSkill.isUnlocked)
        {
            return;
        }
        unLockButton.interactable = true;
        SetButtonOpacity(1f);
    }

    void SetButtonOpacity(float alphaValue)
    {
        Color buttonTextColor = buttonText.color;
        buttonTextColor.a = alphaValue;
        buttonText.color = buttonTextColor;

        Color buttonImageColor = buttonImage.color;
        buttonImageColor.a = alphaValue;
        buttonImage.color = buttonImageColor;
    }

    public void UnlockSkill()
    {
        bloodCount -= activeSkill.cost;
        UpdatebloodCountText();
        activeSkill.isUnlocked = true;
        SkillTreeManager.Instance.UnlockSkill(activeSkill.currentNode);
    }
}
