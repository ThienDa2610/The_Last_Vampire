using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeCombo : MonoBehaviour
{
    public static PlayerMeleeCombo Instance;
    private StateMachine meleeStateMachine;
    public Collider2D hitbox;

    //new input system
    public PlayerInputAction playerInput;
    private InputAction vampireClawInput;

    private void OnEnable()
    {
        vampireClawInput = playerInput.Player.VampireClaw;
        vampireClawInput.Enable();
        vampireClawInput.performed += MeleeInput;
    }
    private void OnDisable()
    {
        vampireClawInput.Disable();
    }
    private void Awake()
    {
        playerInput = new PlayerInputAction();
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        meleeStateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.J) && !StatusManager.Instance.isStun && meleeStateMachine.CurrentState.GetType() == typeof(IdleComboState))
        {
            meleeStateMachine.SetNextState(new MeleeEntryState());
        }*/
    }
    private void MeleeInput(InputAction.CallbackContext context)
    {
        if(!StatusManager.Instance.isStun && meleeStateMachine.CurrentState.GetType() == typeof(IdleComboState))
        {
            meleeStateMachine.SetNextState(new MeleeEntryState());
        }
    }
}
