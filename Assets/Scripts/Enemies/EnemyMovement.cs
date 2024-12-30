using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f;  
    public float followSpeed = 3.0f;
    public float attackSpeed = 1.5f;
    public float moveDistance = 5.0f;  
    public float followDistance = 10.0f; 
    public float stopDistance = 1f;    
    public Transform player;  
    public Animator animator;
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float nextAttackTime = 0f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private Vector3 startPosition;  
    private bool movingRight = true;  
    private bool isFollowingPlayer = false;
    private Pounce pounce;
    public LayerMask groundLayer; 
    

    void Start()
    {
        pounce = GetComponent<Pounce>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;  
    }

    void Update()
    {
        CheckGround();
        /*if(pounce.isPrepare())
            return;*/
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x); 

        if (distanceToPlayer <= followDistance)
        {
            isFollowingPlayer = true;
        }
        else
        {
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
        if (distanceToPlayer <= stopDistance && nextAttackTime < Time.time)
        {
            animator.SetTrigger("Attack");
            sfxManager.Instance.PlaySound3D("Enemy_1", transform.position);
            HealthManager.Instance.takeDamage(attackDamage);
            nextAttackTime = Time.time + attackSpeed;
        }
    }

    void FollowPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer <= stopDistance) return;
        animator.SetTrigger("Follow");
        float directionX = player.position.x > transform.position.x ? 1 : -1;
        rb.velocity = new Vector2(directionX * followSpeed, rb.velocity.y);

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
