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
        /*TryGetComponent(out percentageText);*/

        LootManager.current.onChangePercentage += ChangePercentage;
        LootManager.current.onSetPercentage += SetPercentage;

        slider.minValue = minPercentage;
        slider.maxValue = maxPercentage;
    }

    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            ChangePercentage(5);

        if (Input.GetKeyDown(KeyCode.N))
            ChangePercentage(-10);

    }

    public void ChangePercentage(float amount)
    {
        if (!slider)
            return;

        if (amount > maxPercentage)
            amount = maxPercentage;

        if (slider.value >= maxPercentage && amount > 0)
            return;

        slider.value += amount;
        Mathf.Clamp(slider.value, minPercentage, maxPercentage);

        percentageText.text = slider.value.ToString();
    }

    public void SetPercentage(float value)
    {
        slider.value = value;
    }
}
