using System.Collections;
using UnityEngine;

public class SummoningHowl : BossSkill
{
    public GameObject wolfPrefab;
    public LayerMask groundLayer;
    [SerializeField] private int numberOfWolves = 2;
    [SerializeField] private float maxSummonRadius = 5f;
    [SerializeField] private float howlDuration = 1f;
    protected override void Start()
    {
        base.Start();
        skillDamage = 0f;
        maxCooldown = 25f;
        skillRange = 15f;
        isFacingRight = true;
    }

    private IEnumerator Howling()
    {
        animator.SetTrigger("Prepare");
        yield return new WaitForSeconds(howlDuration);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, maxSummonRadius, groundLayer);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, maxSummonRadius, groundLayer);
        float rightDistance = rightHit ? rightHit.distance : maxSummonRadius;
        float leftDistance = leftHit ? leftHit.distance : maxSummonRadius;
        for (int i = 0; i < numberOfWolves; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(transform.position.x - leftDistance, transform.position.x + rightDistance), transform.position.y);
            GameObject wolf = Instantiate(wolfPrefab, spawnPosition, Quaternion.identity);
            if (transform.localScale.x > 0)
            {
                wolf.transform.localScale = new Vector3(1f, 1f, 1f);
                wolf.GetComponent<EnemyMovement>().movingRight = true;
            }
            else if (transform.localScale.x < 0)
            {
                wolf.transform.localScale = new Vector3(-1f, 1f, 1f);
                wolf.GetComponent<EnemyMovement>().movingRight = false;
            }
            wolf.GetComponent<EnemyHealthManager>().dropable = false;
        }
        skillManager.isCastingSkill = false;
    }

    public override void Play(Transform target)
    {
        base.Play(target);
        StartCoroutine(Howling());
        IntoCooldown();
    }
}
