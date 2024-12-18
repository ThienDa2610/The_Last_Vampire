using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboEntryState : State
{ 
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        State nextState = (State)new MeleeEntryState();
        stateMachine.SetNextState(nextState);
    }
}
