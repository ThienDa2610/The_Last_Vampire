using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AerialEnemyMovement : EnemyMovement
{
    public float moveRange;
    public float originalX;
    public float maxY;
    protected override void Start()
    {
        base.Start();
        originalX = transform.position.x;
        maxY = transform.position.y;
    }
    protected override void Update()
    {
        base.Update();
        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
    }
    protected override bool isBlocked()
    {
        if (transform.position.x - originalX >= moveRange && movingRight)
            return true;
        if (originalX - transform.position.x >= moveRange && !movingRight)
            return true;
        Transform pathChecker = transform.Find("PathChecker");
        if (pathChecker != null)
        {
            RaycastHit2D wallHit = Physics2D.Raycast(pathChecker.position, (transform.localScale.x > 0) ? Vector2.right : Vector2.left, 0.3f, groundLayer);
            if (wallHit)
                return true;
        }
        return false;
    }
    protected override void Patrol()
    {
        float direction = movingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * speed, 0f);

        if (isBlocked())
        {
            movingRight = !movingRight;
            Flip();
        }
    }
}
