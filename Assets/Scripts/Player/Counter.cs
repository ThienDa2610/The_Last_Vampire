using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public static Counter Instance;
    public static bool counterLearned = false;
    public bool isCountering = false;
    public float counterDuration = 0.5f;
    public Animator animator;
    [SerializeField] float teleOffset = 1.5f;
    [SerializeField] float healAmount = 30f;
    [SerializeField] float counterDamage = 30f;

    //skill tree
    public static bool isEnhanced = false;
    private float enhancedDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counterLearned && Input.GetKeyDown(KeyCode.Q) && SkillCDManager.isOffCooldown(SkillType.Counter) && !StatusManager.Instance.isStun)
        {
            StartCoroutine(startBlocking());
        }
    }
    private IEnumerator startBlocking()
    {
        SkillCDManager.IntoCooldown(SkillType.Counter);

        isCountering = true;
        animator.SetBool("isCountering", true);
        yield return new WaitForSeconds(counterDuration);
        if (isEnhanced)
            yield return new WaitForSeconds(enhancedDuration);
        animator.SetBool("isCountering", true);
        isCountering = false;
    }
    public void Countering(GameObject target)
    {
        isCountering = false;
        float newX = (transform.position.x < target.transform.position.x) ? target.transform.position.x + teleOffset : target.transform.position.x - teleOffset;
        transform.position = new Vector3 (newX, target.transform.position.y, transform.position.z);

        HealthManager.Instance.Heal(healAmount);
        target.GetComponent<EnemyHealthManager>().TakeDamage(counterDamage);
    }
}
