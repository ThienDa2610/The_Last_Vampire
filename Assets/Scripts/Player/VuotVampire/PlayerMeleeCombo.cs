using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeCombo : MonoBehaviour
{
    public static PlayerMeleeCombo Instance;
    private StateMachine meleeStateMachine;

    public Collider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        meleeStateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && meleeStateMachine.CurrentState.GetType() == typeof(IdleComboState))
        {
            meleeStateMachine.SetNextState(new MeleeEntryState());
        }
    }
}
