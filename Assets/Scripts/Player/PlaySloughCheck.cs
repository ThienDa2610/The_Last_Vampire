using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySloughCheck : MonoBehaviour
{
    public bool isInSlough = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Slough"))
        {
            isInSlough = true;
            Movement.Instance.isInSlough = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Slough"))
        {
            isInSlough = false;
            Movement.Instance.isInSlough = false;
        }
    }
}
