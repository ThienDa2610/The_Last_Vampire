using System.Collections;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float fallSpeed = 3f;
    public Vector3 initialPosition;
    public float delayBeforeRespawn = 1f;
    public float timeInterval = 4f;
    public float shakeDuration = 1f;
    public float shakeMagnitude = 0.1f;

    private bool isShaking = false;
    private void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(StartFallingCycle());

    }
    IEnumerator StartFallingCycle()
    {
        yield return new WaitForSeconds(delayBeforeRespawn);

        InvokeRepeating("FallAndRespawn", 0f, timeInterval);
    }
    void FallAndRespawn()
    {
        
        if (!isShaking)
        {
            StartCoroutine(FallAndResetCoroutine());
        }
    }

    IEnumerator FallAndResetCoroutine()
    {
        isShaking = true;

        yield return StartCoroutine(ShakeEffect(shakeDuration, shakeMagnitude));

        Vector3 fallTarget = new Vector3(transform.position.x, -6f, transform.position.z);
        float fallDuration = Mathf.Abs(transform.position.y - fallTarget.y) / fallSpeed;
        float elapsedTime = 0f;
        while (elapsedTime < fallDuration)
        {
            transform.position = Vector3.Lerp(transform.position, fallTarget, (elapsedTime / fallDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = fallTarget;

        transform.position = initialPosition;

        yield return new WaitForSeconds(delayBeforeRespawn);

        isShaking = false;
    }

    IEnumerator ShakeEffect(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float xOffset = Random.Range(-magnitude, magnitude);
            float yOffset = Random.Range(-magnitude, magnitude);
            transform.position = originalPosition + new Vector3(xOffset, yOffset, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }
}
