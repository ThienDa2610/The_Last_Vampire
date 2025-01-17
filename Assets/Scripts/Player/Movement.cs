using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
//using UnityEngine.Windows;

public class Movement : MonoBehaviour
{
    public static Movement Instance;

    //new input system
    public PlayerInputAction playerInput;
    private InputAction moveInput;
    private InputAction jumpInput;
    private InputAction dashInput;

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

    public ParticleSystem dust;
    public bool isRight = true;
    public bool isLeft = false;
    public bool isJump = false;
    private void OnEnable()
    {
        moveInput = playerInput.Player.Move;
        moveInput.Enable();

        jumpInput = playerInput.Player.Jump;
        jumpInput.Enable();
        jumpInput.performed += JumpInput;

        dashInput = playerInput.Player.Dash;
        dashInput.Enable();
        dashInput.performed += DashInput;
    }
    private void OnDisable()
    {
        moveInput.Disable();
        jumpInput.Disable();
        dashInput.Disable();
    }
    private void Awake()
    {
        playerInput = new PlayerInputAction();

    }

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
        Vector2 move2d = moveInput.ReadValue<Vector2>();
        float move = move2d.x;

        //horizontal flip
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (!isRight && groundCheck.isOnTheGround())
            {
                dust.Play();
                isRight = true;
                isLeft = false;
            }
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (!isLeft && groundCheck.isOnTheGround())
            {
                dust.Play();
                isRight = false;
                isLeft = true;
            }
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
        /*if (Input.GetKeyDown(KeyCode.Space) && (groundCheck.isOnTheGround() || CanJumpInWater))
        {
            if (groundCheck.isOnTheGround()) { dust.Play(); }
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJump = true;
        }*/
        if (groundCheck.isOnTheGround() && rb.velocity.y <= 0 && isJump)
        {
            isJump = false;
            dust.Play();
        }
        //double jump in water
        if (CanJumpInWater)
        {
            airJumpLeft = true;
        }
        //dash
        /*if (Input.GetKeyDown(KeyCode.LeftShift) && !StatusManager.Instance.isTrap && SkillCDManager.isOffCooldown(SkillType.Dash))
        {
            StartCoroutine(Dash());
        }*/

        //air jump
        /*if (Input.GetKeyDown(KeyCode.Space) && !groundCheck.isOnTheGround() && airJumpable && airJumpLeft)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            airJumpLeft = false;
        }*/
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        if (groundCheck.isOnTheGround() || CanJumpInWater)
        {
            if (groundCheck.isOnTheGround()) { dust.Play(); }
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJump = true;
        }
        else if (!groundCheck.isOnTheGround() && airJumpable && airJumpLeft)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            airJumpLeft = false;
        }
    }
    private void DashInput(InputAction.CallbackContext context)
    {
        if (!StatusManager.Instance.isTrap && SkillCDManager.isOffCooldown(SkillType.Dash))
        {
            StartCoroutine(Dash());
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
