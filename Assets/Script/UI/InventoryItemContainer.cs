using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryItemContainer : MonoBehaviour
{
    [Header("Inventory Elements")]
    [SerializeField] private Image container;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    public Weapon Weapon { get; private set; }
    public ObjectDataSO ObjectData { get; private set; }

    public void Configure(Color containerColor, Sprite iconSprite)
    {
        container.color = containerColor;
        icon.sprite = iconSprite;
    }
    public void Configure(Weapon weapon, Action clickedCallback)
    {
        Weapon = weapon;
        Color containerColor = ColorHolder.GetColor(weapon.Level);
        Sprite iconSprite = weapon.WeaponData.Sprite;
        Configure(containerColor, iconSprite);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => clickedCallback?.Invoke());
    }
    public void Configure(ObjectDataSO objectData, Action clickedCallback)
    {
        ObjectData = objectData;
        Color containerColor = ColorHolder.GetColor(objectData.Rarity);
        Sprite iconSprite = objectData.Icon;
        Configure(containerColor, iconSprite);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => clickedCallback?.Invoke());
    }

}
