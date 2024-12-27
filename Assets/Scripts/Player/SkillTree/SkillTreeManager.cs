using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTreeManager : MonoBehaviour
{
    public static SkillTreeManager Instance;
    private void Awake()
    {
        Instance = this;
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

    private List<SkillNode> unlockedSkillNodeList;
    public SkillTreeManager()
    {
        unlockedSkillNodeList = new List<SkillNode>();
    }
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
