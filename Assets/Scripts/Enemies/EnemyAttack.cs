using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float attackDamage;     
    public float attackRate;
    public float attackDuration;
    public bool isAttacking;
<<<<<<< HEAD
    public bool isPlayerInRange;
    public float attackTimer;
=======
>>>>>>> BaoDi
    public Animator animator;

    protected virtual void Start()
    {
        animator = GetComponentInParent<Animator>();
<<<<<<< HEAD
        isPlayerInRange = false;
=======
>>>>>>> BaoDi
        isAttacking = false;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
<<<<<<< HEAD
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
=======
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            StartCoroutine(Attack());
>>>>>>> BaoDi
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
<<<<<<< HEAD
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
=======
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines(); 
>>>>>>> BaoDi
            isAttacking = false;
        }
    }

<<<<<<< HEAD
    protected virtual void Update()
    {
        
=======
    protected virtual IEnumerator Attack()
    {
        return null;
>>>>>>> BaoDi
    }
}
