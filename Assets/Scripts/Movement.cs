using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded;
    public TrailRenderer tr;

    bool canDash = true;
    bool isDashing;
    float dashingPower = 24f;
    float dashingTime = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) return;
        float move = Input.GetAxis("Horizontal");

        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (move != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        rb.velocity = new Vector2 (move * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillCDManager.isOffCooldown(SkillType.Dash))
        {
            StartCoroutine(Dash());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
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
        tr.emitting = true;
        SoundManager.PlaySound(SoundType.Dash);
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        HealthManager.Instance.isInvincible = false;
        rb.gravityScale = oriGravity;
        isDashing = false;
    }
}
