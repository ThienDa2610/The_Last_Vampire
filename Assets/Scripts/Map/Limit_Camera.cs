using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit_Camera : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;      
    public float minX, maxX;        
    public Vector3 offset = new Vector3(0f, 0, 0); 

    void FixedUpdate()
    {

        Vector3 targetPosition = target.position + offset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = transform.position.y;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
