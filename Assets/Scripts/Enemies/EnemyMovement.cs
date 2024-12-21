using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    protected float speed;
    protected float speedMultiplier;
    public Animator animator;

    protected Rigidbody2D rb;
    protected bool movingRight;  
    public LayerMask groundLayer;
    [SerializeField] EnemyEyeSight eyeSight;
    [SerializeField] EnemyAttack enemyAttack;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        eyeSight = GetComponentInChildren<EnemyEyeSight>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();
    }
    protected virtual void Update()
    {
        if (eyeSight.playerSpotted)
        {
            FollowPlayer();
        }
        else
        {
            animator.SetTrigger("Walk");
            Patrol();
        }
        
    }

    void FollowPlayer()
    {
        if (isBlocked() || enemyAttack.isAttacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        animator.SetTrigger("Follow");
        rb.velocity = new Vector2(transform.localScale.x * speed * speedMultiplier, rb.velocity.y);
    }

    void Patrol()
    {
        float direction = movingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        if (isBlocked())
        {
            movingRight = !movingRight;
            Flip();
        }
    }

    bool isBlocked()
    {
        Transform pathChecker = transform.Find("PathChecker");
        if (pathChecker != null)
        {
            RaycastHit2D wallHit = Physics2D.Raycast(pathChecker.position, (transform.localScale.x > 0) ? Vector2.right : Vector2.left, 0.3f, groundLayer);
            RaycastHit2D groundHit = Physics2D.Raycast(pathChecker.position, Vector2.down, 10f, groundLayer);
            if (wallHit || !groundHit)
                return true;
        }
        return false;
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
