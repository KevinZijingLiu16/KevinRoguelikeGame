using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class StatsContainer : MonoBehaviour
{
    [SerializeField] private Image statImage;
    [SerializeField] private TextMeshProUGUI statNameText;
    [SerializeField] private TextMeshProUGUI statValueText;
    
    public void Configure(Sprite icon, string statName, string statValue)
    {
        statImage.sprite = icon;
        statNameText.text = statName;
        statValueText.text = statValue;
    }
}
