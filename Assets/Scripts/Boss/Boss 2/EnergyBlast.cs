using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBlast : MonoBehaviour
{
    public float skillDamage = 15f;
    public float maxDuration = 10f;
    public float moveSpeed = 15f;
    public bool isFalling = false;
    public Vector3 direction = Vector3.zero;
    public GameObject pf_effExplosion;
    public GameObject shooter;
    Rigidbody2D rb;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, maxDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFalling)
        {
            if (direction == Vector3.zero)
                rb.velocity = new Vector3(transform.localScale.x * moveSpeed, 0f, 0f);
            else
                rb.velocity = new Vector3(direction.x * moveSpeed, direction.y * moveSpeed, 0f);
        }
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthManager.Instance.takeDamage(skillDamage, shooter);
        }
        if (collision.CompareTag("Ground") || collision.CompareTag("Player"))
        {
            CreateEffect();
            Destroy(gameObject);
        }
    }
    protected void CreateEffect()
    {
        Instantiate(pf_effExplosion, transform.position, Quaternion.identity);
    }
}
