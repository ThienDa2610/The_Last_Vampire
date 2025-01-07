using System.Collections;
using UnityEngine;

public class SummoningHowl : BossSkill
{
    public GameObject wolfPrefab; 
    [SerializeField] private int numberOfWolves = 2;
    [SerializeField] private float summonRadius = 5f;
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
<<<<<<< HEAD
        //animator.SetTrigger("Prepare");
=======
        animator.SetTrigger("Prepare");
        animator.SetTrigger("Howl");
>>>>>>> BaoDi
        yield return new WaitForSeconds(howlDuration);
        for (int i = 0; i < numberOfWolves; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(transform.position.x - summonRadius, transform.position.x + summonRadius), transform.position.y);
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
