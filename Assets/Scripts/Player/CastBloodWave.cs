using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CastBloodWave : MonoBehaviour
{
    public static bool bloodWaveLearned = false;
    public GameObject pf_bloodWave;
    public Animator animator;
    //skill tree
    public static bool isEnhanced = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (bloodWaveLearned && Input.GetKeyDown(KeyCode.E) && SkillCDManager.isOffCooldown(SkillType.BloodWave))
        {
            CastingBloodWave();
        }
    }
    private void CastingBloodWave()
    {
        SkillCDManager.IntoCooldown(SkillType.BloodWave);
        animator.SetTrigger("BloodWave");
        GameObject bloodWave = Instantiate(pf_bloodWave,transform.position,Quaternion.identity);
        bloodWave.transform.localScale = transform.localScale;
        bloodWave.GetComponent<BloodWave>().isEnhanced = isEnhanced;
    }
}
