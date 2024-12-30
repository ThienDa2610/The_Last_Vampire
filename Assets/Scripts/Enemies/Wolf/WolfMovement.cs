using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : EnemyMovement
{
    private void Awake()
    {
        speed = 2f;
        speedMultiplier = 1.5f;
        movingRight = true;
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
