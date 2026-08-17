using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class ShopManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] private Transform containerParent;

    [SerializeField] private ShopItemContainer shopItemContainerPrefab;

    [SerializeField] private Button rerollButton;
    [SerializeField] private int rerollPrice;
[SerializeField] private TextMeshProUGUI rerollPriceText;

[SerializeField] private PlayerWeapons playerWeapons;
[SerializeField] private PlayerObject playerObjects;
 

private void Awake()
    {
        CurrencyManager.onUpdated += CurrencyUpdatedCallback;
        ShopItemContainer.onPurchase += ItemPurchasedCallback;
    }
    private void OnDestroy()
    {
        CurrencyManager.onUpdated -= CurrencyUpdatedCallback;
        ShopItemContainer.onPurchase -= ItemPurchasedCallback;
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.SHOP)
        {
            CurrencyManager.instance.UpdateText();
            Configure();
            UpdateRerollVisual();
        }
       
    }

    private void Configure()
    {
       List<GameObject> toDestroy = new List<GameObject>();
       for(int i = 0; i < containerParent.childCount; i++)
        {
            ShopItemContainer container = containerParent.GetChild(i).GetComponent<ShopItemContainer>();
            if(!container.IsLocked)
            {
                toDestroy.Add(container.gameObject);
            }

        }
        while(toDestroy.Count > 0)
        {
         Transform t = toDestroy[0].transform;
         t.SetParent(null);
         Destroy(t.gameObject);
         toDestroy.RemoveAt(0);
         
        }



        int containerToAdd = 6 - containerParent.childCount;
        int weaponContainerCount = Random.Range(Mathf.Min(2, containerToAdd), containerToAdd);
        int objectContainerCount = containerToAdd - weaponContainerCount;
        for (int i = 0; i < weaponContainerCount; i++)
        {
          ShopItemContainer weaponContainerInstance = Instantiate(shopItemContainerPrefab, containerParent);
          //weaponContainerInstance.name = "WeaponContainer";
            WeaponDataSO randomWeapon = ResourcesManager.GetRandomWeapon();
            weaponContainerInstance.Configure(randomWeapon, Random.Range(0, 2));
          
          
        }
        for (int i = 0; i < objectContainerCount; i++)
        {
            ShopItemContainer objectContainerInstance = Instantiate(shopItemContainerPrefab, containerParent);
            //objectContainerInstance.name = "ObjectContainer";
            ObjectDataSO randomObject = ResourcesManager.GetRandomObject();

            objectContainerInstance.Configure(randomObject);
        }
    }

    public void Reroll()
    {
        Configure();
        CurrencyManager.instance.UseCoins(rerollPrice);
    }

    private void UpdateRerollVisual()
    {
        rerollPriceText.text = rerollPrice.ToString();
        rerollButton.interactable = CurrencyManager.instance.HasEnoughCurrency(rerollPrice);
    }

    private void CurrencyUpdatedCallback()
    {
        UpdateRerollVisual();
    }

    private void ItemPurchasedCallback(ShopItemContainer container, int level)
    {
        if(container.WeaponData != null)
        {
            TryPurchaseWeapon(container, level);
           
        }
       
    }
    public void TryPurchaseWeapon(ShopItemContainer container, int level)
    {
        if(playerWeapons.TryAddWeapon(container.WeaponData, level))
        {
            
        }
    }
}

