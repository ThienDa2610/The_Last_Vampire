using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicDestroy : PlayerInteractGuide
{
    public bool isDestroy = false;
    protected override void Interact()
    {
        base.Interact();
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("isDestroy");
            TurnOffGuide();
            Destroy(gameObject, 1f);
            isDestroy = true;
        }
    }
}
