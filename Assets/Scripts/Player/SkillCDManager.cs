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
    [SerializeField] private static Image[] cdOverlay;
    [SerializeField] private static float[] skillCD;
    public static float[] skillCurrentCD;

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
        skillCurrentCD[(int)skillType] = skillCD[(int)skillType];
        cdOverlay[(int)skillType].fillAmount = 1;
    }
    public static bool isOffCooldown(SkillType skillType)
    {
        if (skillCurrentCD[(int)skillType] > 0)
            return false;
        return true;
    }
}
