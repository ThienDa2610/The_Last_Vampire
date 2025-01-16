using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public static Movement Instance;

    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Animator animator;
    private Rigidbody2D rb;
    //public TrailRenderer tr;
    private PlayerGroundCheck groundCheck;
    public bool CanJumpInWater = false;
    public bool isInSlough = false;

    bool isDashing;
    [SerializeField] float dashingPower = 12f;
    float dashingTime = 0.2f;

    //skill tree
    // //air jump
    public bool airJumpable;
    public bool airJumpLeft = true;
    // //blood boiled
    [SerializeField] private float bloodBoiledDuration = 5f;
    [SerializeField] private float bloodBoiledEffect = 0.05f;
    public List<float> speedDif;
    public List<float> bloodBoiledTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //tr = GetComponent<TrailRenderer>();
        groundCheck = GetComponentInChildren<PlayerGroundCheck>();

        airJumpable = SkillTreeManager.Instance.IsSkillUnlocked(SkillTreeManager.SkillNode.AirJump);

        speedDif = new List<float>();
        bloodBoiledTimer = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        //bloodboiled stack reduce
        for (int i = 0; i < bloodBoiledTimer.Count; i++)
        {
            bloodBoiledTimer[i] -= Time.deltaTime;
            if (bloodBoiledTimer[i] < 0)
            {
                bloodBoiledTimer.RemoveAt(i);
                moveSpeed -= speedDif[i];
                speedDif.RemoveAt(i);
            }
        }
        if (isDashing) return;
        if (StatusManager.Instance.isStun) return;
        float move = Input.GetAxis("Horizontal");

        //horizontal flip
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //animation
        if (move != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        //horizontal moving
        rb.velocity = new Vector2 (move * moveSpeed, rb.velocity.y);

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.isOnTheGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !StatusManager.Instance.isTrap && SkillCDManager.isOffCooldown(SkillType.Dash))
        {
            StartCoroutine(Dash());
        }

        //air jump
        if (Input.GetKeyDown(KeyCode.Space) && !groundCheck.isOnTheGround() && airJumpable && airJumpLeft)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            airJumpLeft = false;
        }
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (groundCheck.isOnTheGround() || CanJumpInWater) 
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                if (!groundCheck.isOnTheGround() || !CanJumpInWater) 
                {
                    airJumpLeft = false;
                }
            }
        }*/
        if (!isInSlough)
        {
            moveSpeed = 5f;
            jumpForce = 7f;
        }
        else
        {
            moveSpeed = 2.5f;
            jumpForce = 0f;
        }
    }

    private IEnumerator Dash()
    {
        SkillCDManager.IntoCooldown(SkillType.Dash);
        HealthManager.Instance.isInvincible = true;
        isDashing = true;
        float oriGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //tr.emitting = true;
        sfxManager.Instance.PlaySound2D("Dash");
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        HealthManager.Instance.isInvincible = false;
        rb.gravityScale = oriGravity;
        isDashing = false;

        //skill tree
        if (SkillTreeManager.Instance.IsSkillUnlocked(SkillTreeManager.SkillNode.Swifty_2))
        {
            float speedChange = moveSpeed * 0.2f;
            moveSpeed += speedChange;
            yield return new WaitForSeconds(1f);
            moveSpeed -= speedChange;
        }
    }
    public void InflictBloodBoiled()
    {
        if (bloodBoiledTimer.Count < 3)
        {
            bloodBoiledTimer.Add(bloodBoiledDuration);
            speedDif.Add(moveSpeed * bloodBoiledEffect);
            moveSpeed += moveSpeed * bloodBoiledEffect;
        }
        else
        {
            int lowestTimerIdx = bloodBoiledTimer.Count - 1;
            for (int i = 0; i < bloodBoiledTimer.Count - 1; i++)
            {
                if (bloodBoiledTimer[i] < bloodBoiledTimer[lowestTimerIdx])
                    lowestTimerIdx = i;
            }
            bloodBoiledTimer[lowestTimerIdx] = bloodBoiledDuration;
            moveSpeed -= speedDif[lowestTimerIdx];
            speedDif[lowestTimerIdx] = moveSpeed * bloodBoiledEffect;
            moveSpeed += moveSpeed * bloodBoiledEffect;
        }
    }
}
