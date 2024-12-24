using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType
{
    Dash,
    Counter
}
public class SkillCDManager : MonoBehaviour
{
    public static SkillCDManager instance;
    [SerializeField] private Image[] cdOverlay;
    [SerializeField] private float[] skillCD;
    public float[] skillCurrentCD;
    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cdOverlay.Length; i++)
        {
            if (skillCurrentCD[i] > 0)
            {
                float temp = skillCurrentCD[i] - Time.deltaTime;
                skillCurrentCD[i] = temp > 0 ? temp : 0;
                cdOverlay[i].fillAmount = skillCurrentCD[i] / skillCD[i];
            }
        }
    }
    public static void IntoCooldown(SkillType skillType)
    {
        instance.skillCurrentCD[(int)skillType] = instance.skillCD[(int)skillType];
        instance.cdOverlay[(int)skillType].fillAmount = 1;
    }
    public static bool isOffCooldown(SkillType skillType)
    {
        if (instance.skillCurrentCD[(int)skillType] > 0)
            return false;
        return true;
    }
}
