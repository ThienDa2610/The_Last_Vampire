using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float effectDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, effectDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
