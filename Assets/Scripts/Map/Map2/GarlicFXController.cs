using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class GarlicFXController : MonoBehaviour
{

    public TMP_Text dialogText;
    public string idleMessage;

    public Animator animator;
    private bool isPlayerNear = false;
    public bool destroyed = false;

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
        if (dialogText != null)
            dialogText.enabled = false;
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !destroyed)
        {
            animator.SetTrigger("isDestroy");
            destroyed = true;
            FadeOut();
            if (dialogText != null)
                dialogText.enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (!destroyed && dialogText != null)
            {
                dialogText.enabled = true;
                dialogText.text = idleMessage;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (dialogText != null)

                dialogText.enabled = false;
        }
    }
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
