using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeatBar : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;
    public Camera thirdCamera;
    public Camera forthCamera;
    public Camera fifthCamera;
    public Slider heatSlider;  
    public Image screenOverlay;  
    public Color normalColor = new Color(0, 0, 0, 0); 
    private Color warningColor = new Color(1, 0.4f, 0, 0.1f);

    //public bool isOverheating = false;

    public GameObject player;
    public HealthManager health;

    public PlayerWaterCheck playerWaterCheck;

    void Start()
    {
        if (health == null)
        {
            health = player.GetComponent<HealthManager>();
        }
        if (PlayerPrefs.HasKey("HeatValue"))
        {
            heatSlider.value = PlayerPrefs.GetFloat("HeatValue");
        }
        screenOverlay.color = normalColor;
    }

    void Update()
    {
        if (playerWaterCheck.isInWater || secondaryCamera.gameObject.activeSelf || thirdCamera.gameObject.activeSelf)
        {
            StopOverheating();
        }
        else if (mainCamera.gameObject.activeSelf )
        {
            ResumeOverheating();
        }
        else if(forthCamera.gameObject.activeSelf || fifthCamera.gameObject.activeSelf)
        {
            if (heatSlider.value < heatSlider.maxValue)
            {
                heatSlider.value += Time.deltaTime * 0.005f;
            }
        }
        if (heatSlider.value >= 0.7f * heatSlider.maxValue && heatSlider.value < heatSlider.maxValue)
        {
            float lerpValue = (heatSlider.value - 0.7f * heatSlider.maxValue) / (0.3f * heatSlider.maxValue);
            screenOverlay.color = Color.Lerp(normalColor, warningColor, lerpValue);
        }
        else if (heatSlider.value < 0.8f * heatSlider.maxValue)
        {
            screenOverlay.color = normalColor;
        }
        if (heatSlider.value >= heatSlider.maxValue)
        {
            StartCoroutine(DecreaseHealth());
        }
        if (StatusManager.Instance.isInSlough)
        {
            if (heatSlider.value < heatSlider.maxValue)
            {
                heatSlider.value += Time.deltaTime * 0.1f;
            }
        }
    }
    IEnumerator DecreaseHealth()
    {
        while (heatSlider.value >= heatSlider.maxValue)
        {
            health.currentHealth -= 0.007f;
            health.UpdateHealthbar();
            yield return new WaitForSeconds(1f);
        }
    }
    public void StopOverheating()
    {
        StopCoroutine(DecreaseHealth()); 
        heatSlider.value -= Time.deltaTime * 0.5f;  
    }

    public void ResumeOverheating()
    {
        if (heatSlider.value < heatSlider.maxValue)
        {
            heatSlider.value += Time.deltaTime * 0.025f;
        }
    }
    public void UpdateHeatbar()
    {
        screenOverlay.fillAmount = heatSlider.value / heatSlider.maxValue;

    }
}
