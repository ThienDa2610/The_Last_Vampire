using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillManager : MonoBehaviour
{
    [SerializeField] private float skillCastRate = 2f;
    [SerializeField] private float castTimer;
    public BossSkill[] MoveSet;
    // Start is called before the first frame update
    void Start()
    {
        castTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        castTimer += Time.deltaTime;
        if (castTimer > skillCastRate)
        {
            List<int> availableSkill = new List<int>();
            for (int i = 0; i < MoveSet.Length; i++)
            {
                if (MoveSet[i].isReady())
                    availableSkill.Add(i);
            }
            int skillIndex;
            if (availableSkill.Count > 0)
            {
                skillIndex = Random.Range(0, availableSkill.Count);
            }
            else
                return;

            castTimer = - MoveSet[skillIndex].skillCastDuration;

            MoveSet[skillIndex].Play();
        }
    }
}
