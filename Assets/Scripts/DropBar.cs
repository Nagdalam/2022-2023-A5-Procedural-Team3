using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropBar : MonoBehaviour
{
    [ReadOnly] public float minPercentage = 0;
    [ReadOnly] public float maxPercentage = 100;
    public float amountPercentageIncrease = 0;
    [ReadOnly] public float percentage = 0;

    public Slider slider;
    /*public TextMeshPro percentageText = null;*/
    public TextMeshProUGUI percentageText = null;

    void Awake()
    {
        TryGetComponent(out slider);
        TryGetComponent(out percentageText);

        LootManager.current.onChangePercentage += ChangePercentage;
        LootManager.current.onSetPercentage += SetPercentage;

        slider.minValue = minPercentage;
        slider.maxValue = maxPercentage;
    }

    void Update()
    {
        
    }

    public void ChangePercentage(float amount)
    {
        if (amount > maxPercentage)
            amount = maxPercentage;

        if (slider.value >= maxPercentage && amount > 0)
            return;

        slider.value += amount;
        Mathf.Clamp(slider.value, minPercentage, maxPercentage);

        percentageText.text = slider.ToString();
    }

    public void SetPercentage(float value)
    {
        slider.value = value;
    }
}
