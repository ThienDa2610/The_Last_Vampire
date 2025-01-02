using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossSkillManager : MonoBehaviour
{
    public Transform player;
    public bool inFight;
    [SerializeField] private float skillCastRate;
    [SerializeField] private float castTimer;
    public float moveSpeed = 2f;
    [SerializeField] private int skillIndex;
    public Animator animator;
    public Rigidbody2D rb;
    public bool isCastingSkill;
    public bool isFacingRight = true;
    public BossSkill[] MoveSet;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        castTimer = 0;
        isCastingSkill = false;
        inFight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inFight)
            return;
        if (!isCastingSkill)
            castTimer += Time.deltaTime;
        if (castTimer > skillCastRate)
        {
            List<int> availableSkill = new List<int>();
            for (int i = 0; i < MoveSet.Length; i++)
            {
                if (MoveSet[i].isReady())
                    availableSkill.Add(i);
            }
            if (availableSkill.Count > 0)
            {
                skillIndex = availableSkill[Random.Range(0, availableSkill.Count)];
            }
            else
                return;
            isCastingSkill = true;
            castTimer = 0;
        }
        if (skillIndex >= 0)
        {
            float distance = Mathf.Abs(player.position.x - transform.position.x);
            AdjustDirection(player);
            if (distance < MoveSet[skillIndex].skillRange)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                MoveSet[skillIndex].Play(player);
                skillIndex = -1;
            }
            else
            {
                ApproachPlayer();
            }
        }
    }

    public void ApproachPlayer()
    {
        animator.SetBool("Walk", true);
        rb.velocity = new Vector2(transform.localScale.x * moveSpeed, rb.velocity.y);
    }

    public void AdjustDirection(Transform target)
    {
        float distance = target.position.x - transform.position.x;
        if (distance < 0 && isFacingRight)
            Flip();
        else if (distance > 0 && !isFacingRight)
            Flip();
    }
    protected void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isFacingRight = !isFacingRight;
    }
}
