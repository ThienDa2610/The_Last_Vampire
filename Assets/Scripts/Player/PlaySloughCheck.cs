using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySloughCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slough"))
        {
            StatusManager.Instance.InflictSlough();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Slough"))
        {
            StatusManager.Instance.CleanseSlough();
        }
    }
}
