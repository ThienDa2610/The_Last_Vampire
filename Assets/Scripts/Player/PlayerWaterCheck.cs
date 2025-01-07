using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterCheck : MonoBehaviour
{
    public bool isInWater = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Water"))
        {
            isInWater = true;
            Movement.Instance.CanJumpInWater = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Water"))
        {
            isInWater = false;
            Movement.Instance.CanJumpInWater = false;
        }
    }
   
}
