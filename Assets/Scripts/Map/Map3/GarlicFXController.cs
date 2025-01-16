using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEditor.Rendering;

public class GarlicFXController : MonoBehaviour
{
    public GarlicDestroy garlicDestroy;

    [Header("Time Stats")]
    [SerializeField] private float fadeOutTime = 0.5f;

    [Header("References")]
    [SerializeField] private ScriptableRendererFeature garlicShader;
    [SerializeField] private Material garlicMAT;

    [Header("Intensity")]
    [SerializeField] private float intensityStat = 1.5f;

    private int intensity = Shader.PropertyToID("_Intensity");

    void Start()
    {
        garlicShader.SetActive(false);
    }
    void Update()
    {
        if (garlicDestroy != null && garlicDestroy.getIsNear() && Input.GetKeyDown(KeyCode.F))
            garlicShader.SetActive(true);
    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            garlicShader.SetActive(true);
            garlicMAT.SetFloat(intensity, intensityStat);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            FadeOut();
    }
    */
    void FadeOut()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedValue = Mathf.Lerp(intensityStat, 0, elapsedTime / fadeOutTime);

            garlicMAT.SetFloat(intensity, lerpedValue);
        }
        garlicShader.SetActive(false);
    }
}

