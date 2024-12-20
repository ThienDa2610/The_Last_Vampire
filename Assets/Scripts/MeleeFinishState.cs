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
        StateMachine.animator.SetTrigger("Melee" + attackIndex);
        SoundManager.PlaySound(SoundType.Melee3);
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
