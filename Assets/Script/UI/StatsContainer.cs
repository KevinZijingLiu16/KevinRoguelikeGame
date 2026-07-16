using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class StatsContainer : MonoBehaviour
{
    [SerializeField] private Image statImage;
    [SerializeField] private TextMeshProUGUI statNameText;
    [SerializeField] private TextMeshProUGUI statValueText;
    
    public void Configure(Sprite icon, string statName, float statValue, bool useColor = false)
    {
        statImage.sprite = icon;
        statNameText.text = statName;

        if (useColor)
            ColorizeStatValueText(statValue);
       
        else 
            statValueText.color = Color.black;
            statValueText.text = statValue.ToString("F2");

       
    }

    private void ColorizeStatValueText(float statValue)
    {
        float sign = Mathf.Sign(statValue);

        if (statValue == 0)
        {
            sign = 0;
        }
        
        float abstStatValue = Mathf.Abs(statValue);

        Color statValueTextColor = Color.black; 

        if (sign > 0)
        {
            statValueTextColor = Color.green;
        }
        else if (sign < 0)
        {
            statValueTextColor = Color.red; 
        }

        statValueText.color = statValueTextColor;

        statValueText.text = abstStatValue.ToString("F2");
    }

    public float GetFontSize()
    {
        return statNameText.fontSize;
    }
    public void SetFontSize(float fontSize)
    {
        statNameText.fontSizeMax = fontSize;
        statValueText.fontSizeMax = fontSize;
    }
}
