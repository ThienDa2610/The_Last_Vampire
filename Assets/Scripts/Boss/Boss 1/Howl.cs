using UnityEngine;

public class HowlSummon : MonoBehaviour
{
    public GameObject wolfPrefab; 
    public int numberOfWolves = 5; 
    public float summonRadius = 5f; 
    public float cooldownTime = 20f; 
    public float howlDuration = 1.5f; 

    private Animator animator; 
    private float cooldownTimer = 0f; 
    private Transform player; 

    void Start()
    {
        animator = GetComponent<Animator>();

        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            //player = enemyMovement.player;
        }
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        if (player != null)
        {
            StartCoroutine(PerformHowlSkill());
        }
    }

    private System.Collections.IEnumerator PerformHowlSkill()
    {
        cooldownTimer = cooldownTime;

        if (animator != null)
        {
            animator.SetTrigger("Howl");
        }
        yield return new WaitForSeconds(howlDuration);

        for (int i = 0; i < numberOfWolves; i++)
        {
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * summonRadius;
            GameObject wolf = Instantiate(wolfPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
