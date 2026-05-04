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

public class WeaponSelectionContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;
    [SerializeField] private Image[] levelDependentImage;
    [SerializeField] private Outline outline;
    [SerializeField] private Transform statsContainerParent;
    //private WeaponDataSO weaponData;

    [field: SerializeField] public Button Button { get; private set; }

    public void Configure(WeaponDataSO weaponData, int level)
    {
        nameText.text = weaponData.Name + " (Lv." + (level + 1) + ")";
        icon.sprite = weaponData.Sprite;
        Color imageColor = ColorHolder.GetColor(level);
        Color.RGBToHSV(imageColor, out float h, out float s, out float v);
        nameText.color = Color.HSVToRGB(h, Mathf.Min(s + 0.8f, 1f), v * 0.5f);

        foreach(Image image in levelDependentImage)
        {
            image.color = imageColor;
        }

        if (outline != null)
            outline.effectColor = ColorHolder.GetOutlineColor(level);


        Dictionary<Stat,float> calculatedStats = WeaponStatsCalculator.GetStats(weaponData, level);
            ConfigureStatsContainer(calculatedStats);
    }

    private void ConfigureStatsContainer(Dictionary<Stat,float> calculatedStats)
    {
        StatContainerManager.GenerateStatContainer(calculatedStats, statsContainerParent);
    }
    public void Select()
    {
        // Add selection logic here, such as highlighting the container or playing a sound effect
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.2f).setEase(LeanTweenType.easeInOutSine);
    }
        public void Deselect()
        {
            // Add deselection logic here, such as removing the highlight or stopping the sound effect
            LeanTween.cancel(gameObject);
            LeanTween.scale(gameObject, Vector3.one, 0.2f);
        }
}
