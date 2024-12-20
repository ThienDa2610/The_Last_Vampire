using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f;  
    public float attackSpeed = 4.0f;
    public float moveDistance = 5.0f;  
    public float followDistance = 10.0f; 
    public float stopDistance = 0.5f;    
    public Transform player;  
    public Animator animator;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private Vector3 startPosition;  
    private bool movingRight = true;  
    private bool isFollowingPlayer = false;
    private EnemySoundManager soundManager;
    private Pounce pounce;
    public LayerMask groundLayer; 
    

    void Start()
    {
        pounce = GetComponent<Pounce>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;  
        soundManager = GetComponent<EnemySoundManager>();
    }

    void Update()
    {
        CheckGround();
        if(pounce.isPrepare())
            return;
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x); 

        if (distanceToPlayer <= followDistance)
        {
            if (!isFollowingPlayer) 
            {
                soundManager.PlayAttackSound();
            }
            isFollowingPlayer = true;
        }
        else
        {
            if (isFollowingPlayer) 
            {
                soundManager.StopAttackSound();
            }
            isFollowingPlayer = false;
        }

        if (isFollowingPlayer)
        {
            FollowPlayer(distanceToPlayer);
        }
        else
        {
            animator.SetTrigger("Walk");
            Patrol();
        }
    }

    void FollowPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer <= stopDistance)
        {
            animator.SetTrigger("Attack");
            //player.TakeDamage();
            rb.velocity = Vector2.zero;
            return;
        }
        animator.SetTrigger("Follow");
        float directionX = player.position.x > transform.position.x ? 1 : -1;
        rb.velocity = new Vector2(directionX * attackSpeed, rb.velocity.y);

        if (directionX > 0 && transform.localScale.x < 0 || directionX < 0 && transform.localScale.x > 0)
        {
            Flip();
            movingRight = !movingRight;
        }
    }

    void Patrol()
    {
        float direction = movingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        if (movingRight && transform.position.x >= startPosition.x + moveDistance)
        {
            movingRight = false;
            Flip();
        }
        else if (!movingRight && transform.position.x <= startPosition.x - moveDistance)
        {
            movingRight = true;
            Flip();
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
