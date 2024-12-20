using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        attackIndex = 1;
        duration = 0.5f;
        damage = 10f;
        StateMachine.animator.SetTrigger("Melee" + attackIndex);
        SoundManager.PlaySound(SoundType.Melee1);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new MeleeComboState());
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}
