using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFinishState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        attackIndex = 3;
        duration = 0.5f;
        damage = 18f;
        if (SkillTreeManager.Instance.IsSkillUnlocked(SkillTreeManager.SkillNode.ThreateningAura_1))
        {
            damage *= 1.1f;
        }
        if (SkillTreeManager.Instance.IsSkillUnlocked(SkillTreeManager.SkillNode.ThreateningAura_2))
        {
            damage *= 1.1f;
        }
        if (SkillTreeManager.Instance.IsSkillUnlocked(SkillTreeManager.SkillNode.ThreateningAura_3))
        {
            damage *= 1.05f;
        }
        StateMachine.animator.SetTrigger("Melee" + attackIndex);
        sfxManager.Instance.PlaySound2D("VuotVampire_3");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }
    }
}
