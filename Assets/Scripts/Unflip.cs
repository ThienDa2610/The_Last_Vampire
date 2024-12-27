using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unflip : MonoBehaviour
{
    public Transform parent;
    [SerializeField] private float scaleX;
    // Start is called before the first frame update
    void Start()
    {
        scaleX = parent.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.localScale.x != scaleX)
        {
            Vector3 newScale =  new Vector3(-transform.localScale.x,transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
            scaleX = parent.localScale.x;
        }
    }
}
