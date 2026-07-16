using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using System;
using System.Linq;

public class ChestObjectContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;
    [SerializeField] private Image[] levelDependentImage;
    [SerializeField] private Outline outline;
    [SerializeField] private Transform statsContainerParent;
    //private WeaponDataSO weaponData;

    [field: SerializeField] public Button TakeButton { get; private set; }
    [field: SerializeField] public Button RecycleButton { get; private set; }
    public TextMeshProUGUI recycleButtonText ;
    public void Configure( ObjectDataSO objectData)
    {
        nameText.text = objectData.Name;
        icon.sprite = objectData.Icon;
        recycleButtonText.text =  objectData.RecyclePrice.ToString();
        Color imageColor = ColorHolder.GetColor(objectData.Rarity);
        Color.RGBToHSV(imageColor, out float h, out float s, out float v);
        nameText.color = Color.HSVToRGB(h, Mathf.Min(s + 0.8f, 1f), v * 0.5f);

        foreach(Image image in levelDependentImage)
        {
            image.color = imageColor;
        }

        if (outline != null)
            outline.effectColor = ColorHolder.GetOutlineColor(objectData.Rarity);


            ConfigureStatsContainer(objectData.BaseStats) ;
    }

    private void ConfigureStatsContainer(Dictionary<Stat,float> stats)
    {
        StatContainerManager.GenerateStatContainer(stats, statsContainerParent);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
}
