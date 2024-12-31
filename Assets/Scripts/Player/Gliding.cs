using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : MonoBehaviour
{
    public static bool glidable;
    [SerializeField] private float yMaxSpeed = -2f;
    public static float xPercentReduce;
    [SerializeField] private bool isGliding;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        glidable = SkillTreeManager.Instance.IsSkillUnlocked(SkillTreeManager.SkillNode.GlidingBat);
        rb = GetComponent<Rigidbody2D>();
        xPercentReduce = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!glidable) return;
        if (Input.GetKey(KeyCode.Space))
        {
            StartGlide();
        }
        else
        {
            StopGlide();
        }
        if (isGliding && rb.velocity.y < yMaxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x * xPercentReduce, yMaxSpeed);
        }
    }
    void StartGlide()
    {
        if (!isGliding)
        {
            isGliding = true;
            //rb.gravityScale = glideGravityScale;
        }
    }
    void StopGlide()
    {
        if (isGliding)
        {
            isGliding = false;
            //rb.gravityScale = normalGravityScale;
        }
    }
}
