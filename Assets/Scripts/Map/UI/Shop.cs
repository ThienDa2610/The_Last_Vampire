using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public TMP_Text BloodBuyCountText;
    public TMP_Text BloodCountText;
    public TMP_Text priceText;
    public TMP_Text maxValue;
    public TMP_Text minValue;

    public Slider quantitySlider;

    public TMP_Text ghostText;

    public Button buyButton;
    public TMP_Text buttonText;
    public Image buttonImage;

    public Button increaseButton;
    public TMP_Text buttonTextincrease;
    public Button decreaseButton;
    public TMP_Text buttonTextdecrease;

    private int itemPrice = 2;

    public bool itemRunOut = false;

    private TypeCoinManager typeCoinManager;
    private BloodPotionManager bloodPotionManager;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SavedMaxValueItem"))
        {
            quantitySlider.maxValue = PlayerPrefs.GetInt("SavedMaxValueItem");
        }
        quantitySlider.value = 1;
        typeCoinManager = FindObjectOfType<TypeCoinManager>();
        ghostText.text = typeCoinManager.ghostCount.ToString();
        bloodPotionManager = FindObjectOfType<BloodPotionManager>();
        minValue.text = "1";
        maxValue.text = quantitySlider.maxValue.ToString();
        
        Update();
        if (itemRunOut)
        {
            HandleItemRunOut();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!itemRunOut)
        {
            BloodBuyCountText.text = quantitySlider.value.ToString("F0");
            UpdateIncreaseDecreaseButtons();
        }
        BloodCountText.text = "owned: " + bloodPotionManager.bottleCount.ToString();
        ghostText.text = typeCoinManager.ghostCount.ToString();
        UpdatePriceText();
        UpdateBuyButtonStatus();
        if(!itemRunOut) { HandleKeyboardInput(); }
        if (itemRunOut)
        {
            HandleItemRunOut();
        }
    }

    private void HandleItemRunOut()
    {
        BloodBuyCountText.text = "0";
        UpdateSliderOpacity();
        buyButton.interactable = false;
        SetButtonOpacity(0.5f);
        quantitySlider.maxValue = 2;
        quantitySlider.value = 1;
        maxValue.text = "0";
        minValue.text = "0";

    }
    void UpdatePriceText()
    {
        if (!itemRunOut)
        {
            int totalPrice = Mathf.FloorToInt(quantitySlider.value) * itemPrice;
            priceText.text = totalPrice.ToString();
        }
        else { priceText.text = "0"; }
    }
    public void BuyItem()
    {
        int totalPrice = Mathf.FloorToInt(quantitySlider.value) * itemPrice;
        if (typeCoinManager.SpendGhost(totalPrice))
        {
            for (int i = 0; i < Mathf.FloorToInt(quantitySlider.value); i++)
            {
                bloodPotionManager.CollectBloodBottle();
            }
            
            int remainingMax = Mathf.FloorToInt(quantitySlider.maxValue) - Mathf.FloorToInt(quantitySlider.value);
            if (remainingMax <= 0)
            {
                itemRunOut = true;
            }
            else
            {
                quantitySlider.maxValue = remainingMax;
                quantitySlider.value = 1;
                maxValue.text = quantitySlider.maxValue.ToString();
            }
            

            Update();
        }
    }
    void UpdateBuyButtonStatus()
    {
        if (!itemRunOut)
        {
            int totalPrice = Mathf.FloorToInt(quantitySlider.value) * itemPrice;
            if (totalPrice > typeCoinManager.ghostCount)
            {
                buyButton.interactable = false;
                SetButtonOpacity(0.5f);
            }
            else
            {
                buyButton.interactable = true;
                SetButtonOpacity(1f);
            }
        }
    }
    void SetButtonOpacity(float alphaValue)
    {
        Color buttonTextColor = buttonText.color;
        buttonTextColor.a = alphaValue;
        buttonText.color = buttonTextColor;

        Color buttonImageColor = buttonImage.color;
        buttonImageColor.a = alphaValue;
        buttonImage.color = buttonImageColor;
    }

    public void IncreaseQuantity()
    {
        if (quantitySlider.value < quantitySlider.maxValue)
        {
            quantitySlider.value += 1;
        }
    }

    public void DecreaseQuantity()
    {
        if (quantitySlider.value > quantitySlider.minValue)
        {
            quantitySlider.value -= 1;
        }
    }
    void UpdateIncreaseDecreaseButtons()
    {
        if (quantitySlider.value <= quantitySlider.minValue)
        {
            decreaseButton.interactable = false;
            SetButtonJustTextOpacity2(0.5f);
        }
        else
        {
            decreaseButton.interactable = true;
            SetButtonJustTextOpacity2(1f);
        }

        if (quantitySlider.value >= quantitySlider.maxValue)
        {
            increaseButton.interactable = false;
            SetButtonJustTextOpacity(0.5f);
        }
        else
        {
            increaseButton.interactable = true;
            SetButtonJustTextOpacity(1f);
        }
    }
    void SetButtonJustTextOpacity(float alphaValue)
    {
        Color buttonTextColor = buttonTextincrease.color;
        buttonTextColor.a = alphaValue;
        buttonTextincrease.color = buttonTextColor;
    }
    void SetButtonJustTextOpacity2(float alphaValue)
    {
        Color buttonTextColor = buttonTextdecrease.color;
        buttonTextColor.a = alphaValue;
        buttonTextdecrease.color = buttonTextColor;
    }
    void UpdateSliderOpacity()
    {

        quantitySlider.interactable = false;
        SetSliderOpacity(0.5f);

    }
    void SetSliderOpacity(float alphaValue)
    {/*
        Color sliderFillColor = quantitySlider.fillRect.GetComponent<Image>().color;
        sliderFillColor.a = alphaValue;
        quantitySlider.fillRect.GetComponent<Image>().color = sliderFillColor;

        Color sliderBackgroundColor = quantitySlider.targetGraphic.GetComponent<Image>().color;
        sliderBackgroundColor.a = alphaValue;
        quantitySlider.targetGraphic.GetComponent<Image>().color = sliderBackgroundColor;*/

        decreaseButton.interactable = false;
        SetButtonJustTextOpacity2(0.5f);

        increaseButton.interactable = false;
        SetButtonJustTextOpacity(0.5f);

        SetButtonJustTextOpacity3(alphaValue);
        SetButtonJustTextOpacity4(alphaValue);
    }
    void SetButtonJustTextOpacity3(float alphaValue)
    {
        Color buttonTextColor = maxValue.color;
        buttonTextColor.a = alphaValue;
        maxValue.color = buttonTextColor;
    }
    void SetButtonJustTextOpacity4(float alphaValue)
    {
        Color buttonTextColor = minValue.color;
        buttonTextColor.a = alphaValue;
        minValue.color = buttonTextColor;
    }
    void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            quantitySlider.value = Mathf.Clamp(1, quantitySlider.minValue, quantitySlider.maxValue);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (quantitySlider.maxValue >= 2)
            {
                quantitySlider.value = Mathf.Clamp(2, quantitySlider.minValue, quantitySlider.maxValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (quantitySlider.maxValue >= 3)
            {
                quantitySlider.value = Mathf.Clamp(3, quantitySlider.minValue, quantitySlider.maxValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (quantitySlider.maxValue >= 4)
            {
                quantitySlider.value = Mathf.Clamp(4, quantitySlider.minValue, quantitySlider.maxValue);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (quantitySlider.maxValue >= 5)
            {
                quantitySlider.value = Mathf.Clamp(5, quantitySlider.minValue, quantitySlider.maxValue);
            }
        }
    }
}
