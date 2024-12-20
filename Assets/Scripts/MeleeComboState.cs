using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeComboState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        attackIndex = 2;
        duration = 0.5f;
        damage = 15f;
        StateMachine.animator.SetTrigger("Melee" + attackIndex);
        SoundManager.PlaySound(SoundType.Melee2);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new MeleeFinishState());
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}
