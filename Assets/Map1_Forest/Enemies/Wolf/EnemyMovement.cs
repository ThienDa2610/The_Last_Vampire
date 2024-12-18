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

    private Vector3 startPosition;  
    private bool movingRight = true;  
    private bool isFollowingPlayer = false;
    private EnemySoundManager soundManager;


    void Start()
    {
        startPosition = transform.position;  
        soundManager = GetComponent<EnemySoundManager>();
    }

    void Update()
    {
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
            return;
        }
        animator.SetTrigger("Follow");
        float directionX = player.position.x > transform.position.x ? 1 : -1;
        transform.Translate(Vector3.right * directionX * attackSpeed * Time.deltaTime);

        if (directionX > 0 && transform.localScale.x < 0 || directionX < 0 && transform.localScale.x > 0)
        {
            Flip();
            movingRight = !movingRight;
        }
    }

    void Patrol()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x >= startPosition.x + moveDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
