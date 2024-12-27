using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTreeManager : MonoBehaviour
{
    public static SkillTreeManager Instance;
    private List<SkillNode> unlockedSkillNodeList;

    private void Awake()
    {
        Instance = this;
        unlockedSkillNodeList = new List<SkillNode>();
        OnSkillUnlocked += SkillTreeManager_OnSkillUnlocked1;
    }

    private void SkillTreeManager_OnSkillUnlocked1(object sender, OnSkillUnlockedEventArgs e)
    {
        switch (e.skillNode)
        {
            //survival tree
            case SkillNode.Lifeforce_1:
            case SkillNode.Lifeforce_2:
            case SkillNode.Lifeforce_3:
                HealthManager.Instance.maxHealth += 10f;
                break;
            case SkillNode.VapiricClaws:
                MeleeBaseState.lifeSteal = true;
                break;
            case SkillNode.EnhancedSense:
                Counter.isEnhanced = true;
                break;
            case SkillNode.Rebirth_1:
                HealthManager.rebirthCD = 0;
                break;
            case SkillNode.Rebirth_2:
                HealthManager.canRebirth2 = true;
                break;

            //offensive tree
            case SkillNode.ThreateningAura_1:
            case SkillNode.ThreateningAura_2:
                MeleeBaseState.damage *= 1.1f;
                break;
            case SkillNode.ThreateningAura_3:
                MeleeBaseState.damage *= 1.05f;
                break;
            case SkillNode.DestructiveClaws:
            //
            case SkillNode.Tear:
                MeleeBaseState.Tear = true;
                break;


            //movement tree


        }
    }

    public enum SkillNode
    {
        Lifeforce_1,
        VapiricClaws,
        EnhancedSense,
        Lifeforce_2,
        Rebirth_1,
        Lifeforce_3,
        Rebirth_2,

        ThreateningAura_1,
        DestructiveClaws,
        Tear,
        ThreateningAura_2,
        PiercingWave,
        Infection,
        ThreateningAura_3,

        Swifty_1,
        GlidingBat,
        AirJump,
        GlideMaster,
        Swifty_2,
        MistyStep,
        BloodBoiled
    }
    public class OnSkillUnlockedEventArgs : EventArgs
    {
        public SkillNode skillNode;
    }
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public bool IsSkillUnlocked(SkillNode skillNode)
    {
        return unlockedSkillNodeList.Contains(skillNode);
    }
    public void UnlockSkill(SkillNode skillNode)
    {
        if (!IsSkillUnlocked(skillNode))
        {
            unlockedSkillNodeList.Add(skillNode);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillNode = skillNode });
        }
    }
}
