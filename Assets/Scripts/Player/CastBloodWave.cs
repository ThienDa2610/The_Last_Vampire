using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CastBloodWave : MonoBehaviour
{
    public static bool bloodWaveLearned = false;
    public float bloodWavePenalty = 15f;
    public GameObject pf_bloodWave;
    public Animator animator;
    //new input system
    public PlayerInputAction playerInput;
    private InputAction bloodWaveInput;
    //skill tree
    public static bool isEnhanced = false;
    private void OnEnable()
    {
        bloodWaveInput = playerInput.Player.BloodWave;
        bloodWaveInput.Enable();
        bloodWaveInput.performed += CastingBloodWave;
    }
    private void OnDisable()
    {
        bloodWaveInput.Disable();
    }
    private void Awake()
    {
        playerInput = new PlayerInputAction();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        
    }
    private void CastingBloodWave(InputAction.CallbackContext context)
    {
        if(bloodWaveLearned && !StatusManager.Instance.isStun && SkillCDManager.isOffCooldown(SkillType.BloodWave))
        {
            SkillCDManager.IntoCooldown(SkillType.BloodWave);
            HealthManager.Instance.takeDamage(bloodWavePenalty, null);
            animator.SetTrigger("BloodWave");
            GameObject bloodWave = Instantiate(pf_bloodWave, transform.position, Quaternion.identity);
            bloodWave.transform.localScale = transform.localScale;
            bloodWave.GetComponent<BloodWave>().isEnhanced = isEnhanced;
        }
    }
}
