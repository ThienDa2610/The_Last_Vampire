using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public float skillDamage;
    [SerializeField] protected float maxCooldown;
    [SerializeField] protected float currentCooldown;
    public float skillRange;
    protected bool isFacingRight;
    public BossSkillManager skillManager;
    protected Animator animator;
    protected Rigidbody2D rb;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        //currentCooldown = 0f;
        skillManager = GetComponent<BossSkillManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;
        else if (currentCooldown < 0)
            currentCooldown = 0;

    }
    public bool isReady()
    {
        if (currentCooldown == 0)
        {
            //Debug.Log(currentCooldown);
            return true;
        }
        return false;
    }
    public virtual void Play(Transform target)
    {
        

    }
    protected void IntoCooldown()
    {
        currentCooldown = maxCooldown;
    }
    
    
}
