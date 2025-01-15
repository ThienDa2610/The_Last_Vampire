using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomController : MonoBehaviour
{
    public Animator animator;

    public CheckRange isNear;
    public bool explode = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (isNear == null)
        {
            isNear = GetComponentInChildren<CheckRange>();
        }
    }

    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            explode = true;
        }
        if (explode == true)
        {
            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {   
        animator.SetTrigger("startCountDown");
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("explode");
        if (isNear.isNear)
        {
            Debug.Log("Take damage");
            HealthManager.Instance.takeDamage(10, null);
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
