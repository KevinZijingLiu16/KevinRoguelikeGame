using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
public class ShopItemContainer : MonoBehaviour

{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI nameText;
      [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image icon;
    [SerializeField] private Image[] levelDependentImage;
    [SerializeField] private Outline outline;
    [SerializeField] private Transform statsContainerParent;

    public static Action<ShopItemContainer, int> onPurchase;
     [field: SerializeField] public Button PurchaseButton { get; private set; }
     [field: SerializeField] public Image LockImage { get; private set; }

      //  [SerializeField] private Button lockButton;
    [SerializeField] private Sprite lockSprite, unlockSprite;

   public bool IsLocked { get; private set; } = false;

   private int weaponLevel;

   public WeaponDataSO WeaponData { get; private set; }
   public ObjectDataSO ObjectData { get; private set; }

    public void Configure(WeaponDataSO weaponData, int level)
    {

        weaponLevel = level;
        WeaponData = weaponData;
        nameText.text = weaponData.Name + " (Lv." + (level + 1) + ")";
        icon.sprite = weaponData.Sprite;
        int weaponPrice = WeaponStatsCalculator.GetPurchasePrice(weaponData, level);
        priceText.text = weaponPrice.ToString();
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
         PurchaseButton.onClick.AddListener(Purchase);
            PurchaseButton.interactable = CurrencyManager.instance.HasEnoughCurrency(weaponPrice);
    }
        public void Configure(ObjectDataSO objectData)
    {

        ObjectData = objectData;
        nameText.text = objectData.Name;
        icon.sprite = objectData.Icon;
        //icon.rectTransform.localScale = Vector3.one * 0.8f;
        priceText.text = objectData.Price.ToString();
        Color imageColor = ColorHolder.GetColor(objectData.Rarity);
        Color.RGBToHSV(imageColor, out float h, out float s, out float v);
        nameText.color = Color.HSVToRGB(h, Mathf.Min(s + 0.8f, 1f), v * 0.5f);

        foreach(Image image in levelDependentImage)
        {
            image.color = imageColor;
        }

        if (outline != null)
            outline.effectColor = ColorHolder.GetOutlineColor(objectData.Rarity);


       //Dictionary<Stat,float> calculatedStats = WeaponStatsCalculator.GetStats(weaponData, level);
            ConfigureStatsContainer(objectData.BaseStats);
              PurchaseButton.onClick.AddListener(Purchase);
            
            PurchaseButton.interactable = CurrencyManager.instance.HasEnoughCurrency(objectData.Price);
    }
    private void ConfigureStatsContainer(Dictionary<Stat,float> stats)
    {
        statsContainerParent.Clear();
        StatContainerManager.GenerateStatContainer(stats, statsContainerParent);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LockButtonCallback()
    {
       IsLocked = !IsLocked;
        UpdateLockVisual();
    }
    private void UpdateLockVisual()
    {
        if (IsLocked)
        {
            LockImage.sprite = lockSprite;
        }
        else
        {
            LockImage.sprite = unlockSprite;
        }
    }

    private void Purchase()
    {
        onPurchase?.Invoke(this, weaponLevel);
    }
}
