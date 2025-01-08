using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTreeManager : MonoBehaviour
{
    public static SkillTreeManager Instance;
    private static List<SkillNode> unlockedSkillNodeList;

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
                BoxCollider2D hitbox = transform.Find("Hitbox").GetComponent<BoxCollider2D>();
                float dif = hitbox.size.x * 0.3f;
                hitbox.size = new Vector2(hitbox.size.x * 1.3f, hitbox.size.y);
                hitbox.offset = new Vector2(hitbox.offset.x + (dif / 2), hitbox.offset.y);
                break;
            case SkillNode.Tear:
                MeleeBaseState.Tear = true;
                break;
            case SkillNode.PiercingWave:
            //need script
            case SkillNode.Infection:
                EnemyHealthManager.infectable = true;
                break;


            //movement tree
            case SkillNode.Swifty_1:
                Movement.Instance.moveSpeed *= 1.1f;
                Debug.Log(Movement.Instance.moveSpeed);
                break;
            case SkillNode.GlidingBat:
                Gliding.glidable = true;
                break;
            case SkillNode.AirJump:
                Movement.Instance.airJumpable = true;
                break;
            case SkillNode.Swifty_2:
                //no code needed
                break;
            case SkillNode.GlideMaster:
                Gliding.xPercentReduce = 0.9f;
                break;
            case SkillNode.MistyStep:
                SkillCDManager.instance.skillCD[(int)SkillType.Dash] -= 1f;
                break;
            case SkillNode.BloodBoiled:
                //no code needed
                break;
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
