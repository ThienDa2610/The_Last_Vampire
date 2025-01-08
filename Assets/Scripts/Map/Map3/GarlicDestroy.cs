using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicDestroy : PlayerInteractGuide
{
    protected override void Interact()
    {
        base.Interact();
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("isDestroy");
            TurnOffGuide();
            Destroy(gameObject, 0.5f);
        }
    }
}
