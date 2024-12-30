using UnityEngine;

public class Pounce : MonoBehaviour
{
    public Transform player; 
    public float maxDistance = 30f;
    public int damage = 30; 
    public float cooldownTime = 10f; 
    public float preparationTime = 1f; 
    public float jumpForce = 8.0f;

    private Animator animator; 
    private Rigidbody2D rb; 
    private float cooldownTimer = 0f; 
    private bool isPreparing = false; 

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        if (player != null && Vector2.Distance(transform.position, player.position) <= maxDistance)
        {
            StartCoroutine(PerformSkill());
        }
    }

    private System.Collections.IEnumerator PerformSkill()
    {
        isPreparing = true;
        cooldownTimer = cooldownTime;

        if (animator != null)
        {
            animator.SetTrigger("Prepare");
        }

        yield return new WaitForSeconds(preparationTime);

        if (player != null && Vector2.Distance(transform.position, player.position) <= maxDistance)
        {
            if (animator != null)
            {
                animator.SetTrigger("Pounce");
            }

            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 jumpDirection = new Vector2 (direction.x * jumpForce, jumpForce/2 );
            rb.AddForce(jumpDirection, ForceMode2D.Impulse);

            //yield return new WaitForSeconds(0.5f); 
            if (Vector2.Distance(transform.position, player.position) <= 1.5f) 
            {
                // PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                // if (playerHealth != null)
                // {
                //     playerHealth.TakeDamage(damage);
                // }
            }
        }

        isPreparing = false;
    }

    public bool isPrepare()
    {
        return isPreparing;
    }
}
