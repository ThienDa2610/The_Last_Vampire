using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public Vector3 initialPosition; 
    public float fallDistance = 5f; 
    public float fallSpeed = 2f; 
    public float timeToFall = 2f; 
    private bool isFalling = false;

    void Start()
    {
        initialPosition = transform.position; 
        StartCoroutine(FallAndReturn());
    }
    IEnumerator FallAndReturn()
    {
        while (true)
        {
            isFalling = true;
            Vector3 targetPosition = initialPosition - new Vector3(0, fallDistance, 0); 
            float journeyLength = Vector3.Distance(transform.position, targetPosition);
            float startTime = Time.time;

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                float distanceCovered = (Time.time - startTime) * fallSpeed;
                float fractionOfJourney = distanceCovered / journeyLength;
                transform.position = Vector3.Lerp(transform.position, targetPosition, fractionOfJourney);
                yield return null;
            }

            yield return new WaitForSeconds(timeToFall);

            isFalling = false;
            journeyLength = Vector3.Distance(transform.position, initialPosition);
            startTime = Time.time;

            while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
            {
                float distanceCovered = (Time.time - startTime) * fallSpeed;
                float fractionOfJourney = distanceCovered / journeyLength;
                transform.position = Vector3.Lerp(transform.position, initialPosition, fractionOfJourney);
                yield return null;
            }

            yield return new WaitForSeconds(timeToFall);
        }
    }
}
