using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : MonoBehaviour
{
    public Image skillImage;
    public TMP_Text skillName;
    public TMP_Text skillDes;
    public TMP_Text skillCost;

    public int skillButtonId;

    public void PessSkillButton()
    {
        SkillTreeUIManager.instance.activeSkill = transform.GetComponent<Skill>();
        skillImage.sprite = SkillTreeUIManager.instance.skills[skillButtonId].skillSprite;
        skillName.text = SkillTreeUIManager.instance.skills[skillButtonId].skillName;
        skillDes.text = SkillTreeUIManager.instance.skills[skillButtonId].skillDes;   
        skillCost.text = SkillTreeUIManager.instance.skills[skillButtonId].cost.ToString();
    }
}