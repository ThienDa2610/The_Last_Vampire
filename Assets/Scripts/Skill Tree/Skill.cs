using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public string skillName;
    public Sprite skillSprite;
    public int cost = 2;
    public Skill previousSkill;

    [TextArea(1,3)]
    public string skillDes;
    public bool isUnlocked;

    
    private Image skillImage;

    void Update()
    {
        skillImage = GetComponent<Image>();
        UpdateSkillImage();
    }

    void UpdateSkillImage()
    {
        if (skillImage != null)
        {
            Color skillImageColor = skillImage.color;

            skillImageColor.a = isUnlocked ? 1f : 0.1f;

            skillImage.color = skillImageColor;
        }
    }
}
